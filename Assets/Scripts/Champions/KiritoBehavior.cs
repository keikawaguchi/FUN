// Character Template
// Used to show basic character script logic

using UnityEngine;
using System.Collections;

public class KiritoBehavior : MonoBehaviour {
	private const string CD_VIEWER_PREFAB_PATH = "Prefabs/Skills/CooldownViewer";
	private const string SUTERUSU_PREFAB_PATH = "Prefabs/Skills/Suterusu";
	
	private GameObject suterusuPrefab;
	
	// skill cooldown times
	private const float suterusuCD = 10f;
	private const float chinmokuCD = 15f;
	
	// skill timers
	private float suterusuTimer = -99f;
	private float chinmokuTimer = -99f;
	
	private bool suterusuUsed = false;
	private float suterusuDuration = 0f;
	
	private GameObject animation;
	private CharacterMovement characterMovement;
	private Controller controller;
	private Hero hero;
	
	// view cooldown
	private GameObject skillOneCD;
	private GameObject skillOneCDViewer;
	private GameObject skillTwoCD;
	private GameObject skillTwoCDViewer;
	
	
	void Start () {
		loadSkills();
		loadScripts();
		loadAnimation();
		
		skillOneCDViewer = Instantiate(skillOneCD) as GameObject;
		skillTwoCDViewer = Instantiate(skillTwoCD) as GameObject;
	}
	
	void Update () {
		updateCDViewerPos();
		updateCDViewerColor();
		
		if (isStunned()) {
			return;
		}
		
		if (isSilence ()) {
			return;
		}
		
		if(Input.GetButtonDown(controller.getButton("Skill1"))) {
			suterusuTriggered();
		}
		
		if(Input.GetButtonDown(controller.getButton("Skill2"))) {
			chinmokuTriggered();
		}
	}
	
	#region Initialization Methods
	private void loadSkills() {
		suterusuPrefab = Resources.Load (SUTERUSU_PREFAB_PATH) as GameObject;
		skillOneCD = Resources.Load (CD_VIEWER_PREFAB_PATH) as GameObject;	
		skillTwoCD = Resources.Load (CD_VIEWER_PREFAB_PATH) as GameObject;
	}
	
	private void loadScripts() {
		characterMovement = GetComponent<CharacterMovement>();
		controller = GetComponent<Controller>();
		hero = GetComponent<Hero>();
	}
	
	 private void loadAnimation() {	
		animation = Resources.Load ("Prefabs/Animations/Characters/Ninja") as GameObject;
		animation = Instantiate (animation) as GameObject;
		animation.GetComponent<Animation>().attachToObject (gameObject);
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
		if (Time.time - suterusuTimer > suterusuCD) {
			// skill 1 here
			Debug.Log("Skill One Triggered!");
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
		if (Time.time - chinmokuTimer > chinmokuCD) {
			// skill 2 here
			Debug.Log("Skill Two Triggered!");
			chinmoku = gameObject.AddComponent<Chinmoku>();
			chinmoku.execute ();
			
			// keep track of cooldown timer
			chinmokuTimer = Time.time;
		}
	}
	#endregion
	
	public int getSuterusuCD()
	{
		if(Time.time - suterusuTimer > suterusuCD)
			return 0;
		else
			return (int)((suterusuCD + 1) - (Time.time - suterusuTimer));
	}
	
	public int getChinmokuCD()
	{
		if(Time.time - chinmokuTimer > chinmokuCD)
			return 0;
		else
			return (int)((chinmokuCD + 1) - (Time.time - chinmokuTimer));
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
		viewer.updateCDViewerColor( (getSuterusuCD() != 0) );
			
		viewer = skillTwoCDViewer.GetComponent<CooldownViewer>();
		viewer.updateCDViewerColor( (getChinmokuCD() != 0) );
	}
	#endregion
}
