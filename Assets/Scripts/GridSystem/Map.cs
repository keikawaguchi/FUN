using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	
	const string INDESTRUCTABLE_BLOCK_PREFAB_PATH = "Prefabs/Indestructuble Wall";
	const string DESTRUCTABLE_BLOCK_PREFAB_PATH = "Prefabs/Destructuble Wall";
	const string UPGRADE_PREFAB_PATH = "Prefabs/Upgrade";
	
	public GameObject indestructableBlockPrefab;
	public GameObject destructableBlockPrefab;
	private GameObject upgrade;
	
	public bool[,] grid;
	public bool[,] destructibleWallGrid;
	
	GridSystem gridSystem;


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
		
		return grid[xCoord, yCoord];
	}
	public bool isGridFull(int x, int y) {
		return grid[x, y];
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
		gridSystem = this.GetComponent<GridSystem>();
		if (gridSystem == null) {
			Debug.Log("GridSystem is null");
		}
	}
	#endregion
	
	// creates the basic map layout with the indestructuble walls
	private void buildMap() {
		
		int gridWidth = gridSystem.getGridWidth();
		int gridHeight = gridSystem.getGridHeight();
				
		grid = new bool[gridWidth, gridHeight];
		destructibleWallGrid = new bool[gridWidth, gridHeight];
		
		// initialize grid array
		for (int x = 0; x < gridWidth; x++)
			for (int y = 0; y < gridHeight; y++) {
				grid[x,y] = false;
		}
		
		// initialize indestructuble walls
		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridHeight; y++) {
				
				if (gridSystem.isEdge(x, y) || (x % 2 == 0 && y % 2 == 0)) {
					
					GameObject go = Instantiate(indestructableBlockPrefab) as GameObject;
					IndestructubleWall wall = go.GetComponent<IndestructubleWall>();
					
					wall.initialize(gridSystem.getXCoord(x), gridSystem.getYCoord(y));
					
					grid[x,y] = true;
				}
			}
		}
		
		// initialize destructuble walls
//		for (int x = 1; x < gridWidth - 1; x++) {
//			for (int y = 1; y < gridHeight - 1; y++) {
//				
//				// don't create wall on indestructuble walls
//				if (grid[x,y])
//					continue;	
//				
//				// don't create walls in corners of map
//				if (x == 1 || x == (gridWidth - 2)) {
//					if (y == 1 || y == 2 || y == (gridHeight - 2) || y == (gridHeight - 3))
//						continue;
//				} else if (x == 2 || x == (gridWidth - 3))
//					if (y == 1 || y == (gridHeight - 2))
//						continue;
//				
//				// don't create wall in center of map
//				if (x == (gridWidth / 2) && y == (gridHeight / 2))
//					continue;
//				
//				GameObject go = Instantiate(destructableBlockPrefab) as GameObject;
//				DestructibleWall wall = go.GetComponent<DestructibleWall>();
//				wall.initialize(getXCoord(x), getYCoord(y));
//					
//				grid[x,y] = true;
//			}
//		}
	}
	
	private void spawnUpgrade() {
		int gridWidth = gridSystem.getGridWidth();
		int gridHeight = gridSystem.getGridHeight();
		GameObject instantiateUpgrade = Instantiate (upgrade) as GameObject;
		instantiateUpgrade.transform.position = 
			new Vector3(gridSystem.getXCoord(gridWidth / 2), 0f, gridSystem.getYCoord(gridHeight / 2));
	}
}
