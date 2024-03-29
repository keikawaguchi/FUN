using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	
	public int mapID;
	
	public GameObject[,] grid;
	public GameObject[,] destructibleWallGrid;
	private GameObject[,] impassableObjects;
	private Vector3[] spawnPoints;

	GridSystem gridSystem;
	MapBuilder mapBuilder;
	PlayerControls saveSelection;
	
	private int lastRespawnPoint = 0;	// keep track of last respawn index location
	
	void Start () {
		loadScripts();
		buildMap();
	}
	
	#region Public Methods
	public bool isGridFull(float x, float y) {
		return isGridFull(gridSystem.getXPos(x), gridSystem.getYPos(y));
	}
	public bool isGridFull(int x, int y) {
		if (isOutOfBounds(x, y)) {
			return true;
		}	
		return grid[x, y] != null
			|| destructibleWallGrid[x, y] != null
			|| impassableObjects[x, y] != null;
	}
	
	public bool isPlayerAtGridLocation(float x, float y) {
		return isPlayerAtGridLocation(gridSystem.getXPos(x), gridSystem.getYPos(y));
	}
	public bool isPlayerAtGridLocation(int x, int y) {
		if (isOutOfBounds(x, y)) {
			return false;
		}

		if (getPlayerAtLocation(x, y) != null) {
			return true;
		}
		return false;
	}
	
	public GameObject getPlayerAtLocation(float x, float y) {
		return getPlayerAtLocation(gridSystem.getXPos(x), gridSystem.getYPos(y));
	}
	public GameObject getPlayerAtLocation(int x, int y) {
		if (isOutOfBounds(x, y)) {
			return null;
		}
		
		int playerPositionX = 0;
		int playerPositionY = 0;
		
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		for (int i = 0; i < players.Length; i++) {
			playerPositionX = gridSystem.getXPos(players[i].transform.position.x);
			playerPositionY = gridSystem.getYPos(players[i].transform.position.z);
			if (playerPositionX == x && playerPositionY == y) {
				return players[i];
			}
		}
		
		return null;
	}
	
	public GameObject getObjectAtGridLocation(float x, float y) {
		return getObjectAtGridLocation(gridSystem.getXPos(x), gridSystem.getYPos(y));
	}
	public GameObject getObjectAtGridLocation(int x, int y) {
		if (isOutOfBounds(x, y)) {
			return null;
		}
		
		GameObject objectAtLocation;
		objectAtLocation = getPlayerAtLocation(x, y);
		if (objectAtLocation != null) {
			return objectAtLocation;
		}
		
		// Check for impassable objects
		if (impassableObjects[x, y] != null) {
			return impassableObjects[x, y];
		}
		
		// Check if a wall exists
		if (grid[x, y] != null) {
			return grid[x, y];
		}
		
		if (destructibleWallGrid[x, y] != null) {
			return destructibleWallGrid[x, y];
		}
		
		return null;
	}
	
	public void removeWall(float x, float y) {
		int gridX = gridSystem.getXPos(x);
		int gridY = gridSystem.getYPos(y);
		removeWall(gridX, gridY);
	}
	public void removeWall(int x, int y) {
		if (isOutOfBounds(x, y)) {
			return;
		}
		
		if (grid[x, y] != null) {
			Destroy(grid[x, y].gameObject);
			grid[x, y] = null;
		}
		if (destructibleWallGrid[x, y] != null) {
			Destroy(destructibleWallGrid[x, y].gameObject);
			destructibleWallGrid[x, y] = null;	
		}
	}
	
	public bool addImpassableObject(float x, float y, GameObject obj) {
		return addImpassableObject(gridSystem.getXPos(x), gridSystem.getYPos(y), obj);
	}
	public bool addImpassableObject(int x, int y, GameObject obj) {
		if (isOutOfBounds(x, y)) {
			return false;
		}
		
		if (impassableObjects[x, y] != null) {
			return false;
		}
		impassableObjects[x, y] = obj;
		return true;
	}
	
	public Vector3 getSpawnLoc(int playerNumber) { 
		return spawnPoints[playerNumber]; 
	}
	
	public Vector3 getRespawnLoc() {
		int respawnIndex = Random.Range(1,4);
		
		while (respawnIndex == lastRespawnPoint)
			respawnIndex = Random.Range(1,4);
		
		lastRespawnPoint = respawnIndex;
		
		return spawnPoints[respawnIndex];
	}
	#endregion
	
	#region Initialize Methods
	private void loadScripts() {
		gridSystem = GetComponent<GridSystem>();
		mapBuilder = GetComponent<MapBuilder>();
		saveSelection = GameObject.Find ("Controls").GetComponent<PlayerControls>();
	}
	#endregion
	
	private void buildMap() {
		mapID = saveSelection.mapNum;
		int gridWidth = gridSystem.getGridWidth();
		int gridHeight = gridSystem.getGridHeight();
			
		initializeGrids();
		mapBuilder.buildMap (mapID, grid, destructibleWallGrid, spawnPoints);
	}
	
	private void initializeGrids() {
		int gridWidth = gridSystem.getGridWidth();
		int gridHeight = gridSystem.getGridHeight();
				
		grid = new GameObject[gridWidth, gridHeight];
		destructibleWallGrid = new GameObject[gridWidth, gridHeight];
		impassableObjects = new GameObject[gridWidth, gridHeight];
		spawnPoints = new Vector3[5];
	}
	
	private bool isOutOfBounds(int x, int y) {
		return (x < 0 || x >= gridSystem.getGridWidth())
			|| (y < 0 || y >= gridSystem.getGridHeight());
	}
}
