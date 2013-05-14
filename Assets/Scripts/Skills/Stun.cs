using UnityEngine;
using System.Collections;

public class Stun : MonoBehaviour {
	private const string STUN_BUTTON = "Jump";
	private const string TRAP_PREFAB_PATH = "Prefabs/Skills/Trap";
	 
	private GameObject trap;
	private GameObject heroObj;
	private GridSystem gridSystem;

	// Use this for initialization
	void Start () {
		trap = Resources.Load (TRAP_PREFAB_PATH) as GameObject;
		heroObj = GameObject.Find ("Hero");
		GameObject mapObj = GameObject.Find ("Map");
		gridSystem = mapObj.GetComponent<GridSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown(STUN_BUTTON)) {
			GameObject instantiateTrap = Instantiate(trap) as GameObject;
			
			// place trap in closest grid position
			int xCoord = gridSystem.getXPos(heroObj.transform.position.x);
			int yCoord = gridSystem.getYPos(heroObj.transform.position.z);
			
			Vector3 trapPos = new Vector3(gridSystem.getXCoord(xCoord), 0, gridSystem.getYCoord(yCoord));
			instantiateTrap.transform.position = trapPos;
		}
	}
}
