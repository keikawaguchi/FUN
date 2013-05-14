using UnityEngine;
using System.Collections;
using System.IO;

public class MapBuilder : MonoBehaviour
{
	
	private const char INDESTRUCTABLE_WALL = '#';
	private const char DESTRUCTABLE_WALL = '+';
	private string[] maps;

	void Start ()
	{
		maps = new string[10];
		maps [1] = "Maps/map1";
		maps [2] = "Maps/map2";
	}
	
	public void buildMap (bool[,] indestructable, bool[,] destructable, int mapID) {
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
					indestructable [gridX, gridY] = true;
				}
				if (mapUnit == DESTRUCTABLE_WALL) {
					destructable [gridX, gridY] = true;
				}
				gridX++;
			}
			gridY--;
		}
	}
	
	private TextAsset loadMapFile(int mapID) {
		string mapToLoad = maps [mapID];
		if (mapToLoad == null) {
			Debug.Log ("MapID " + mapID + " does not exist.");
		}	
		return Resources.Load (mapToLoad, typeof(TextAsset)) as TextAsset;
	}
}
