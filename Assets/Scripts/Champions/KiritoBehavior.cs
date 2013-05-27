// Character Template
// Used to show basic character script logic

using UnityEngine;
using System.Collections;

public class KiritoBehavior : MonoBehaviour {
	// skill cooldown times
	private const float suterusuCD = 5f;
	private const float skillTwoCD = 10f;
	
	// skill timers
	private float suterusuTimer = -99f;
	private float skillTwoTimer = -99f;
	
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
			checkSkillOneButtonPress();
		}
		
		if(Input.GetButtonDown(controller.getButton("Skill2"))) {
			checkSkillTwoButtonPress();
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
			
			
			// keep track of cooldown timer
			suterusuTimer = Time.time;
		}
	}
	
	private void checkSkillTwoButtonPress() {
		// check if cooldown expired
		if (Time.time - skillTwoTimer > skillTwoCD) {
			// skill 2 here
			Debug.Log("Skill Two Triggered!");
			
			
			// keep track of cooldown timer
			skillTwoTimer = Time.time;
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
	
	public int getSkillTwoCD()
	{
		if(Time.time - skillOneTimer > skillTwoCD)
			return 0;
		else
			return (int)((skillTwoCD + 1) - (Time.time - skillOneTimer));
	}
}
