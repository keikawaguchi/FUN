using UnityEngine;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

public class MapBuilder : MonoBehaviour {
	
	private const char INDESTRUCTABLE_WALL = '#';
	private const char DESTRUCTABLE_WALL = '+';
	private const char UPGRADE = 'u';
	
	private const char SPAWN1 = '1';
	private const char SPAWN2 = '2';
	private const char SPAWN3 = '3';
	private const char SPAWN4 = '4';
	
	private Texture indestructableWallTexture;
	private Texture destructableWallTexture;
	private Texture floorTexture;
	private AudioClip backgroundMusic;
	
	const string INDESTRUCTABLE_BLOCK_PREFAB_PATH = "Prefabs/Wall/Indestructuble Wall";
	const string DESTRUCTABLE_BLOCK_PREFAB_PATH = "Prefabs/Wall/Destructuble Wall";
	const string GRASS_FLOOR_PREFAB_PATH = "Prefabs/Floor/GrassFloor";
	const string UPGRADE_PREFAB_PATH = "Prefabs/Upgrade";
	
	GameObject indestructableWallPrefab;
	GameObject destructableWallPrefab;
	GameObject grassFloorTilePrefab;
	GameObject upgradePrefab;
	
	private string[] maps;
	private Vector3[] spawnPoints;
	
	GridSystem gridSystem;
	
	void Start () {
		maps = new string[10];
		maps [1] = "Maps/map1";
		maps [2] = "Maps/map2";
		maps [3] = "Maps/map3";
		maps [4] = "Maps/map4";
		maps [5] = "Maps/map5";
		maps [6] = "Maps/map6";
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
			// Debug.Log ("Map not found or not readable");
			return;
		} 
		
		loadTextures(mapFile);
		loadBackgroundMusic(mapFile);
		buildFloor(grassFloorTilePrefab);
		moveToMapSectionOfMapFile(mapReader);
		
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
					// Debug.Log ("Respawn " + mapUnit + " added at index " + (mapUnit - 48));
					spawnPoints[mapUnit - 48] = new Vector3(gridSystem.getXCoord(gridX), 0, gridSystem.getYCoord(gridY));
				}
				gridX++;
			}
			gridY--;
		}
	}
	
	public void spawnIndestructableWall(int gridX, int gridY, GameObject[,] indestructable) {
		indestructable[gridX, gridY] = Instantiate(indestructableWallPrefab) as GameObject;
		if (indestructableWallTexture == null) {
			// Debug.Log ("Indest Texture NULL");
		}
		indestructable[gridX, gridY].renderer.material.mainTexture = indestructableWallTexture;
		indestructable[gridX, gridY].GetComponent<IndestructubleWall>().initialize(gridSystem.getXCoord(gridX), gridSystem.getYCoord(gridY));
	}
	
	public void spawnDestructableWall(int gridX, int gridY, GameObject[,] destructable) {
		destructable[gridX, gridY] = Instantiate(destructableWallPrefab) as GameObject;
		destructable[gridX, gridY].renderer.material.mainTexture = destructableWallTexture;
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
		grassFloorTilePrefab = Resources.Load(GRASS_FLOOR_PREFAB_PATH) as GameObject;
		upgradePrefab = Resources.Load(UPGRADE_PREFAB_PATH) as GameObject;
	}
	
	private void loadScripts() {
		gridSystem = GameObject.Find("Map").GetComponent<GridSystem>();
	}
	#endregion
	
	private TextAsset loadMapFile(int mapID) {
		string mapToLoad = maps [mapID];
		if (mapToLoad == null) {
			// Debug.Log ("MapID " + mapID + " does not exist.");
		}	
		return Resources.Load (mapToLoad, typeof(TextAsset)) as TextAsset;
	}
	
	private void loadTextures(TextAsset mapFile) {
		Match match;
	
		match = Regex.Match(mapFile.text, @"Indestructable:(.*)");
		if (match.Success) {
			indestructableWallTexture = Resources.Load(match.Groups[1].ToString()) as Texture;
			// Debug.Log("Indest. Wall Texture: " + match.Groups[1].ToString ());
		}
		
		match = Regex.Match(mapFile.text, @"Destructable:(.*)");
		if (match.Success) {
			destructableWallTexture = Resources.Load(match.Groups[1].ToString()) as Texture;
			// Debug.Log("Dest. Wall Texture: " + match.Groups[1].ToString ());
		}
		
		match = Regex.Match(mapFile.text, @"Floor:(.*)");
		if (match.Success) {
			floorTexture = Resources.Load(match.Groups[1].ToString()) as Texture;
			// Debug.Log("Floor Wall Texture: " + match.Groups[1].ToString ());
		}
	}
	
	private void loadBackgroundMusic(TextAsset mapFile) {
		Match match;
	
		match = Regex.Match(mapFile.text, @"Music:(.*)");
		if (!match.Success) {
			return;
		}
		
		backgroundMusic = Resources.Load(match.Groups[1].ToString()) as AudioClip;
		GameObject bgMusicObj = GameObject.Find("Music") as GameObject;
		
		if (bgMusicObj != null) {
			bgMusicObj.GetComponent<AudioSource>().clip = backgroundMusic;
			bgMusicObj.GetComponent<AudioSource>().Play();
			// Debug.Log("Music: " + match.Groups[1].ToString ());
		}
	}
	
	private void moveToMapSectionOfMapFile(StringReader mapReader) {
		string inputFileLine;
		bool atStartingPositionInFile = false;
		Match match;
		
		while (!atStartingPositionInFile) {
			inputFileLine = mapReader.ReadLine();
			match = Regex.Match (inputFileLine, @"StartMap");
			if (match.Success) {
				atStartingPositionInFile = true;
			}
		}
	}
	
	private void buildFloor(GameObject floorPrefab) {
		int mapHeight = gridSystem.getGridHeight();
		int mapWidth = gridSystem.getGridWidth();
		
		GameObject currentFloorTile;
		for (int row = 0; row < mapWidth; row++) {
			for (int column = 0; column < mapHeight; column++) {
				currentFloorTile = Instantiate(floorPrefab) as GameObject;
				currentFloorTile.renderer.material.mainTexture = floorTexture;
				currentFloorTile.transform.position = new Vector3(gridSystem.getXCoord(row), -1, gridSystem.getYCoord(column));
			}
		}
	}
}
