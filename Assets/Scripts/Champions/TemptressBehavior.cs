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
	private const float loveStruckCD = 10f;
	
	public float lureTimer = -99f;
	public float loveStruckTimer = -99f;

	void Start () {
		loadResources();
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
	
	public int getLureCD() {
		if(Time.time - lureTimer > lureCD) {
			return 0;
		}
		else {
			return (int)((lureCD+1) - (Time.time - lureTimer));
		}
	}
	
	public int getLSCD() {
		if(Time.time - loveStruckTimer > loveStruckCD) {
			return 0;
		}
		else {
			return (int)((loveStruckCD+1) - (Time.time - loveStruckTimer));
		}
	}
	
	#region Initialization Methods
	private void loadResources() {
		lureSkillPrefab = Resources.Load(LURE_PREFAB_PATH) as GameObject;
	}
	
	private void loadScripts() {
		loveStruck = gameObject.AddComponent<LoveStruck>();
		characterMovement = GetComponent<CharacterMovement>();
		controller = GetComponent<Controller>();
	}
	#endregion
	
	private bool isStunned() {
		return characterMovement.isStunned();
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
		if (Time.time - loveStruckTimer > loveStruckCD) {
			Debug.Log ("LoveStruck skill used");
			loveStruck.execute();
			loveStruckTimer = Time.time;
		}
	}
}
