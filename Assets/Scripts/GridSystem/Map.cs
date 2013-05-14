using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	
	const string INDESTRUCTABLE_BLOCK_PREFAB_PATH = "Prefabs/Wall/Indestructuble Wall";
	const string DESTRUCTABLE_BLOCK_PREFAB_PATH = "Prefabs/Wall/Destructuble Wall";
	const string UPGRADE_PREFAB_PATH = "Prefabs/Upgrade";
	
	public int mapID = 1;
	
	private GameObject indestructableBlockPrefab;
	private GameObject destructableBlockPrefab;
	private GameObject upgrade;
	
	public bool[,] grid;
	public bool[,] destructibleWallGrid;
	
	GridSystem gridSystem;
	MapBuilder mapBuilder;

	void Start () {
		loadResources();
		loadScripts();
		buildMap();
		spawnUpgrade();
	}
	
	#region Public Methods
	public bool isGridFull(float x, float y) {
		int xCoord = gridSystem.getXPos(x);
		int yCoord = gridSystem.getYPos(y);	
		return grid[xCoord, yCoord] || destructibleWallGrid[xCoord, yCoord];
	}
	public bool isGridFull(int x, int y) {
		return grid[x, y] || destructibleWallGrid[x, y];
	}
	
	public GameObject getObjectAtGridLocation(int x, int y) {
		int playerPositionX = 0;
		int playerPositionY = 0;
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		
		for (int i = 0; players[i] != null; i++) {
			playerPositionX = gridSystem.getXPos(players[i].transform.position.x);
			playerPositionY = gridSystem.getYPos(players[i].transform.position.z);
			if (playerPositionX == x && playerPositionY == y) {
				return players[i];
			}
		}
		return null;
	}
	
	public void removeWall(int x, int y) {
		grid[x, y] = false;
		destructibleWallGrid[x, y] = false;
	}
	public void removeWall(float x, float y) {
		int gridX = gridSystem.getXPos(x);
		int gridY = gridSystem.getYPos(y);
		grid[gridX, gridY] = false;
		destructibleWallGrid[gridX, gridY] = false;
	}
	#endregion
	
	#region Initialize Methods
	private void loadResources() {
		indestructableBlockPrefab = Resources.Load(INDESTRUCTABLE_BLOCK_PREFAB_PATH) as GameObject;
		if (indestructableBlockPrefab == null) {
			Debug.Log("Indestructable block prefab is null");
		}
		
		destructableBlockPrefab = Resources.Load(DESTRUCTABLE_BLOCK_PREFAB_PATH) as GameObject;
		if (indestructableBlockPrefab == null) {
			Debug.Log("Destructable block prefab is null");
		}
		
		upgrade = Resources.Load(UPGRADE_PREFAB_PATH) as GameObject;
		if (indestructableBlockPrefab == null) {
			Debug.Log("Upgrade block prefab is null");
		}
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
		buildIndestructableWalls();
		buildDestructableWalls();
	}
	
	private void initializeGrids() {
		int gridWidth = gridSystem.getGridWidth();
		int gridHeight = gridSystem.getGridHeight();
				
		grid = new bool[gridWidth, gridHeight];
		destructibleWallGrid = new bool[gridWidth, gridHeight];

		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridHeight; y++) {
				grid[x,y] = false;
				destructibleWallGrid[x,y] = false;
			}
		}
	}
	
	private void buildIndestructableWalls() {
		int gridWidth = gridSystem.getGridWidth();
		int gridHeight = gridSystem.getGridHeight();
		
		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridHeight; y++) {
				if (grid[x, y] == true) {
					GameObject go = Instantiate(indestructableBlockPrefab) as GameObject;
					IndestructubleWall wall = go.GetComponent<IndestructubleWall>();
					wall.initialize(gridSystem.getXCoord(x), gridSystem.getYCoord(y));
				}
			}
		}
	}
	
	private void buildDestructableWalls() {
		int gridWidth = gridSystem.getGridWidth();
		int gridHeight = gridSystem.getGridHeight();
		
		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridHeight; y++) {
				if (destructibleWallGrid[x, y] == true) {
					GameObject go = Instantiate(destructableBlockPrefab) as GameObject;
					DestructibleWall wall = go.GetComponent<DestructibleWall>();
					wall.initialize(gridSystem.getXCoord(x), gridSystem.getYCoord(y));
				}
			}
		}
	}
	
	private void spawnUpgrade() {
		int gridWidth = gridSystem.getGridWidth();
		int gridHeight = gridSystem.getGridHeight();
		GameObject instantiateUpgrade = Instantiate (upgrade) as GameObject;
		instantiateUpgrade.transform.position = 
			new Vector3(gridSystem.getXCoord(gridWidth / 2), 0f, gridSystem.getYCoord(gridHeight / 2));
	}
}
