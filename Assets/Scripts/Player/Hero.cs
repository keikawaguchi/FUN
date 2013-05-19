using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	const string BOMB_PREFAB_PATH = "Prefabs/Bomb/Bomb";

	public float lives = 5;
	public float dropBombCoolDownSeconds = 1.5f;
	public float bombX = 4;
	public float bombZ = 3;
	
	// timers
	private float timeOfLastBombDrop;
	private float deathTimer = 0f;
	
	private GameObject bomb;
	private GridSystem gridSystem;
	private Map map;
	private Controller controller;
	
	private Vector3[] spawnPoints;

	void Start () {
		loadResources();
		loadScripts();
		initialize();
		timeOfLastBombDrop = -999f;
		
		spawn();
	}
	
	void Update () {	
		handleControllerInput();
		
		// check if dead
		if (deathTimer > 0) {
				
			// how long a hero is dead should be grabbed from another script
			if (lives == 0) {
				Debug.Log ("Game Over");
			} else if (deathTimer != 0 && Time.time - deathTimer > 2.0f) {
				lives--;
				
				Debug.Log ("Lives Left: " + lives);
				deathTimer = 0;
				GetComponent<MeshRenderer>().enabled = true;
				
				// call gridsystem to get a respawn point
				respawn ();
			}
		}
	}
	
	public void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "KillsPlayer") {
			
			GetComponent<MeshRenderer>().enabled = false;
			deathTimer = Time.time;
		}
		
		if(collider.name == "BombUpgrade")
		{
			if(dropBombCoolDownSeconds > 0)
				dropBombCoolDownSeconds-=.5f;
		}
		if(collider.name == "ExplosionUpgrade")
		{
			bombX++;
			bombZ++;
		}
	}
	
	#region Initialization Methods
	private void loadResources() {
		bomb = Resources.Load (BOMB_PREFAB_PATH) as GameObject;
		if (bomb == null) {
			Debug.Log("Bomb prefab is NULL");
		}
	}
	
	private void loadScripts() {
		gridSystem = GameObject.Find("Map").GetComponent<GridSystem>();
		map = GameObject.Find("Map").GetComponent<Map>();
		controller = GetComponent<Controller>();
	} 
	
	private void initialize() {
		collider.isTrigger = true;
		
		/*
		 * Place this somewhere else and generate spawn points based on grid dimensions
		 */
		spawnPoints = new Vector3[4];
		
		// should grab spaw
		spawnPoints[0] = new Vector3(-112f, 0, 61);
		spawnPoints[1] = new Vector3(112f, 0, -79);
		spawnPoints[2] = new Vector3(112f, 0, 61);
		spawnPoints[3] = new Vector3(-112f, 0, -79);
	}
	#endregion
	
	private void handleControllerInput() {
		if (Input.GetButtonDown(controller.getButton("DropBomb"))) {
			dropBomb();
		}
	}
	
	private void dropBomb() {
		if (Time.time - timeOfLastBombDrop < dropBombCoolDownSeconds) {
			return;
		}
		GameObject instantiateBomb = Instantiate(bomb) as GameObject;

		// place bomb in closest grid position
		int xCoord = gridSystem.getXPos(transform.position.x);
		int yCoord = gridSystem.getYPos(transform.position.z);
		
		Vector3 bombLocation = new Vector3(gridSystem.getXCoord(xCoord), 0f, gridSystem.getYCoord(yCoord));
		instantiateBomb.transform.position = bombLocation;
		BombBehavior boom = instantiateBomb.GetComponent<BombBehavior>();
		boom.setter(bombX,bombZ);
		
		timeOfLastBombDrop = Time.time;
	}
	
	#region re/spawn PLACE SOMEWHERE ELSE
	private void spawn() {
		int playerNum = controller.controllerNumber;
		transform.position = spawnPoints[playerNum - 1];
		Debug.Log (transform.position);
	}
	
	private void respawn() {
		int rand = Random.Range(0,3);
		
		transform.position = spawnPoints[rand];
	}
	#endregion
}
