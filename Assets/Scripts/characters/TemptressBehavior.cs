using UnityEngine;
using System.Collections;

public class TemptressBehavior : MonoBehaviour {
	
	private Lure lureSkill;

	void Start () {

	}
	
	void Update () {
	
	}
	
	private void loadSkills() {
		lureSkill = GameObject.Find("Lure").GetComponent<Lure>();
		if (lureSkill == null) {
			Debug.Log ("Lure skill loaded unsuccessfully");	
		}
		else {
			Debug.Log ("Lure skill loaded successfully");	
		}
	}
}
