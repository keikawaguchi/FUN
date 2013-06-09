using UnityEngine;
using System.Collections;

public class TemptressBehavior : MonoBehaviour {
	private const string CD_VIEWER_PREFAB_PATH = "Prefabs/Skills/CooldownViewer";
	const string LURE_PREFAB_PATH = "Prefabs/Skills/Lure";
	
	private const string IDLE_TEXTURE_PATH = "Textures/SpriteSheets/Characters/Temptress/TemptressIdleSpritesheet";
	private const string RUNNING_TEXTURE_PATH = "Textures/SpriteSheets/Characters/Temptress/TemptressRunningSpritesheet";
	
	private GameObject lureSkillPrefab;
	private GameObject currentLure;
	private LoveStruck loveStruck;
	private GameObject animation;
	
	private CharacterMovement characterMovement;
	private XInputController controller;
	
	private const float skillOneCD = 5f;  // lure
	private const float skillTwoCD = 10f;  // love struck
	
	public float lureTimer = -99f;
	public float loveStruckTimer = -99f;
	
	// view cooldown
	private GameObject skillOneCDPrefab;
	private GameObject skillOneCDViewer;
	private GameObject skillTwoCDPrefab;
	private GameObject skillTwoCDViewer;
	
	void Start () {
		loadResources();
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
		
		if (controller.GetButtonPressed("Skill1")) {
			checkLureButtonPress();
		}
		
		if(controller.GetButtonPressed("Skill2")) {
			LoveStruckButtonPress();
		}
		
		updateLure();
	}
	
	public int getLureCD() {
		if(Time.time - lureTimer > skillOneCD) {
			return 0;
		}
		else {
			return (int)((skillOneCD+1) - (Time.time - lureTimer));
		}
	}

	public int getLSCD() {
		if(Time.time - loveStruckTimer > skillTwoCD) {
			return 0;
		}
		else {
			return (int)((skillTwoCD+1) - (Time.time - loveStruckTimer));
		}
	}
	
	public float getSkillOneCD() {
		return skillOneCD;
	}
	
	public float getSkillTwoCD() {
		return skillTwoCD;
	}
	
	#region Initialization Methods
	private void loadResources() {
		lureSkillPrefab = Resources.Load(LURE_PREFAB_PATH) as GameObject;
		
		skillOneCDPrefab = Resources.Load (CD_VIEWER_PREFAB_PATH) as GameObject;	
		skillTwoCDPrefab = Resources.Load (CD_VIEWER_PREFAB_PATH) as GameObject;	
	}
	
	private void loadScripts() {
		loveStruck = gameObject.AddComponent<LoveStruck>();
		characterMovement = GetComponent<CharacterMovement>();
		controller = GetComponent<XInputController>();
	}
	
	private void loadAnimation() {	
		animation = Resources.Load ("Prefabs/Animations/Characters/Temptress") as GameObject;
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
	
	private void checkLureButtonPress() {
		if (currentLure != null) {
			return;	
		}
		if (Time.time - lureTimer < skillOneCD) {
			return;
		}
		
		characterMovement.setMovementState(CharacterMovement.MovementState.CannotMove);
		
		float playerHeight = this.transform.localScale.z;
		Vector3 aimDirection = characterMovement.getAimDirection();
		if (aimDirection == new Vector3(0, 0, 0)) {
			aimDirection = new Vector3(0, 0, 1);
		}
		
		currentLure = Instantiate(lureSkillPrefab) as GameObject;
		currentLure.GetComponent<Lure>().setLureOwner(gameObject);
		currentLure.GetComponent<Lure>().setPullToLocation(transform.position);
		currentLure.transform.position = this.transform.position;
		currentLure.transform.position += aimDirection * playerHeight;
		
		currentLure.transform.forward = characterMovement.getAimDirection();
	
		lureTimer = Time.time;
	}
	
	private void updateLure() {
		if (currentLure == null) {
			return;
		}
		
		if (currentLure.GetComponent<Lure>().isComplete()) {
			currentLure = null;
			characterMovement.setMovementState (CharacterMovement.MovementState.CanMove);
		}
	}
	
	private void LoveStruckButtonPress() {
		if (Time.time - loveStruckTimer > skillTwoCD) {
			Debug.Log ("LoveStruck skill used");
			loveStruck.execute();
			loveStruckTimer = Time.time;
		}
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
		viewer.updateCDViewerColor( (getLureCD() != 0) );
			
		viewer = skillTwoCDViewer.GetComponent<CooldownViewer>();
		viewer.updateCDViewerColor( (getLSCD() != 0) );
	}
	#endregion
}
