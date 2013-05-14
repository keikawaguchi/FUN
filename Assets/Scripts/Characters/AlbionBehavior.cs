using UnityEngine;
using System.Collections;

public class AlbionBehavior : MonoBehaviour {
	private const string STUN_PREFAB_PATH = "Prefabs/Skills/Stun";
	private const string BLINK_PREFAB_PATH = "Prefabs/Skills/Blink";
	
	private Player1Controller controller;
	private GameObject stunPrefab;
	private GameObject stun;
	private GameObject blinkPrefab;
	private GameObject blink;
	
	// Use this for initialization
	void Start () {
		LoadSkills();
		LoadScripts();
	}
	
	// Update is called once per frame
	void Update () {
		StunButtonPress ();
		BlinkButtonPress ();
	}
	
	private void LoadSkills() {
		stunPrefab = Resources.Load (STUN_PREFAB_PATH) as GameObject;
		blinkPrefab = Resources.Load (BLINK_PREFAB_PATH) as GameObject;
	}
	
	private void LoadScripts() {
		controller = GetComponent<Player1Controller>();
	}
	
	private void StunButtonPress() {
		if (Input.GetButtonDown (controller.getButton ("Skill1"))) {
			stun = Instantiate (stunPrefab) as GameObject;
			stun.GetComponent<Stun>().SetTrapPosition (transform.position);
		}
	}
	
	private void BlinkButtonPress() {
		if (Input.GetButtonDown (controller.getButton ("Skill2"))) {
			blink = Instantiate (blinkPrefab) as GameObject;
			blink.GetComponent<Blink>().SetGameObject (gameObject);
		}
	}
}
