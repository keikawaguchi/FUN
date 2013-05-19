// Character Template
// Used to show basic character script logic

using UnityEngine;
using System.Collections;

public class Template : MonoBehaviour {
	// skill cooldown times
	private const float skillOneCD = 1f;
	private const float skillTwoCD = 1f;
	
	// skill timers
	private float skillOneTimer = -99f;
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
		return characterMovement.getMovementState() == CharacterMovement.MovementState.Stunned;
	}
	
	#region Character Skills
	private void checkSkillOneButtonPress() {
		// check if cooldown expired
		if (Time.time - skillOneTimer > skillOneCD) {
			// skill 1 here
			Debug.Log("Skill One Triggered!");
			
			
			// keep track of cooldown timer
			skillOneTimer = Time.time;
		}
	}
	
	private void checkSkillTwoButtonPress() {
		// check if cooldown expired
		if (Time.time - skillTwoTimer > skillTwoCD) {
			// skill 1 here
			Debug.Log("Skill Two Triggered!");
			
			
			// keep track of cooldown timer
			skillTwoTimer = Time.time;
		}
	}
	#endregion
}
