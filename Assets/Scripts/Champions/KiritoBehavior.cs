// Character Template
// Used to show basic character script logic

using UnityEngine;
using System.Collections;

public class KiritoBehavior : MonoBehaviour {
	private const string CD_VIEWER_PREFAB_PATH = "Prefabs/Skills/CooldownViewer";
	private const string SUTERUSU_PREFAB_PATH = "Prefabs/Skills/Suterusu";
	private const string CHINMOKU_PREFAB_PATH = "Prefabs/Skills/Chinmoku";
	
	// animation sprite sheets
	private const string IDLE_TEXTURE_PATH = "Textures/SpriteSheets/Characters/Ninja/NinjaIdleSpritesheet";
	private const string RUNNING_TEXTURE_PATH = "Textures/SpriteSheets/Characters/Ninja/NinjaRunningSpritesheet";
	
	private GameObject suterusuPrefab;
	private GameObject chinmokuPrefab;
	
	// skill cooldown times
	private const float skillOneCD = 10f;  // suterusu
	private const float skillTwoCD = 15f;  // chinmoku
	
	// skill timers
	private float suterusuTimer = -99f;
	private float chinmokuTimer = -99f;
	
	private bool suterusuUsed = false;
	private float suterusuDuration = 0f;
	
	private GameObject animation;
	private CharacterMovement characterMovement;
	private XInputController controller;
	private Hero hero;
	
	// view cooldown
	private GameObject skillOneCDPrefab;
	private GameObject skillOneCDViewer;
	private GameObject skillTwoCDPrefab;
	private GameObject skillTwoCDViewer;
	private bool viewerVisible = true;
	
	
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
		updateViewerVisibility();
		
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
			suterusuTriggered();
		}
		
		if(controller.GetButtonPressed("Skill2")) {
			chinmokuTriggered();
		}
	}
	
	#region Initialization Methods
	private void loadSkills() {
		suterusuPrefab = Resources.Load (SUTERUSU_PREFAB_PATH) as GameObject;
		chinmokuPrefab = Resources.Load (CHINMOKU_PREFAB_PATH) as GameObject;
		skillOneCDPrefab = Resources.Load (CD_VIEWER_PREFAB_PATH) as GameObject;	
		skillTwoCDPrefab = Resources.Load (CD_VIEWER_PREFAB_PATH) as GameObject;
	}
	
	private void loadScripts() {
		characterMovement = GetComponent<CharacterMovement>();
		controller = GetComponent<XInputController>();
		hero = GetComponent<Hero>();
	}
	
	 private void loadAnimation() {	
		animation = Resources.Load ("Prefabs/Animations/Characters/Ninja") as GameObject;
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
	
	#region Character Skills
	private void suterusuTriggered() {
		// check if cooldown expired
		if (Time.time - suterusuTimer > skillOneCD) {
			// skill 1 here
			// Debug.Log("Skill One Triggered!");
			GameObject suterusuObj = Instantiate (suterusuPrefab) as GameObject;
			suterusuObj.GetComponent<Suterusu>().setOwner (gameObject);
			suterusuObj.GetComponent<Suterusu>().setAnimationScript(animation.GetComponent<Animation>());
			
			// keep track of cooldown timer
			suterusuTimer = Time.time;
		}
	}
	
	private void chinmokuTriggered() {
		Chinmoku chinmoku;
		// check if cooldown expired
		if (Time.time - chinmokuTimer > skillTwoCD) {
			// skill 2 here
			// Debug.Log("Skill Two Triggered!");
//			GameObject chinmokuObj = Instantiate (chinmokuPrefab) as GameObject;
//			chinmokuObj.GetComponent<Chinmoku>().setOwner (gameObject);
//			chinmokuObj.GetComponent<Chinmoku>().execute ();
			chinmoku = gameObject.AddComponent<Chinmoku>();
			chinmoku.execute ();
			
			// keep track of cooldown timer
			chinmokuTimer = Time.time;
		}
	}
	#endregion
	
	public int getSuterusuCD()
	{
		if(Time.time - suterusuTimer > skillOneCD)
			return 0;
		else
			return (int)((skillOneCD + 1) - (Time.time - suterusuTimer));
	}
	
	public int getChinmokuCD()
	{
		if(Time.time - chinmokuTimer > skillTwoCD)
			return 0;
		else
			return (int)((skillTwoCD + 1) - (Time.time - chinmokuTimer));
	}
	
	public GameObject getAnimationObject() {
		return animation;
	}
	
	public float getSkillOneCD() {
		return skillOneCD;
	}
	
	public float getSkillTwoCD() {
		return skillTwoCD;
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
		viewer.updateCDViewerColor( (getSuterusuCD() != 0) );
			
		viewer = skillTwoCDViewer.GetComponent<CooldownViewer>();
		viewer.updateCDViewerColor( (getChinmokuCD() != 0) );
	}
	
	private void updateViewerVisibility() {
		if (viewerVisible) {
			CooldownViewer viewer = skillOneCDViewer.GetComponent<CooldownViewer>();
			viewer.setVisibility (true);
			
			viewer = skillTwoCDViewer.GetComponent<CooldownViewer>();
			viewer.setVisibility (true);
		}
		else {
			CooldownViewer viewer = skillOneCDViewer.GetComponent<CooldownViewer>();
			viewer.setVisibility (false);
			
			viewer = skillTwoCDViewer.GetComponent<CooldownViewer>();
			viewer.setVisibility (false);
		}
	}
	
	public void setViewerVisibility(bool visible) {
		viewerVisible = visible;
	}
	#endregion
}
