using UnityEngine;
using System.Collections;

public class HammerTime : MonoBehaviour {
	private const string PLAYER_TAG = "Player";
	private const string HAMMERTIMEAOE_PREFAB_PATH = "Prefabs/Skills/HammerTimeAOE";
	private const float HAMMER_SPEED = 150f;
	private const float HAMMER_TRAVEL_DISTANCE = 100f;
	private const float HAMMER_AOE_EFFECT = 20f;
	private const float HAMMER_STUN_DURATION = 1f;
	
	private GameObject hammerAOEPrefab;
	private GameObject hammerAOE;
	
	private GridSystem gridSystem;
	
	private Vector3 startingPos;
	
	private GameObject owner;
	private bool isStun;

	// Use this for initialization
	void Start () {
		startingPos = transform.position;
		loadSkills();
		loadScripts();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * HAMMER_SPEED * Time.smoothDeltaTime;
		
		float travelDistance = Vector3.Distance(transform.position, startingPos);
		if (travelDistance > HAMMER_TRAVEL_DISTANCE) {
			
			// Create AOE effect
			hammerAOE = Instantiate (hammerAOEPrefab) as GameObject;
			hammerAOE.transform.position = transform.position;
			
			checkAreaForEnemies(transform.position);
			
			Destroy (gameObject);	
		}
	}
	
	private void loadSkills() {
		hammerAOEPrefab = Resources.Load (HAMMERTIMEAOE_PREFAB_PATH) as GameObject;
	}
	
	private void loadScripts() {
		gridSystem = GameObject.Find("Map").GetComponent<GridSystem>(); 
	}
	
	public void SetStunOwner(GameObject owner) {
		this.owner = owner;
	}
	
	void OnTriggerEnter(Collider collision) {
		if (collision.tag == PLAYER_TAG && collision.gameObject != owner) {
			
			triggerStun(collision.gameObject);
			popupStunText(collision.gameObject.transform.position);
		
			Destroy (gameObject);
			
			// Create AOE effect
			hammerAOE = Instantiate (hammerAOEPrefab) as GameObject;
			hammerAOE.transform.position = collision.gameObject.transform.position;
			
			checkAreaForEnemies(collision.gameObject.transform.position);
		}
	}
	
	private void checkAreaForEnemies(Vector3 collisionPos) {

		GameObject[] players;
		players = GameObject.FindGameObjectsWithTag("Player");
		
		foreach(GameObject player in players) {
			if (player != owner) {
				
				float distance = Vector3.Distance(collisionPos, player.transform.position);
				if (distance < HAMMER_AOE_EFFECT) {
					triggerStun(player);
					popupStunText(player.transform.position);
				}
			}
			
		}
	}

	private void triggerStun(GameObject otherPlayer) {
		AlterSpeed alterSpeed;
		alterSpeed = otherPlayer.gameObject.AddComponent<AlterSpeed>();
		alterSpeed.setSpeedMultiplier(0f);
		alterSpeed.setDurationInSeconds(HAMMER_STUN_DURATION);
	}
	
	private void popupStunText(Vector3 textPos) {
		// add stun gametext
		GameObject t = Resources.Load ("Prefabs/Text/PopupText") as GameObject;
		GameObject text = Instantiate(t) as GameObject;	
		
		PopupText popupText = text.GetComponent<PopupText>();
		popupText.initialize();
		popupText.setDuration(HAMMER_STUN_DURATION);
		popupText.setPosition(textPos.x, textPos.z + 7);
		popupText.setPredefinedText("Stun");
	}
}
