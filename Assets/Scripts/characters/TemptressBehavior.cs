using UnityEngine;
using System.Collections;

public class TemptressBehavior : MonoBehaviour {
	
	const string LURE_PREFAB_PATH = "Prefabs/Skill_Prefabs/Lure";
	const string LURE_BUTTON = "Fire1";
	const string LOVESTRUCK_BUTTON = "Fire2";
	
	private CharacterMovement characterMovement;
	private GameObject lureSkillPrefab;
	private GameObject currentLure;

	void Start () {
		loadSkills();
		loadScripts();
	}
	
	void Update () {
		if (currentLure == null) {
			checkLureButtonPress();
			return;
		}
		
		if (currentLure.GetComponent<Lure>().isComplete ()) {
			currentLure = null;
			return;
		}	
	}
	
	#region Initialization Methods
	private void loadSkills() {
		lureSkillPrefab = Resources.Load(LURE_PREFAB_PATH) as GameObject;
		if (lureSkillPrefab == null) {
			Debug.Log ("Lure skill loaded unsuccessfully");	
		}
		else {
			Debug.Log ("Lure skill loaded successfully");	
		}
	}
	
	private void loadScripts() {
		characterMovement = GetComponent<CharacterMovement>();
	}
	#endregion
	
	private void checkLureButtonPress() {
		if (Input.GetButtonDown(LURE_BUTTON)) {
			currentLure = Instantiate(lureSkillPrefab) as GameObject;
			currentLure.transform.position = this.transform.position;
			currentLure.transform.forward = characterMovement.getAimDirection();
		}
	}
}
