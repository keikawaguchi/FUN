using UnityEngine;
using System.Collections;

public class AlbionBehavior : MonoBehaviour {
	private const string STUN_PREFAB_PATH = "Prefabs/Skills/Stun";
	private const string BLINK_PREFAB_PATH = "Prefabs/Skills/Blink";
	
	private Controller controller;
	private GameObject stunPrefab;
	private GameObject stun;
	private GameObject blinkPrefab;
	private GameObject blink;
	
	// skill cooldown times
	private const float blinkCD = 10f;
	private const float trapCD = 10f;
	
	// skill timers
	private float blinkTimer = 0f;
	private float trapTimer = 0f;
	
	void Start () {
		LoadSkills();
		LoadScripts();
	}
	
	void Update () {
		StunButtonPress ();
		BlinkButtonPress ();
	}
	
	private void LoadSkills() {
		stunPrefab = Resources.Load (STUN_PREFAB_PATH) as GameObject;
		blinkPrefab = Resources.Load (BLINK_PREFAB_PATH) as GameObject;
	}
	
	private void LoadScripts() {
		controller = GetComponent<Controller>();
	}
	
	private void StunButtonPress() {
		if (Input.GetButtonDown (controller.getButton ("Skill1"))) {
			if (Time.time - trapTimer > trapCD) {
				stun = Instantiate (stunPrefab) as GameObject;
				stun.GetComponent<Stun>().SetStunOwner (gameObject);
				
				trapTimer = Time.time;
			}
		}
	}
	
	private void BlinkButtonPress() {
		if (Input.GetButtonDown (controller.getButton ("Skill2"))) {
			if (Time.time - blinkTimer > blinkCD) {
				blink = Instantiate (blinkPrefab) as GameObject;
				blink.GetComponent<Blink>().SetGameObject (gameObject);
			
				blinkTimer = Time.time;
			}
		}
	}
}
