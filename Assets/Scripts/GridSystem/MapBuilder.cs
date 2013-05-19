using UnityEngine;
using System.Collections;
using System.IO;

public class MapBuilder : MonoBehaviour {
	
	private const char INDESTRUCTABLE_WALL = '#';
	private const char DESTRUCTABLE_WALL = '+';
	private const char UPGRADE = 'u';
	
	private const char SPAWN1 = '1';
	private const char SPAWN2 = '2';
	private const char SPAWN3 = '3';
	private const char SPAWN4 = '4';
	
	const string INDESTRUCTABLE_BLOCK_PREFAB_PATH = "Prefabs/Wall/Indestructuble Wall";
	const string DESTRUCTABLE_BLOCK_PREFAB_PATH = "Prefabs/Wall/Destructuble Wall";
	const string UPGRADE_PREFAB_PATH = "Prefabs/Upgrade";
	
	GameObject indestructableWallPrefab;
	GameObject destructableWallPrefab;	
	GameObject upgradePrefab;
	
	private string[] maps;
	private Vector3[] spawnPoints;
	
	GridSystem gridSystem;
	
	void Start () {
		maps = new string[10];
		maps [1] = "Maps/map1";
		maps [2] = "Maps/map2";
		loadResources();
		loadScripts();
	}
	
	#region Public Methods
	public void buildMap (int mapID, 
		GameObject[,] indestructable, 
		GameObject[,] destructable, 
		Vector3[] spawnPoints) 
	{
		TextAsset mapFile;
		StringReader mapReader;
		
		mapFile = loadMapFile(mapID);	
		mapReader = new StringReader(mapFile.text);
		if (mapReader == null) {
			Debug.Log ("Map not found or not readable");
			return;
		} 

		string inputFileLine;
		int gridY = 12;
		int gridX = 0;
		
		while ((inputFileLine = mapReader.ReadLine()) != null) {
			gridX = 0;
			foreach (char mapUnit in inputFileLine) {
				if (mapUnit == INDESTRUCTABLE_WALL) {
					spawnIndestructableWall(gridX, gridY, indestructable);
				}
				if (mapUnit == DESTRUCTABLE_WALL) {
					spawnDestructableWall(gridX, gridY, destructable);
				}
				if (mapUnit == UPGRADE) {
					spawnUpgrade(gridX, gridY);
				}
				if (mapUnit == SPAWN1 || mapUnit == SPAWN2 || mapUnit == SPAWN3 || mapUnit == SPAWN4) {
					Debug.Log ("Respawn " + mapUnit + " added at index " + (mapUnit - 48));
					spawnPoints[mapUnit - 48] = new Vector3(gridSystem.getXCoord(gridX), 0, gridSystem.getYCoord(gridY));
				}
				gridX++;
			}
			gridY--;
		}
	}
	
	public void spawnIndestructableWall(int gridX, int gridY, GameObject[,] indestructable) {
		indestructable[gridX, gridY] = Instantiate(indestructableWallPrefab) as GameObject;
		indestructable[gridX, gridY].GetComponent<IndestructubleWall>().initialize(gridSystem.getXCoord(gridX), gridSystem.getYCoord(gridY));
	}
	
	public void spawnDestructableWall(int gridX, int gridY, GameObject[,] destructable) {
		destructable[gridX, gridY] = Instantiate(destructableWallPrefab) as GameObject;
		destructable[gridX, gridY].GetComponent<DestructibleWall>().initialize(gridSystem.getXCoord(gridX), gridSystem.getYCoord(gridY));
	}
	
	public void spawnUpgrade(int x, int y) {
		Vector3 position;
		position.y = 0;
		position.x = gridSystem.getXCoord(x);
		position.z = gridSystem.getYCoord(y);
		
		GameObject upgrade = Instantiate(upgradePrefab) as GameObject;
		upgrade.transform.position = position;
	}
	#endregion
	
	#region Initialization Methods
	private void loadResources() {
		indestructableWallPrefab = Resources.Load(INDESTRUCTABLE_BLOCK_PREFAB_PATH) as GameObject;
		destructableWallPrefab = Resources.Load(DESTRUCTABLE_BLOCK_PREFAB_PATH) as GameObject;	
		upgradePrefab = Resources.Load(UPGRADE_PREFAB_PATH) as GameObject;
	}
	
	private void loadScripts() {
		gridSystem = GameObject.Find("Map").GetComponent<GridSystem>();
	}
	#endregion
	
	private TextAsset loadMapFile(int mapID) {
		string mapToLoad = maps [mapID];
		if (mapToLoad == null) {
			Debug.Log ("MapID " + mapID + " does not exist.");
		}	
		return Resources.Load (mapToLoad, typeof(TextAsset)) as TextAsset;
	}
}
