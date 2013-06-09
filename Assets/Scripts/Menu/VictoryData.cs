using UnityEngine;
using System.Collections;

public class VictoryData : MonoBehaviour {
	
	Hero Albion;
	Hero Temptress;
	Hero Merlini;
	Hero Fanndis;
	Hero Kirito;
	public int AlbionPlayerNumber;
	public int AlbionTeamNumber = -999;
	public int AlbionNumOfKills;
	public int AlbionNumOfDeaths;
	
	public int TemptressPlayerNumber;
	public int TemptressTeamNumber = -999;
	public int TemptressNumOfKills;
	public int TemptressNumOfDeaths;
	
	public int MerliniPlayerNumber;
	public int MerliniTeamNumber = -999;
	public int MerliniNumOfKills;
	public int MerliniNumOfDeaths;
	
	public int FanndisPlayerNumber;
	public int FanndisTeamNumber = -999;
	public int FanndisNumOfKills;
	public int FanndisNumOfDeaths;
	
	public int KiritoPlayerNumber;
	public int KiritoTeamNumber = -999;
	public int KiritoNumOfKills;
	public int KiritoNumOfDeaths;
	
	public bool winnerIsTeam;
	public int winnerNum;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevel == 1)
			Destroy(this);
	}
	
	public void GetData()
	{
		winnerIsTeam = GameObject.Find ("Game Manager").GetComponent<GameManager>().winnerIsTeam;
		winnerNum = GameObject.Find ("Game Manager").GetComponent<GameManager>().winner;
		if(GameObject.Find("Albion") != null)
		{
			Albion = GameObject.Find("Albion").GetComponent<Hero>();
			AlbionPlayerNumber = Albion.playerNumber;
			AlbionTeamNumber = Albion.teamNumber;
			AlbionNumOfKills = Albion.numOfKills;
			AlbionNumOfDeaths = Albion.numOfDeaths;
		}
		if(GameObject.Find("Temptress") != null)
		{
			Temptress = GameObject.Find("Temptress").GetComponent<Hero>();
			TemptressPlayerNumber = Temptress.playerNumber;
			TemptressTeamNumber = Temptress.teamNumber;
			TemptressNumOfKills = Temptress.numOfKills;
			TemptressNumOfDeaths = Temptress.numOfDeaths;
		}
		if(GameObject.Find("Merlini") != null)
		{
			Merlini = GameObject.Find("Merlini").GetComponent<Hero>();
			MerliniPlayerNumber = Merlini.playerNumber;
			MerliniTeamNumber = Merlini.teamNumber;
			MerliniNumOfKills = Merlini.numOfKills;
			MerliniNumOfDeaths = Merlini.numOfDeaths;
		}
		if(GameObject.Find("Fanndis") != null)
		{
			Fanndis = GameObject.Find("Fanndis").GetComponent<Hero>();
			FanndisPlayerNumber = Fanndis.playerNumber;
			FanndisTeamNumber = Fanndis.teamNumber;
			FanndisNumOfKills = Fanndis.numOfKills;
			FanndisNumOfDeaths = Fanndis.numOfDeaths;
		}
		if(GameObject.Find("Kirito") != null)
		{
			Kirito = GameObject.Find("Kirito").GetComponent<Hero>();
			KiritoPlayerNumber = Kirito.playerNumber;
			KiritoTeamNumber = Kirito.teamNumber;
			KiritoNumOfKills = Kirito.numOfKills;
			KiritoNumOfDeaths = Kirito.numOfDeaths;
		}
	}
	void Awake()
	{
		DontDestroyOnLoad (this);
	}
}
