// Character Template
// Used to show basic character script logic

using UnityEngine;
using System.Collections;

public class KiritoBehavior : MonoBehaviour {
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
	
	private CharacterMovement characterMovement;
	private Controller controller;
	private Hero hero;
	
	void Start () {
		loadSkills();
		loadScripts();
	}
	
	void Update () {
		if (isStunned()) {
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
	}
	
	private void loadScripts() {
		characterMovement = GetComponent<CharacterMovement>();
		controller = GetComponent<Controller>();
		hero = GetComponent<Hero>();
	}
	#endregion
	
	private bool isStunned() {
		return characterMovement.isStunned();
	}
	
	#region Character Skills
	private void suterusuTriggered() {
		// check if cooldown expired
		if (Time.time - suterusuTimer > suterusuCD) {
			// skill 1 here
			Debug.Log("Skill One Triggered!");
			GameObject suterusuObj = Instantiate (suterusuPrefab) as GameObject;
			suterusuObj.GetComponent<Suterusu>().setOwner (gameObject);
			
			// keep track of cooldown timer
			suterusuTimer = Time.time;
		}
	}
	
	private void chinmokuTriggered() {
		// check if cooldown expired
		if (Time.time - chinmokuTimer > chinmokuCD) {
			// skill 2 here
			Debug.Log("Skill Two Triggered!");
			
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
}
