using UnityEngine;
using System.Collections;

public class TemptressBehavior : MonoBehaviour {
	
	const string LURE_PREFAB_PATH = "Prefabs/Skills/Lure";
	
	private GameObject lureSkillPrefab;
	private GameObject currentLure;
	private LoveStruck loveStruck;
	
	private CharacterMovement characterMovement;
	private Controller controller;
	
	private const float lureCD = 5f;
	public float lureTimer = -99f;
	

	void Start () {
		loadSkills();
		loadScripts();
	}
	
	void Update () {
		if (isStunned()) {
			return;
		}
		
		if(Input.GetButtonDown(controller.getButton("Skill2"))) {
			LoveStruckButtonPress();
		}
		if (currentLure == null) {
			characterMovement.setMovementState(CharacterMovement.MovementState.CanMove);
			checkLureButtonPress();
			return;
		}
		else {
			characterMovement.setMovementState(CharacterMovement.MovementState.CannotMove);
		}
		
		if (currentLure.GetComponent<Lure>().isComplete ()) {
			currentLure = null;
			return;
		}
	}
	
	public int getCoolDown()
	{
		if(Time.time - lureTimer > lureCD)
		{
			return 0;
		}
		else
			return (int)((lureCD+1) - (Time.time - lureTimer));
	}
	
	#region Initialization Methods
	private void loadSkills() {
		lureSkillPrefab = Resources.Load(LURE_PREFAB_PATH) as GameObject;
		loveStruck = gameObject.AddComponent<LoveStruck>();
	}
	
	private void loadScripts() {
		characterMovement = GetComponent<CharacterMovement>();
		controller = GetComponent<Controller>();
	}
	#endregion
	
	private bool isStunned() {
		return characterMovement.getMovementState() == CharacterMovement.MovementState.Stunned;
	}
	
	private void checkLureButtonPress() {
		if (Input.GetButtonDown(controller.getButton("Skill1"))) {
			
			if (Time.time - lureTimer > lureCD) {
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
		}
	}
	
	private void LoveStruckButtonPress() {
		Debug.Log ("LoveStruck skill used");
		loveStruck.execute();	
	}
}
