using UnityEngine;
using System.Collections;

public class HammerTime : MonoBehaviour {
	private const string PLAYER_TAG = "Player";
	private const string HAMMERTIMEAOE_PREFAB_PATH = "Prefabs/Skills/HammerTimeAOE";
	private const string HAMMERTIME_SFX_PATH = "Audio/SFX/lightningStrike";
	private const float HAMMER_SPEED = 150f;
	private const float HAMMER_TRAVEL_DISTANCE = 100f;
	private const float HAMMER_AOE_EFFECT = 20f;
	private const float HAMMER_STUN_DURATION = 1f;
	
	private GameObject hammerAOEPrefab;
	private GameObject hammerAOE;
	private GameObject animation;
	private AudioClip hammerSFX;
	
	private GridSystem gridSystem;
	
	private Vector3 startingPos;
	
	private GameObject owner;
	private int teamNum;
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
			
			AudioSource.PlayClipAtPoint (hammerSFX, transform.position, 0.4f);
			
			// Create AOE effect
			hammerAOE = Instantiate (hammerAOEPrefab) as GameObject;
			hammerAOE.transform.position = transform.position;
			
			checkAreaForEnemies(transform.position);
			
			Destroy (gameObject);	
		}
	}
	
	private void loadSkills() {
		hammerAOEPrefab = Resources.Load (HAMMERTIMEAOE_PREFAB_PATH) as GameObject;
		hammerSFX = Resources.Load (HAMMERTIME_SFX_PATH) as AudioClip;
		animation = Resources.Load ("Prefabs/Animations/Electric-Hammer-Animation") as GameObject;
		animation = Instantiate (animation) as GameObject;
		animation.GetComponent<Animation>().attachToObject (gameObject);
	}
	
	private void loadScripts() {
		gridSystem = GameObject.Find("Map").GetComponent<GridSystem>(); 
	}
	
	public void SetStunOwner(GameObject owner) {
		this.owner = owner;
	}
	
	public void SetTeamNum(int teamNum) {
		this.teamNum = teamNum;
	}

	
	void OnTriggerEnter(Collider collision) {
		
		// int teamNumber = owner.
		
		if (collision.tag == PLAYER_TAG && collision.gameObject != owner) {
			
			Debug.Log (collision.gameObject.GetComponent<Hero>().getTeamNumber());
			
			triggerStun(collision.gameObject);
			AudioSource.PlayClipAtPoint (hammerSFX, transform.position, 0.4f);
		
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
				}
			}
			
		}
	}

	private void triggerStun(GameObject otherPlayer) {
		AlterSpeed alterSpeed;
		alterSpeed = otherPlayer.gameObject.AddComponent<AlterSpeed>();
		alterSpeed.Start(0f, HAMMER_STUN_DURATION);
	}
}
