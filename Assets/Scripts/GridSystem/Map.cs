using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	
	public int mapID = 1;
	
	public GameObject[,] grid;
	public GameObject[,] destructibleWallGrid;
	private GameObject[,] impassableObjects;
	
	GridSystem gridSystem;
	MapBuilder mapBuilder;
	
	private Vector3[] spawnPoint;
	
	private int lastRespawnPoint = 0;	// keep track of last respawn index location
	
	void Start () {
		loadScripts();
		buildMap();
		getSpawnPoints();
	}
	
	#region Public Methods
	public bool isGridFull(float x, float y) {
		return isGridFull(gridSystem.getXPos(x), gridSystem.getYPos(y));
	}
	public bool isGridFull(int x, int y) {
		if (isOutOfBounds(x, y)) {
			return true;
		}	
		return (grid[x, y] != null) 
			|| (destructibleWallGrid[x, y] != null) 
			|| (impassableObjects[x, y] != null);
	}
	
	public GameObject getObjectAtGridLocation(float x, float y) {
		return getObjectAtGridLocation(gridSystem.getXPos(x), gridSystem.getYPos(y));
	}
	public GameObject getObjectAtGridLocation(int x, int y) {
		if (isOutOfBounds(x, y)) {
			return null;
		}
		
		int playerPositionX = 0;
		int playerPositionY = 0;
		
		// Check for players first
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		for (int i = 0; i < players.Length; i++) {
			playerPositionX = gridSystem.getXPos(players[i].transform.position.x);
			playerPositionY = gridSystem.getYPos(players[i].transform.position.z);
			if (playerPositionX == x && playerPositionY == y) {
				return players[i];
			}
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
	#endregion
	
	#region Initialize Methods
	private void loadScripts() {
		gridSystem = GetComponent<GridSystem>();
		mapBuilder = GetComponent<MapBuilder>();
	}
	#endregion
	
	private void buildMap() {	
		int gridWidth = gridSystem.getGridWidth();
		int gridHeight = gridSystem.getGridHeight();
			
		initializeGrids();
		mapBuilder.buildMap (grid, destructibleWallGrid, mapID);
	}
	
	private void initializeGrids() {
		int gridWidth = gridSystem.getGridWidth();
		int gridHeight = gridSystem.getGridHeight();
				
		grid = new GameObject[gridWidth, gridHeight];
		destructibleWallGrid = new GameObject[gridWidth, gridHeight];
		impassableObjects = new GameObject[gridWidth, gridHeight];

		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridHeight; y++) {
				grid[x,y] = null;
				destructibleWallGrid[x,y] = null;
				impassableObjects[x, y] = null;
			}
		}
	}
	
	private bool isOutOfBounds(int x, int y) {
		return (x < 0 && x >= gridSystem.getGridWidth())
			&& (y < 0 && y >= gridSystem.getGridHeight());
	}
	
	#region player spawn and respawn
	private void getSpawnPoints() { spawnPoint = mapBuilder.getSpawnPoints(); }
	
	public Vector3 getSpawnLoc(int playerNumber) { return spawnPoint[playerNumber]; }
	
	public Vector3 getRespawnLoc() {
		int respawnIndex = Random.Range(1,4);
		
		while (respawnIndex == lastRespawnPoint)
			respawnIndex = Random.Range(1,4);
		
		lastRespawnPoint = respawnIndex;
		
		return spawnPoint[respawnIndex];
	}
	#endregion
}
