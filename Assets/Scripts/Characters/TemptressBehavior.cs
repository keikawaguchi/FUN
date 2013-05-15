using UnityEngine;
using System.Collections;

public class TemptressBehavior : MonoBehaviour {
	
	const string LURE_PREFAB_PATH = "Prefabs/Skills/Lure";
	
	private CharacterMovement characterMovement;
	private Player1Controller controller;
	private GameObject lureSkillPrefab;
	private GameObject currentLure;

	void Start () {
		loadSkills();
		loadScripts();
	}
	
	void Update () {
		if(Input.GetButtonDown(controller.getButton("Skill2")))
		{
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
	
	#region Initialization Methods
	private void loadSkills() {
		lureSkillPrefab = Resources.Load(LURE_PREFAB_PATH) as GameObject;
	}
	
	private void loadScripts() {
		characterMovement = GetComponent<CharacterMovement>();
		controller = GetComponent<Player1Controller>();
	}
	#endregion
	
	private void checkLureButtonPress() {
		if (Input.GetButtonDown(controller.getButton("Skill1"))) {
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
		}
	}
	
	private void LoveStruckButtonPress() {
		Debug.Log ("LoveStruck skill used");
		Map map = GameObject.Find ("Map").GetComponent<Map>();
		GridSystem GS = GameObject.Find("Map").GetComponent<GridSystem>();
		GameObject temp;
		
		temp = map.getObjectAtGridLocation(GS.getXPos(transform.position.x), GS.getYPos(transform.position.z));
		if(temp != null && temp != gameObject && temp.tag == "Player")
			temp.GetComponent<CharacterMovement>().decreaseSpeedByPercentage(10);
		
		temp = map.getObjectAtGridLocation(GS.getXPos(transform.position.x)+1, GS.getYPos(transform.position.z));
		if(temp != null && temp != gameObject && temp.tag == "Player")
			temp.GetComponent<CharacterMovement>().decreaseSpeedByPercentage(10);
		
		temp = map.getObjectAtGridLocation(GS.getXPos(transform.position.x)+2, GS.getYPos(transform.position.z));
		if(temp != null && temp != gameObject && temp.tag == "Player")
			temp.GetComponent<CharacterMovement>().decreaseSpeedByPercentage(10);
		
		temp = map.getObjectAtGridLocation(GS.getXPos(transform.position.x)-1, GS.getYPos(transform.position.z));
		if(temp != null && temp != gameObject && temp.tag == "Player")
			temp.GetComponent<CharacterMovement>().decreaseSpeedByPercentage(10);
		
		temp = map.getObjectAtGridLocation(GS.getXPos(transform.position.x)-2, GS.getYPos(transform.position.z));
		if(temp != null && temp != gameObject && temp.tag == "Player")
			temp.GetComponent<CharacterMovement>().decreaseSpeedByPercentage(10);
		
		temp = map.getObjectAtGridLocation(GS.getXPos(transform.position.x), GS.getYPos(transform.position.z)+1);
		if(temp != null && temp != gameObject && temp.tag == "Player")
			temp.GetComponent<CharacterMovement>().decreaseSpeedByPercentage(10);
		
		temp = map.getObjectAtGridLocation(GS.getXPos(transform.position.x), GS.getYPos(transform.position.z)+2);
		if(temp != null && temp != gameObject && temp.tag == "Player")
			temp.GetComponent<CharacterMovement>().decreaseSpeedByPercentage(10);
		
		temp = map.getObjectAtGridLocation(GS.getXPos(transform.position.x), GS.getYPos(transform.position.z)-1);
		if(temp != null && temp != gameObject && temp.tag == "Player")
			temp.GetComponent<CharacterMovement>().decreaseSpeedByPercentage(10);
		
		temp = map.getObjectAtGridLocation(GS.getXPos(transform.position.x), GS.getYPos(transform.position.z)-2);
		if(temp != null && temp != gameObject && temp.tag == "Player")
			temp.GetComponent<CharacterMovement>().decreaseSpeedByPercentage(20);	
	}
}