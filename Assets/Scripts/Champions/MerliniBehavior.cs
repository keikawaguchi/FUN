using UnityEngine;
using System.Collections;

public class MerliniBehavior : MonoBehaviour {
	
	private CharacterMovement characterMovement;
	private Controller controller;
	
	// skill cooldown times
	private const float skillOneCD = 1f;
	private const float skillTwoCD = 1f;
	
	// skill timers
	private float skillOneTimer = -99f;
	private float skillTwoTimer = -99f;
	

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
	
	#region Merlini Skills
	private void checkSkillOneButtonPress() {
		
		if (Input.GetButtonDown(controller.getButton("Skill1"))) {
			
			if (Time.time - skillOneTimer > skillOneCD) {
				// skill 1 here
				Debug.Log("Skill One Triggered!");
				
				skillOneTimer = Time.time;
			
			}
		}
	}
	
	private void checkSkillTwoButtonPress() {
		
		if (Input.GetButtonDown(controller.getButton("Skill2"))) {
			
			if (Time.time - skillTwoTimer > skillTwoCD) {
				// skill 1 here
				Debug.Log("Skill Two Triggered!");
				
				skillTwoTimer = Time.time;
			
			}
		}
	}
	#endregion
}
