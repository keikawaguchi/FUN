using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
	
	private XInputController controller;

	// Use this for initialization
	void Start () {
		loadScripts();
	}
	
	// Update is called once per frame
	void Update () {
		if(controller.GetButtonPressed("Skill3")) {
			transform.position = new Vector3(-112.3f, 0, -79f);
		}
	}
	
	private void loadScripts() {
		controller = GetComponent<XInputController>();
	}
}
