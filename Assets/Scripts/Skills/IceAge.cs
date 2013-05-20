using UnityEngine;
using System.Collections;

public class IceAge : MonoBehaviour {
	private const string ICEAGE_PREFAB_PATH = "Prefabs/Skills/IceAge";
	private const float ICE_AGE_DURATION = 3f;
	
<<<<<<< HEAD
	private GameObject owner;
=======
	private GameObject iceAgePrefab;
>>>>>>> 1208997d4835d98a7d984da0b930cbc34f0928b7
	private GameObject iceAgeObj;
	private GameObject iceAgePrefab;
	private IndestructubleWall iceAge;
	private Map map;
	private GridSystem gridSystem;
	private CharacterMovement characterMovement;
	private float singleGridSize;
	
	// Use this for initialization
	void Start () {
<<<<<<< HEAD
		loadScripts ();
		
		singleGridSize = gridSystem.getSingleGridWidth();
=======
		iceAgePrefab =  Resources.Load(ICEAGE_PREGAB_PATH) as GameObject;
>>>>>>> 1208997d4835d98a7d984da0b930cbc34f0928b7
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		iceAgePrefab = Resources.Load (ICEAGE_PREFAB_PATH) as GameObject;
		iceAgeObj = Instantiate (iceAgePrefab) as GameObject;
		
		constructIceAge();
		Destroy (iceAgeObj, 2f);
	}
	
	private void loadScripts() {
		GameObject mapObj = GameObject.Find ("Map");
		
		map = mapObj.GetComponent<Map>();
		gridSystem = mapObj.GetComponent<GridSystem>();
		characterMovement = GetComponent<CharacterMovement>();
	}
	
	private void constructIceAge() {
		Vector3 iceAgePos = owner.transform.position;
		Vector3 aimDirection = characterMovement.getAimDirection ();
		aimDirection.x += singleGridSize;
		
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
		
		iceAgeObj.GetComponent<IndestructubleWall>().initialize(gridSystem.getXCoord(iceAgePosX), gridSystem.getYCoord(iceAgePosY));
	}
	
	public void setOwner(GameObject owner) {
		this.owner = owner;
	}
	
	public float getIceAgeDuration() {
		return ICE_AGE_DURATION;
=======
		iceAgeObj = Instantiate(iceAgePrefab) as GameObject;	
>>>>>>> 1208997d4835d98a7d984da0b930cbc34f0928b7
	}
}
