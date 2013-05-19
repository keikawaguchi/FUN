// Character Template
// Used to show basic character script logic

using UnityEngine;
using System.Collections;

public class FanndisBehavior : MonoBehaviour {
	// skill cooldown times
	private const float zeroFrictionCD = 1f;  // ZeroFriction
	private const float iceAgeCD = 1f;  // IceAge
	
	// skill timers
	private float zeroFrictionTimer = -99f;
	private float iceAgeTimer = -99f;
		
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
			zeroFictionTriggered();
		}
		
		if(Input.GetButtonDown(controller.getButton("Skill2"))) {
			iceAgeTriggered();
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
	private void zeroFictionTriggered() {
		
		if (Input.GetButtonDown(controller.getButton("Skill1"))) {
			
			if (Time.time - zeroFrictionTimer > zeroFrictionCD) {
				// skill 1 here
				Debug.Log("Skill One Triggered!");
				
				
				zeroFrictionTimer = Time.time;
			
			}
		}
	}
	
	private void iceAgeTriggered() {
		
		if (Input.GetButtonDown(controller.getButton("Skill2"))) {
			
			if (Time.time - iceAgeTimer > iceAgeCD) {
				// skill 1 here
				Debug.Log("Skill Two Triggered!");
				
				iceAgeTimer = Time.time;
			
			}
		}
	}
	#endregion
}
