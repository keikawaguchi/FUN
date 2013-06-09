using UnityEngine;
using System.Collections;

public class AlbionBehavior : MonoBehaviour {
	private const string CD_VIEWER_PREFAB_PATH = "Prefabs/Skills/CooldownViewer";
	
	private const string IDLE_TEXTURE_PATH = "Textures/SpriteSheets/Characters/Albion/AlbionIdleSpritesheet";
	private const string RUNNING_TEXTURE_PATH = "Textures/SpriteSheets/Characters/Albion/AlbionRunningSpriteSheet";
	
	private CharacterMovement characterMovement;
	private XInputController controller;
	private HolyTrap holyTrap;
	private HolyBlink holyBlink;
	private GameObject animation;
	
	// skill cooldown times
	private const float skillOneCD = 10f;  // holy trap
	private const float skillTwoCD = 15f;  // holy blink
	
	// skill timers
	private float skillOneTimer = -99f;
	private float skillTwoTimer = -99f;
	
	// view cooldown
	private GameObject skillOneCDPrefab;
	private GameObject skillOneCDViewer;
	private GameObject skillTwoCDPrefab;
	private GameObject skillTwoCDViewer;
	
	void Start () {
		LoadScripts();
		loadSkills();
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
		
		updateCDViewerPos();
		updateCDViewerColor();
		
		if (isSilence ()) {
			return;
		}
		
		if (controller.GetButtonPressed("Skill1")) {
			HolyTrapTriggered ();
		}
		if (controller.GetButtonPressed("Skill2")) {
			HolyBlinkTriggered ();
		}
	}
	
	private void LoadScripts() {
		characterMovement = GetComponent<CharacterMovement>();
		controller = GetComponent<XInputController>();
	}
	
	private void loadSkills() {
		skillOneCDPrefab = Resources.Load (CD_VIEWER_PREFAB_PATH) as GameObject;	
		skillTwoCDPrefab = Resources.Load (CD_VIEWER_PREFAB_PATH) as GameObject;	
	}
	
	private void loadAnimation() {	
		animation = Resources.Load ("Prefabs/Animations/Characters/Hunter") as GameObject;
		animation = Instantiate (animation) as GameObject;
		animation.GetComponent<Animation>().attachToObject (gameObject);
		
		Texture idle = Resources.Load (IDLE_TEXTURE_PATH) as Texture;
		Texture running = Resources.Load (RUNNING_TEXTURE_PATH) as Texture;
		animation.GetComponent<Animation>().setIdleTexture(idle);
		animation.GetComponent<Animation>().setRunningTexture(running);
	}
	
	private void HolyTrapTriggered() {
		if (Time.time - skillOneTimer > skillOneCD) {
			holyTrap = gameObject.AddComponent<HolyTrap>();
			holyTrap.SetTrapOwner(gameObject);
			
			skillOneTimer = Time.time;
		}
	}
	
	private void HolyBlinkTriggered() {
		if (Time.time - skillTwoTimer > skillTwoCD) {
			holyBlink = gameObject.AddComponent<HolyBlink>();
			holyBlink.SetOwner(gameObject);
		
			skillTwoTimer = Time.time;
		}
	}
	
	private bool isSilence() {
		return characterMovement.getIsSilence ();
	}
	
	public int gettrapCD()
	{
		if(Time.time - skillOneTimer > skillOneCD)
			return 0;
		else
			return (int)((skillOneCD+1) - (Time.time - skillOneTimer));
	}
	
	public int getblinkCD()
	{
		if(Time.time - skillTwoTimer > skillTwoCD)
			return 0;
		else
			return (int)((skillTwoCD+1) - (Time.time - skillTwoTimer));
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
		viewer.updateCDViewerColor( (gettrapCD() != 0) );
			
		viewer = skillTwoCDViewer.GetComponent<CooldownViewer>();
		viewer.updateCDViewerColor( (getblinkCD() != 0) );
	}
	#endregion
}
