using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	const string BOMB_PREFAB_PATH = "Prefabs/Bomb/Bomb";
	private const int MAX_NUM_0F_BOMBS = 5;
	
	public int playerNumber;
	public int teamNumber;
	
	public int numOfKills;
	public int numOfDeaths;

	public int lives = 5;
	public int numOfBombs;
	public float bombX = 4;
	public float bombZ = 3;
	
	// timers
	private float timeOfLastBombDrop;
	private float deathTimer = 0f;
	
	private GameObject bomb;
	private GridSystem gridSystem;
	private Map map;
	private XInputController controller;
	private bool isInvincible = false;
	private bool canDropBomb = true;
	private int bombCounter;

	public bool isAlive;

	void Start () {
		loadResources();
		loadScripts();
		initialize();
		timeOfLastBombDrop = -999f;	
		spawnHero();
		
		numOfKills = 0;
		numOfDeaths = 0;
		
		numOfBombs = 1;
		bombCounter = 0;
		
		isAlive = true;
	}
	
	void Update () {
		
		// check if dead
		if (deathTimer > 0) {
				
			// how long a hero is dead should be grabbed from another script
			if (lives == 0) {
				checkGameOver();
			} else if (deathTimer != 0 && Time.time - deathTimer > 2.0f) {
				deathTimer = 0;
				GetComponent<MeshRenderer>().enabled = true;
				
				respawnHero();
			}
		} else {
			handleControllerInput();
		}
	}
	
	public void OnTriggerEnter(Collider collider) {

		if (collider.gameObject.tag == "KillsPlayer" && isAlive && !isInvincible) {
			isAlive = false;
			lives--;
			numOfDeaths++;
			
			Hero bombOwner =  collider.gameObject.GetComponent<FireBehavior>().owner;
			updatePlayerScore(bombOwner);
			
			GetComponent<MeshRenderer>().enabled = false;
			
			// quick fix so heroes can't do anything when dead
			transform.position = new Vector3(-9999, 0, 0);
			
			deathTimer = Time.time;
		}
		
		if(collider.name == "BombUpgrade")
		{
			if (numOfBombs <= MAX_NUM_0F_BOMBS)
				numOfBombs++;
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
	}
	
	private void loadScripts() {
		gridSystem = GameObject.Find("Map").GetComponent<GridSystem>();
		map = GameObject.Find("Map").GetComponent<Map>();
		controller = GetComponent<XInputController>();
	} 
	
	private void initialize() {
		collider.isTrigger = true;
		playerNumber = controller.GetControllerNumber();
	}
	#endregion
	
	private void handleControllerInput() {
		if (controller.GetButtonPressed("DropBomb") && canDropBomb) {
			dropBomb();
		}
	}
	
	private void dropBomb() {
		if (bombCounter >= numOfBombs)
			return;
		
		GameObject instantiateBomb = Instantiate(bomb) as GameObject;

		// place bomb in closest grid position
		int xCoord = gridSystem.getXPos(transform.position.x);
		int yCoord = gridSystem.getYPos(transform.position.z);
		
		Vector3 bombLocation = new Vector3(gridSystem.getXCoord(xCoord), 0f, gridSystem.getYCoord(yCoord));
		instantiateBomb.transform.position = bombLocation;
		BombBehavior boom = instantiateBomb.GetComponent<BombBehavior>();
		boom.setExplosionDistance(bombX,bombZ);
		boom.setHero(gameObject.GetComponent<Hero>());
		bombCounter++;
		
		timeOfLastBombDrop = Time.time;
	}
	
	#region Player spawn and respawn locations
	private void spawnHero() { 
		transform.position = map.getSpawnLoc(playerNumber); 
	}
	
	private void respawnHero() { 
		transform.position = map.getRespawnLoc(); 
		isAlive = true;
	}
	#endregion
	
	public int getTeamNumber() {
		return teamNumber;	
	}
	
	private void updatePlayerScore(Hero bombOwner) {
		
		int killerTeamNum = bombOwner.getTeamNumber();
		
		if (teamNumber == killerTeamNum)
			bombOwner.numOfKills--;
		else
			bombOwner.numOfKills++;
	}
	
	private void checkGameOver() {
		GameManager manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		
		if (manager.checkForWinner()) {
			int currentLevel = Application.loadedLevel;
			Application.LoadLevel (++currentLevel);
		}
	}
	
	public void setInvincible(bool isInvincible) {
		this.isInvincible = isInvincible;
	}
	
	public void setCanDropBomb(bool canDropBomb) {
		this.canDropBomb = canDropBomb;
	}
	
	public void decreaseBombCounter() {
		if (bombCounter != 0)
			bombCounter--;
		if (bombCounter == 0)
			bombCounter = 0;
	}
}
