using UnityEngine;
using System.Collections;

public class IceAge : MonoBehaviour {
	private const string ICEAGE_PREFAB_PATH = "Prefabs/Skills/IceAge";
	private const float ICE_AGE_DURATION = 3f;
	
	private GameObject owner;
	private GameObject iceAgePrefab;
	private GameObject iceAgeObj;
	private IndestructubleWall iceAge;
	private Map map;
	private GridSystem gridSystem;
	private CharacterMovement characterMovement;
	private Controller controller;
	private float singleGridSize;
	
	// Use this for initialization
	void Start () {
		loadScripts ();
		
		singleGridSize = gridSystem.getSingleGridWidth();
	}
	
	// Update is called once per frame
	void Update () {
		iceAgePrefab = Resources.Load (ICEAGE_PREFAB_PATH) as GameObject;
		iceAgeObj = Instantiate (iceAgePrefab) as GameObject;
		
		if(Input.GetButtonDown(controller.getButton("Skill2")))
			constructIceAge();
		
		Destroy (iceAgeObj, 2f);  // destroy the ice wall after 2 seconds
	}
	
	private void loadScripts() {
		GameObject mapObj = GameObject.Find ("Map");
		
		map = mapObj.GetComponent<Map>();
		gridSystem = mapObj.GetComponent<GridSystem>();
		characterMovement = GetComponent<CharacterMovement>();
		controller = GetComponent<Controller>();
	}
	
	private void constructIceAge() {
		Vector3 iceAgePos = owner.transform.position;
		Vector3 aimDirection = characterMovement.getAimDirection ();
		
		if (aimDirection.x > 0)
			aimDirection.x = 1;
		if (aimDirection.x < 0)
			aimDirection.x = -1;
		if (aimDirection.z > 0)
			aimDirection.z = 1;
		if (aimDirection.z  < 0)
			aimDirection.z = -1;
		
		iceAgePos += aimDirection * singleGridSize;
		int iceAgePosX = gridSystem.getXPos(iceAgePos.x);
		int iceAgePosY = gridSystem.getYPos(iceAgePos.z);
		
		if (!map.isGridFull (iceAgePosX, iceAgePosY)) {
			map.addImpassableObject (iceAgePosX, iceAgePosY, iceAgeObj);
			iceAgeObj.GetComponent<IndestructubleWall>().initialize(gridSystem.getXCoord(iceAgePosX), gridSystem.getYCoord(iceAgePosY));
		}
	}
	
	public void setOwner(GameObject owner) {
		this.owner = owner;
	}
	
	public float getIceAgeDuration() {
		return ICE_AGE_DURATION;
	}
}
