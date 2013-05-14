using UnityEngine;
using System.Collections;

public class AlbionBehavior : MonoBehaviour {
	private const string STUN_PREFAB_PATH = "Prefabs/Skills/Stun";
	private const string BLINK_PREFAB_PATH = "Prefabs/Skills/Blink";
	
	private Player1Controller controller;
	private GameObject stun;
	private GameObject blink;
	
	// Use this for initialization
	void Start () {
		LoadSkills();
		LoadScripts();
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetButton (controller.getButton ("Skill1")))
//			Instantiate (stun) as GameObject;
	}
	
	private void LoadSkills() {
		stun = Resources.Load (STUN_PREFAB_PATH) as GameObject;
		blink = Resources.Load (BLINK_PREFAB_PATH) as GameObject;
	}
	
	private void LoadScripts() {
		controller = GetComponent<Player1Controller>();
	}
}
