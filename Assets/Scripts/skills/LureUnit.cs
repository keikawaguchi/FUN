using UnityEngine;
using System.Collections;

public class LureUnit : MonoBehaviour {
	
	private bool canGrabPlayers;
	private GameObject grabbedPlayer;

	void Start () {
		canGrabPlayers = false;
	}
	
	void OnTriggerEnter(Collider collisionObject) {
		if (collisionObject.GetComponent<Hero>() == null) {
			return;
		}
		
		grabbedPlayer = collisionObject.gameObject;
		if (grabbedPlayer != null) {
			Debug.Log("LureUnit: Hooked to player");
		}
	}
	
	public void setToGrabPlayers(bool val) {
		canGrabPlayers = val;
	}
	
	public GameObject getGrabbedPlayer() {
		return grabbedPlayer;
	}
}
