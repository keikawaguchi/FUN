// Character Template
// Used to show basic character script logic

using UnityEngine;
using System.Collections;

public class KiritoBehavior : MonoBehaviour {
	// skill cooldown times
	private const float suterusuCD = 5f;
	private const float chinmokuCD = 10f;
	
	// skill timers
	private float suterusuTimer = -99f;
	private float chinmokuTimer = -99f;
	
	private bool suterusuUsed = false;
	private float suterusuDuration = 0f;
	
	private CharacterMovement characterMovement;
	private Controller controller;
	
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
		
		if (suterusuUsed)  // keep a timer for invisible duration
			suterusuDuration += Time.smoothDeltaTime;
		
		if (suterusuDuration >= 3f) {  // visible after 3 sec
			Debug.Log("Visible!");
			gameObject.renderer.enabled = true;
			suterusuDuration = 0f;
			suterusuUsed = false;
		}
	}
	
	#region Initialization Methods
	private void loadSkills() {
		// lureSkillPrefab = Resources.Load(LURE_PREFAB_PATH) as GameObject;
	}
	
	private void loadScripts() {
		characterMovement = GetComponent<CharacterMovement>();
		controller = GetComponent<Controller>();
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
			gameObject.renderer.enabled = false;
			
			// keep track of cooldown timer
			suterusuTimer = Time.time;
			
			suterusuUsed = true;
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
