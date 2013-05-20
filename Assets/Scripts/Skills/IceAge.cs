using UnityEngine;
using System.Collections;

public class IceAge : MonoBehaviour {
	private const string ICEAGE_PREGAB_PATH = "Prefabs/Skills/IceAge";
	
	private GameObject iceAgeObj;
	private IndestructubleWall iceAge;
	private Vector3 iceAgePos;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		iceAgeObj = Instantiate (ICEAGE_PREGAB_PATH) as GameObject;
		
	}
}
