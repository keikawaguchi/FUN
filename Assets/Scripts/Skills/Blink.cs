using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour {
	private const string BLINK_BUTTON = "Jump";
	private const float TELEPORT_DISTANCE = 14f * 3f;
	
	private GameObject heroObj;
//	private GlobalBehavior globalBehavior;
	private GridMove grid;

	// Use this for initialization
	void Start () {
		heroObj = GameObject.Find ("Hero");
//		GameObject globalBehviorObject = globalBehviorObject = GameObject.Find ("Global Behavior");
//		globalBehavior = globalBehviorObject.GetComponent<GlobalBehavior>();
//		GameObject gridMoveObj = GameObject.Find ();
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
			Vector3 heroPos = heroObj.transform.position;
//			if (globalBehavior.getXPos(heroPos) > 
				heroObj.transform.position += heroObj.transform.forward * TELEPORT_DISTANCE;  // teleport to facing direction
		}
	}
}