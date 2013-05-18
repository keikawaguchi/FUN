using UnityEngine;
using System.Collections;

public class MerliniBehavior : MonoBehaviour {
	private const string HAMMERTIME_PREFAB_PATH = "Prefabs/Skills/HammerTime";
	
	private CharacterMovement characterMovement;
	private Controller controller;
	
	private GameObject hammerPrefab;
	private GameObject hammer;
	
	// skill cooldown times
	private const float hammerTimeCD = 1f;
	private const float skillTwoCD = 1f;
	
	// skill timers
	private float hammerTimeTimer = -99f;
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
			hammerTimeButtonPress();
		}
		
		if(Input.GetButtonDown(controller.getButton("Skill2"))) {
			checkSkillTwoButtonPress();
		}
	}
	
	#region Initialization Methods
	private void loadSkills() {
		hammerPrefab = Resources.Load (HAMMERTIME_PREFAB_PATH) as GameObject;
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
	private void hammerTimeButtonPress() {
		
		// check if cooldown expired
		if (Time.time - hammerTimeTimer > hammerTimeCD) {

			Debug.Log("It's hammer time!!");
			hammer = Instantiate (hammerPrefab) as GameObject;
			hammer.GetComponent<HammerTime>().SetStunOwner (gameObject);
			hammer.transform.position = transform.position;
			hammer.transform.forward = characterMovement.getAimDirection();
			
			// keep track of cooldown timer
			hammerTimeTimer = Time.time;
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
