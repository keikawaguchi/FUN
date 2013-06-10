using UnityEngine;
using System.Collections;

public class CreditsMenu: MonoBehaviour {
	
	XInputController controller;

	void Start () {
		controller = GetComponent<XInputController>();
	}
	
	void Update () {
		if (controller.GetButtonPressed("b")) {
			Application.LoadLevel("Intro");
		}
	}
}
