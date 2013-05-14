using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	
	public int mapID = 1;
	
	public GameObject[,] grid;
	public GameObject[,] destructibleWallGrid;
	
	GridSystem gridSystem;
	MapBuilder mapBuilder;

	void Start () {
		loadResources();
		loadScripts();
		buildMap();
	}
	
	#region Public Methods
	public bool isGridFull(float x, float y) {
		int xCoord = gridSystem.getXPos(x);
		int yCoord = gridSystem.getYPos(y);	
		return (grid[xCoord, yCoord] != null) 
			|| (destructibleWallGrid[xCoord, yCoord] != null);
	}
	public bool isGridFull(int x, int y) {
		return (grid[x, y] != null) 
			|| (destructibleWallGrid[x, y] != null);
	}
	
	public GameObject getObjectAtGridLocation(int x, int y) {
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
		
		// If no players, check if a wall exists
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
		if (grid[x, y] != null) {
			Destroy(grid[x, y].gameObject);
			grid[x, y] = null;
		}
		if (destructibleWallGrid[x, y] != null) {
			Destroy(destructibleWallGrid[x, y].gameObject);
			destructibleWallGrid[x, y] = null;	
		}
	}
	#endregion
	
	#region Initialize Methods
	private void loadResources() {
		
	}
	
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

		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridHeight; y++) {
				grid[x,y] = null;
				destructibleWallGrid[x,y] = null;
			}
		}
	}
}
