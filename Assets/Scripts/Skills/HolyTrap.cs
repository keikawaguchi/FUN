using UnityEngine;
using System.Collections;

public class HolyTrap : MonoBehaviour {
	private const string HOLYTRAP_PREFAB_PATH = "Prefabs/Skills/HolyTrap";
	
	private GameObject holyTrapPrefab;
	private GameObject owner;
	private GridSystem gridSystem;
	private Vector3 trapPostion;
	private bool isStun;
	
	private float trapDuration = 15f;

	// Use this for initialization
	void Start () {
		holyTrapPrefab = Resources.Load (HOLYTRAP_PREFAB_PATH) as GameObject;
		GameObject mapObj = GameObject.Find ("Map");
		gridSystem = mapObj.GetComponent<GridSystem>();
		isStun = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isStun) {  // i don't get why i need this............. it instantiates a lot of stun
			GameObject instantiateTrap = Instantiate(holyTrapPrefab) as GameObject;
			
			// place trap in closest grid position
			int xCoord = gridSystem.getXPos(trapPostion.x);
			int yCoord = gridSystem.getYPos(trapPostion.z);
			Vector3 trapPos = new Vector3(gridSystem.getXCoord(xCoord), 0, gridSystem.getYCoord(yCoord));
			instantiateTrap.transform.position = trapPos;
			instantiateTrap.GetComponent<HolyTrapUnit>().SetTrapOwner(owner);
			isStun = true;
			
			Destroy (instantiateTrap, trapDuration);
		}
	}
	
	public void SetTrapOwner(GameObject owner) {
		this.owner = owner;
		trapPostion = owner.transform.position;
	}
}
