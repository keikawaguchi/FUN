using UnityEngine;
using System.Collections;

public class VictoryData : MonoBehaviour {
	
	Hero Albion;
	Hero Temptress;
	Hero Merlini;
	Hero Fanndis;
	Hero Kirito;
	public int AlbionPlayerNumber = -1;
	public int AlbionTeamNumber = -1;
	public int AlbionNumOfKills = -1;
	public int AlbionNumOfDeaths = -1;
	
	public int TemptressPlayerNumber = -1;
	public int TemptressTeamNumber = -1;
	public int TemptressNumOfKills = -1;
	public int TemptressNumOfDeaths = -1;
	
	public int MerliniPlayerNumber = -1;
	public int MerliniTeamNumber = -1;
	public int MerliniNumOfKills = -1;
	public int MerliniNumOfDeaths = -1;
	
	public int FanndisPlayerNumber = -1;
	public int FanndisTeamNumber = -1;
	public int FanndisNumOfKills = -1;
	public int FanndisNumOfDeaths = -1;
	
	public int KiritoPlayerNumber = -1;
	public int KiritoTeamNumber = -1;
	public int KiritoNumOfKills = -1;
	public int KiritoNumOfDeaths = -1;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void GetData()
	{
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
		DontDestroyOnLoad(this);
	}
}
