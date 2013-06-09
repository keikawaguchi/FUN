using UnityEngine;
using System.Collections;

public class MerliniBehavior : MonoBehaviour {
	private const string CD_VIEWER_PREFAB_PATH = "Prefabs/Skills/CooldownViewer";
	private const string HAMMERTIME_PREFAB_PATH = "Prefabs/Skills/HammerTime";
	private const string BOMB_PREFAB_PATH = "Prefabs/Bomb/Bomb";
	
	private const string IDLE_TEXTURE_PATH = "Textures/SpriteSheets/Characters/Merlini/MerliniIdleSpritesheet";
	private const string RUNNING_TEXTURE_PATH = "Textures/SpriteSheets/Characters/Merlini/MerliniRunningSpritesheet";
	
	private CharacterMovement characterMovement;
	private XInputController controller;
	
	private GameObject animation;
	private GameObject hammerPrefab;
	private GameObject hammer;
	private GameObject bomb;
	
	// skill cooldown times
	private const float skillOneCD = 10f;  // hammer time
	private const float skillTwoCD = 25f;  // bomb voyage
	
	// skill timers
	private float hammerTimeTimer = -99f;
	private float bombVoyageTimer = -99f;
	
	// view cooldown
	private GameObject skillOneCDPrefab;
	private GameObject skillOneCDViewer;
	private GameObject skillTwoCDPrefab;
	private GameObject skillTwoCDViewer;

	void Start () {
		loadSkills();
		loadScripts();
		loadAnimation();
		
		skillOneCDViewer = Instantiate(skillOneCDPrefab) as GameObject;
		skillTwoCDViewer = Instantiate(skillTwoCDPrefab) as GameObject;
	}
	
	void Update () {
		updateCDViewerPos();
		updateCDViewerColor();
	
		// don't do anything if hero is dead
		bool isAlive = gameObject.GetComponent<Hero>().isAlive;
		if (!isAlive)
			return;

		if (isStunned()) {
			return;
		}
		
		if (isSilence ()) {
			return;
		}
		
		if(controller.GetButtonPressed("Skill1")) {
			hammerTimeButtonPress();
		}
		
		if(controller.GetButtonPressed("Skill2")) {
			bombVoyageButtonPress();
		}
	}
	
	#region Initialization Methods
	private void loadSkills() {
		hammerPrefab = Resources.Load (HAMMERTIME_PREFAB_PATH) as GameObject;
		bomb = Resources.Load (BOMB_PREFAB_PATH) as GameObject;
		
		skillOneCDPrefab = Resources.Load (CD_VIEWER_PREFAB_PATH) as GameObject;	
		skillTwoCDPrefab = Resources.Load (CD_VIEWER_PREFAB_PATH) as GameObject;	
	}
	
	private void loadScripts() {
		characterMovement = GetComponent<CharacterMovement>();
		controller = GetComponent<XInputController>();
	}
	
	private void loadAnimation() {
		animation = Resources.Load ("Prefabs/Animations/Characters/Wizard") as GameObject;
		animation = Instantiate (animation) as GameObject;
		animation.GetComponent<Animation>().attachToObject (gameObject);
		
		Texture idle = Resources.Load (IDLE_TEXTURE_PATH) as Texture;
		Texture running = Resources.Load (RUNNING_TEXTURE_PATH) as Texture;
		animation.GetComponent<Animation>().setIdleTexture(idle);
		animation.GetComponent<Animation>().setRunningTexture(running);
	}
	#endregion
	
	private bool isStunned() {
		return characterMovement.isStunned();
	}
	
	private bool isSilence() {
		return characterMovement.getIsSilence ();
	}
	
	#region Merlini Skills
	private void hammerTimeButtonPress() {
		
		// check if cooldown expired
		if (Time.time - hammerTimeTimer > skillOneCD) {

			Debug.Log("It's hammer time!!");
			hammer = Instantiate (hammerPrefab) as GameObject;
			hammer.GetComponent<HammerTime>().SetStunOwner (gameObject);
			hammer.transform.position = transform.position;
			hammer.transform.forward = characterMovement.getAimDirection();
			
			int teamNum = gameObject.GetComponent<Hero>().getTeamNumber();
			hammer.GetComponent<HammerTime>().SetTeamNum(teamNum);
			
			// keep track of cooldown timer
			hammerTimeTimer = Time.time;
		}
	}
	
	private void bombVoyageButtonPress() {		
		if (Time.time - bombVoyageTimer < skillTwoCD) {
			return;
		}
			
		GameObject[] players;
		players = GameObject.FindGameObjectsWithTag("Player");

		foreach(GameObject player in players) {
			GameObject instantiateBomb = Instantiate(bomb) as GameObject;
			instantiateBomb.transform.position = player.transform.position;
			BombBehavior boom = instantiateBomb.GetComponent<BombBehavior>();
			boom.setHero(gameObject.GetComponent<Hero>());
		}
		
		bombVoyageTimer = Time.time;
	}
	#endregion
	
	public float getHammerTimeCD()
	{
		if(Time.time - hammerTimeTimer > skillOneCD)
			return 0;
		else
			return (int)((skillOneCD+1) - (Time.time - hammerTimeTimer));;
	}
	public float getBombVoyageCD()
	{
		if(Time.time - bombVoyageTimer > skillTwoCD)
			return 0;
		else
			return (int)((skillTwoCD+1) - (Time.time - bombVoyageTimer));
	}
	
	public float getSkillOneCD() {
		return skillOneCD;
	}
	
	public float getSkillTwoCD() {
		return skillTwoCD;
	}
	
	public GameObject getAnimationObject() {
		return animation;
	}
	
	#region Cooldown Viewer Methods
	private void updateCDViewerPos() {
		CooldownViewer viewer = skillOneCDViewer.GetComponent<CooldownViewer>();
		viewer.updateCDViewerPosition(transform.position, true);
		viewer = skillTwoCDViewer.GetComponent<CooldownViewer>();
		viewer.updateCDViewerPosition(transform.position, false);
	}
	
	private void updateCDViewerColor() {
		
		CooldownViewer viewer = skillOneCDViewer.GetComponent<CooldownViewer>();
		viewer.updateCDViewerColor( (getHammerTimeCD() != 0) );
			
		viewer = skillTwoCDViewer.GetComponent<CooldownViewer>();
		viewer.updateCDViewerColor( (getBombVoyageCD() != 0) );
	}
	#endregion
}
