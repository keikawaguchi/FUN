using UnityEngine;
using System.Collections;

public class SpawnChamps : MonoBehaviour {
	private const string ALBION_PREFAB_PATH = "Prefabs/Champions/Albion";
	private const string FANNDIS_PREFAB_PATH = "Prefabs/Champions/Fanndis";
	private const string KIRITO_PREFAB_PATH = "Prefabs/Champions/Kirito";
	private const string MERLINI_PREFAB_PATH = "Prefabs/Champions/Merlini";
	private const string TEMPTRESS_PREFAB_PATH = "Prefabs/Champions/Temptress";
	
	private const int ALBION = 0;
	private const int FANNDIS = 1;
	private const int KIRITO = 2;
	private const int MERLINI = 3;
	private const int TEMPTRESS = 4;
	
	private const int TOTAL_AVAILABLE_CHAMPS = 5;
	private const int MAX_PLAYERS = 4;
	
	private GameObject[] championsPrefabs;
	private GameObject[] champions;
	private int[] playersChamps;
	
	private PlayerControls controlNums;
	
	// Use this for initialization
	void Start () {
		initializeVariables ();
		loadPrefabs ();
		setPlayersChamps ();
		
		spawnChampions ();
	}
	
	private void initializeVariables() {
		controlNums = GameObject.Find("Controls").GetComponent<PlayerControls>();
		championsPrefabs = new GameObject[TOTAL_AVAILABLE_CHAMPS];
		champions = new GameObject[MAX_PLAYERS];
		playersChamps = new int[MAX_PLAYERS];
	}
	
	private void setPlayersChamps() {
		playersChamps[0] = controlNums.player1;
		playersChamps[1] = controlNums.player2;
		playersChamps[2] = controlNums.player3;
		playersChamps[3] = controlNums.player4;
	}
	
	private void loadPrefabs() {
		championsPrefabs[0] = Resources.Load (ALBION_PREFAB_PATH) as GameObject;
		championsPrefabs[1] = Resources.Load (FANNDIS_PREFAB_PATH) as GameObject;
		championsPrefabs[2] = Resources.Load (KIRITO_PREFAB_PATH) as GameObject;
		championsPrefabs[3] = Resources.Load (MERLINI_PREFAB_PATH) as GameObject;
		championsPrefabs[4] = Resources.Load (TEMPTRESS_PREFAB_PATH) as GameObject;
	}
	
	private void spawnChampions() {
		// player 1
		if (controlNums.player1 == ALBION)
			champions[0] = Instantiate (championsPrefabs[0]) as GameObject;
		else if (controlNums.player1 == FANNDIS)
			champions[0] = Instantiate (championsPrefabs[1]) as GameObject;
		else if (controlNums.player1 == KIRITO)
			champions[0] = Instantiate (championsPrefabs[2]) as GameObject;
		else if (controlNums.player1 == MERLINI)
			champions[0] = Instantiate (championsPrefabs[3]) as GameObject;
		else if (controlNums.player1 == TEMPTRESS)
			champions[0] = Instantiate (championsPrefabs[4]) as GameObject;
		champions[0].GetComponent<XInputController>().SetControllerNumber (1);
		
		// player 2
		if (controlNums.player2 == ALBION)
			champions[1] = Instantiate (championsPrefabs[0]) as GameObject;
		else if (controlNums.player2 == FANNDIS)
			champions[1] = Instantiate (championsPrefabs[1]) as GameObject;
		else if (controlNums.player2 == KIRITO)
			champions[1] = Instantiate (championsPrefabs[2]) as GameObject;
		else if (controlNums.player2 == MERLINI)
			champions[1] = Instantiate (championsPrefabs[3]) as GameObject;
		else if (controlNums.player2 == TEMPTRESS)
			champions[1] = Instantiate (championsPrefabs[4]) as GameObject;
		champions[1].GetComponent<XInputController>().SetControllerNumber (2);
		
		// player 3
		if (controlNums.player3 == ALBION)
			champions[2] = Instantiate (championsPrefabs[0]) as GameObject;
		else if (controlNums.player3 == FANNDIS)
			champions[2] = Instantiate (championsPrefabs[1]) as GameObject;
		else if (controlNums.player3 == KIRITO)
			champions[2] = Instantiate (championsPrefabs[2]) as GameObject;
		else if (controlNums.player3 == MERLINI)
			champions[2] = Instantiate (championsPrefabs[3]) as GameObject;
		else if (controlNums.player3 == TEMPTRESS)
			champions[2] = Instantiate (championsPrefabs[4]) as GameObject;
		champions[2].GetComponent<XInputController>().SetControllerNumber (3);
		
		// player 4
		if (controlNums.player4 == ALBION)
			champions[3] = Instantiate (championsPrefabs[0]) as GameObject;
		else if (controlNums.player4 == FANNDIS)
			champions[3] = Instantiate (championsPrefabs[1]) as GameObject;
		else if (controlNums.player4 == KIRITO)
			champions[3] = Instantiate (championsPrefabs[2]) as GameObject;
		else if (controlNums.player4 == MERLINI)
			champions[3] = Instantiate (championsPrefabs[3]) as GameObject;
		else if (controlNums.player4 == TEMPTRESS)
			champions[3] = Instantiate (championsPrefabs[4]) as GameObject;
		champions[3].GetComponent<XInputController>().SetControllerNumber (4);
	}
}
