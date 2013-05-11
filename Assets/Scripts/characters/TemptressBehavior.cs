using UnityEngine;
using System.Collections;

public class TemptressBehavior : MonoBehaviour {
	
	const string LURE_PREFAB_PATH = "Prefabs/Skill_Prefabs/Lure";
	const string LURE_BUTTON = "Fire1";
	const string LOVESTRUCK_BUTTON = "Fire2";
	
	private GameObject lureSkill;

	void Start () {
		loadSkills();
	}
	
	void Update () {
		if (Input.GetButtonDown(LURE_BUTTON)) {
			GameObject newLure = Instantiate(lureSkill) as GameObject;
			newLure.transform.position = this.transform.position;
			newLure.transform.forward = this.transform.forward;
		}
	}
	
	private void loadSkills() {
		lureSkill = Resources.Load(LURE_PREFAB_PATH) as GameObject;
		if (lureSkill == null) {
			Debug.Log ("Lure skill loaded unsuccessfully");	
		}
		else {
			Debug.Log ("Lure skill loaded successfully");	
		}
	}
}
