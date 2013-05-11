using UnityEngine;
using System.Collections;

public class LureUnit : MonoBehaviour {
	
	private bool canGrabPlayers;
	private GameObject grabbedPlayer;

	void Start () {
		canGrabPlayers = false;
	}
	
	void OnTriggerEnter(Collider collisionObject) {
		Debug.Log ("Lure collision!");
		if (collisionObject.GetComponent<Hero>() == null) {
			return;
		}
		
		string name = collisionObject.name;
		if (name == "Temptress") {
			grabbedPlayer = GameObject.Find("Temptress");
		}
	}
	
	public void setToGrabPlayers(bool val) {
		canGrabPlayers = val;
	}
	
	public GameObject getGrabbedPlayer() {
		return grabbedPlayer;
	}
}
