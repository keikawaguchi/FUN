using UnityEngine;
using System.Collections;

public class Stun : MonoBehaviour {
	private const string STUN_BUTTON = "Jump";
	private const string TRAP_PREFAB_PATH = "Prefabs/Trap";
	
	private GlobalBehavior globalBehavior;
	private GameObject heroObj; 
	private GameObject trap;

	// Use this for initialization
	void Start () {
		trap = Resources.Load (TRAP_PREFAB_PATH) as GameObject;
		heroObj = GameObject.Find ("Hero");
		GameObject gbObj = GameObject.Find ("Global Behavior");
		globalBehavior = gbObj.GetComponent<GlobalBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown(STUN_BUTTON)) {
			GameObject instantiateTrap = Instantiate(trap) as GameObject;
			
			// place trap in closest grid position
			int xCoord = globalBehavior.getXPos(heroObj.transform.position.x);
			int yCoord = globalBehavior.getYPos(heroObj.transform.position.z);
			
			Vector3 trapPos = new Vector3(globalBehavior.getXCoord(xCoord), 0, globalBehavior.getYCoord(yCoord));
			instantiateTrap.transform.position = trapPos;
		}
	}
}
