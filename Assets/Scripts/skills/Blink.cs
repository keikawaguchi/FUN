using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour {
	private const string BLINK_BUTTON = "Jump";
	private const float TELEPORT_DISTANCE = 14f * 3f;
	
	private GameObject heroObj;
	private GameObject globalBehviorObject;

	// Use this for initialization
	void Start () {
		heroObj = GameObject.Find ("Hero");
		globalBehviorObject = GameObject.Find ("Global Behavior");
		Bounds worldBound = globalBehviorObject.GetComponent<GlobalBehavior>().WorldBound;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		 * Conditions:
		 * 1. Out of bound
		 * 2. Jump onto walls
		 * 3. Jump onto bombs
		 */
		
		if (Input.GetButtonDown(BLINK_BUTTON)) {
			
			heroObj.transform.position += heroObj.transform.forward * TELEPORT_DISTANCE;  // teleport to facing direction
		}
	}
}
