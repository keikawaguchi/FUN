using UnityEngine;
using System.Collections;

public class Stun : MonoBehaviour {
	private const string STUN_BUTTON = "Jump";
	private const string TRAP_PREFAB_PATH = "Prefabs/Skills/Trap";
	
	private GameObject trapPrefab;
	private GameObject stunOwner;
	private GridSystem gridSystem;
	private Vector3 trapPostion;
	private bool isStun;

	// Use this for initialization
	void Start () {
		trapPrefab = Resources.Load (TRAP_PREFAB_PATH) as GameObject;
		GameObject mapObj = GameObject.Find ("Map");
		gridSystem = mapObj.GetComponent<GridSystem>();
		isStun = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isStun) {  // i don't get why i need this............. it instantiates a lot of stun
			GameObject instantiateTrap = Instantiate(trapPrefab) as GameObject;
			
			// place trap in closest grid position
			int xCoord = gridSystem.getXPos(trapPostion.x);
			int yCoord = gridSystem.getYPos(trapPostion.z);
			Vector3 trapPos = new Vector3(gridSystem.getXCoord(xCoord), 0, gridSystem.getYCoord(yCoord));
			instantiateTrap.transform.position = trapPos;
			isStun = true;
		}
	}
	
	public void SetStunOwner(GameObject owner) {
		stunOwner = owner;
		trapPostion = stunOwner.transform.position;
//		trapPrefab.GetComponent<Trap>().SetTrapOwner (stunOwner);
//		if (stunOwner == null)
//		Debug.Log ("Trap owner name: null");
	}
}
