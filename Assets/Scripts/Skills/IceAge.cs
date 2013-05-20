using UnityEngine;
using System.Collections;

public class IceAge : MonoBehaviour {
	private const string ICEAGE_PREGAB_PATH = "Prefabs/Skills/IceAge";
	
	private GameObject iceAgePrefab;
	private GameObject iceAgeObj;
	private IndestructubleWall iceAge;
	private Vector3 iceAgePos;
	
	// Use this for initialization
	void Start () {
		iceAgePrefab =  Resources.Load(ICEAGE_PREGAB_PATH) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		iceAgeObj = Instantiate(iceAgePrefab) as GameObject;	
	}
}
