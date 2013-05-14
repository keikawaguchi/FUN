using UnityEngine;
using System.Collections;

public class Stun : MonoBehaviour {
	private const string STUN_BUTTON = "Jump";
	private const string TRAP_PREFAB_PATH = "Prefabs/Skills/Trap";
	
	private GameObject trap;
	private GridSystem gridSystem;
	private Vector3 trapPostion;
	private bool isStun;

	// Use this for initialization
	void Start () {
		trap = Resources.Load (TRAP_PREFAB_PATH) as GameObject;
		GameObject mapObj = GameObject.Find ("Map");
		gridSystem = mapObj.GetComponent<GridSystem>();
		isStun = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isStun) {  // i don't get why i need this............. it instantiates a lot of stun
			GameObject instantiateTrap = Instantiate(trap) as GameObject;
			
			// place trap in closest grid position
			int xCoord = gridSystem.getXPos(trapPostion.x);
			int yCoord = gridSystem.getYPos(trapPostion.z);
			Vector3 trapPos = new Vector3(gridSystem.getXCoord(xCoord), 0, gridSystem.getYCoord(yCoord));
			instantiateTrap.transform.position = trapPos;
			isStun = true;
		}
	}
	
	public void SetTrapPosition(Vector3 position) {
		trapPostion = position;
	}
}
