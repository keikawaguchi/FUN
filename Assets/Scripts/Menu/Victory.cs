using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour {
	
	private GameManager manager;
	VictoryData VD;
	private int winnerPlayerNum;
	int AlbionPlayerNumber;
	int AlbionTeamNumber;
	int AlbionNumOfKills;
	int AlbionNumOfDeaths;
	
	int TemptressPlayerNumber;
	int TemptressTeamNumber;
	int TemptressNumOfKills;
	int TemptressNumOfDeaths;
	
	int MerliniPlayerNumber;
	int MerliniTeamNumber;
	int MerliniNumOfKills;
	int MerliniNumOfDeaths;
	
	int FanndisPlayerNumber;
	int FanndisTeamNumber;
	int FanndisNumOfKills;
	int FanndisNumOfDeaths;
	
	int KiritoPlayerNumber;
	int KiritoTeamNumber;
	int KiritoNumOfKills;
	int KiritoNumOfDeaths;
	
	
	int SoloPCounter = 1;
	int Team1PCounter = 1;
	int Team2PCounter = 1;
	
	Rect NamePosition;
	Rect KillsPosition;
	Rect DeathPosition;
	

	
	// GUIStyle
	public GUIStyle titleStyle;
	public GUIStyle playerTagStyle;
	public GUIStyle bodyStyle;
	
	float originalWidth = 800;
	float originalHeight = 600;
	Vector3 scale;
	// Use this for initialization
	void Start () {
		GameObject managerObj = GameObject.Find ("Game Manager");
		manager = managerObj.GetComponent<GameManager>();
		VD = GameObject.Find("Mule").GetComponent<VictoryData>();
		
		setData();
	}
	void Update()
	{
		winnerPlayerNum = manager.getWinner ();
	}
	// Update is called once per frame
	void OnGUI () {
		scale.x = Screen.width/originalWidth; // calculate hor scale
    	scale.y = Screen.height/originalHeight; // calculate vert scale
    	scale.z = 1;
    	var svMat = GUI.matrix; // save current matrix
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		if(winnerPlayerNum == 0)
			GUI.Label(new Rect(150,100,100,100),"No Winner",titleStyle);
		else if(VD.winnerIsTeam == true)
			GUI.Label(new Rect(150,100,100,100),"Team "+winnerPlayerNum+" Victory",titleStyle);
		else
			GUI.Label(new Rect(150,100,100,100),"Player "+winnerPlayerNum+" Victory",titleStyle);
		
		displaySolo();
		displayTeam1();
		displayTeam2();
		
		SoloPCounter = 1;
		Team1PCounter = 1;
		Team2PCounter = 1;

		GUI.matrix = svMat;
	}
	void displaySolo()
	{
		if(AlbionTeamNumber == 0 || TemptressTeamNumber == 0 || MerliniTeamNumber == 0 || FanndisTeamNumber == 0 || KiritoTeamNumber == 0)
		{
			GUI.Label(new Rect(375,250,100,100),"Solo",playerTagStyle);
			GUI.Label(new Rect(400,300,100,100),"Kills",bodyStyle);
			GUI.Label(new Rect(475,300,100,100),"Deaths",bodyStyle);
			if(AlbionTeamNumber == 0)
			{
				setSoloPosition();
				GUI.Label(NamePosition,"Player "+AlbionPlayerNumber,bodyStyle);
				GUI.Label(KillsPosition,AlbionNumOfKills.ToString(),bodyStyle);
				GUI.Label(DeathPosition,AlbionNumOfDeaths.ToString(),bodyStyle);
				SoloPCounter++;
			}
			if(TemptressTeamNumber == 0)
			{
				setSoloPosition();
				GUI.Label(NamePosition,"Player "+TemptressPlayerNumber,bodyStyle);
				GUI.Label(KillsPosition,TemptressNumOfKills.ToString(),bodyStyle);
				GUI.Label(DeathPosition,TemptressNumOfDeaths.ToString(),bodyStyle);
				SoloPCounter++;
			}
			if(MerliniTeamNumber == 0)
			{
				setSoloPosition();
				GUI.Label(NamePosition,"Player "+MerliniPlayerNumber,bodyStyle);
				GUI.Label(KillsPosition,MerliniNumOfKills.ToString(),bodyStyle);
				GUI.Label(DeathPosition,MerliniNumOfDeaths.ToString(),bodyStyle);
				SoloPCounter++;
			}
			if(FanndisTeamNumber == 0)
			{
				setSoloPosition();
				GUI.Label(NamePosition,"Player "+FanndisPlayerNumber,bodyStyle);
				GUI.Label(KillsPosition,FanndisNumOfKills.ToString(),bodyStyle);
				GUI.Label(DeathPosition,FanndisNumOfDeaths.ToString(),bodyStyle);
				SoloPCounter++;
			}
			if(KiritoTeamNumber == 0)
			{
				setSoloPosition();
				GUI.Label(NamePosition,"Player "+KiritoPlayerNumber,bodyStyle);
				GUI.Label(KillsPosition,KiritoNumOfKills.ToString(),bodyStyle);
				GUI.Label(DeathPosition,KiritoNumOfDeaths.ToString(),bodyStyle);
				SoloPCounter++;
			}
		}
	}
	void setSoloPosition()
	{
		if(SoloPCounter == 1)
		{
			NamePosition = new Rect(305,350,100,100);
			KillsPosition = new Rect(415,350,100,100);
			DeathPosition = new Rect(510,350,100,100);
		}
		if(SoloPCounter == 2)
		{
			NamePosition = new Rect(305,400,100,100);
			KillsPosition = new Rect(415,400,100,100);
			DeathPosition = new Rect(510,400,100,100);
		}
		if(SoloPCounter == 3)
		{
			NamePosition = new Rect(305,450,100,100);
			KillsPosition = new Rect(415,450,100,100);
			DeathPosition = new Rect(510,450,100,100);
		}
		if(SoloPCounter == 4)
		{
			NamePosition = new Rect(305,500,100,100);
			KillsPosition = new Rect(415,500,100,100);
			DeathPosition = new Rect(510,500,100,100);
		}
	}
	
	void displayTeam1()
	{
		if(AlbionTeamNumber == 1 || TemptressTeamNumber == 1 || MerliniTeamNumber == 1 || FanndisTeamNumber == 1 || KiritoTeamNumber == 1)
		{
			GUI.Label(new Rect(100,250,100,100),"Team 1",playerTagStyle);
			GUI.Label(new Rect(115,300,100,100),"Kills",bodyStyle);
			GUI.Label(new Rect(190,300,100,100),"Deaths",bodyStyle);
			if(AlbionTeamNumber == 1)
			{
				setTeam1Position();
				GUI.Label(NamePosition,"Player "+AlbionPlayerNumber,bodyStyle);
				GUI.Label(KillsPosition,AlbionNumOfKills.ToString(),bodyStyle);
				GUI.Label(DeathPosition,AlbionNumOfDeaths.ToString(),bodyStyle);
				Team1PCounter++;
			}
			if(TemptressTeamNumber == 1)
			{
				setTeam1Position();
				GUI.Label(NamePosition,"Player "+TemptressPlayerNumber,bodyStyle);
				GUI.Label(KillsPosition,TemptressNumOfKills.ToString(),bodyStyle);
				GUI.Label(DeathPosition,TemptressNumOfDeaths.ToString(),bodyStyle);
				Team1PCounter++;
			}
			if(MerliniTeamNumber == 1)
			{
				setTeam1Position();
				GUI.Label(NamePosition,"Player "+MerliniPlayerNumber,bodyStyle);
				GUI.Label(KillsPosition,MerliniNumOfKills.ToString(),bodyStyle);
				GUI.Label(DeathPosition,MerliniNumOfDeaths.ToString(),bodyStyle);
				Team1PCounter++;
			}
			if(FanndisTeamNumber == 1)
			{
				setTeam1Position();
				GUI.Label(NamePosition,"Player "+FanndisPlayerNumber,bodyStyle);
				GUI.Label(KillsPosition,FanndisNumOfKills.ToString(),bodyStyle);
				GUI.Label(DeathPosition,FanndisNumOfDeaths.ToString(),bodyStyle);
				Team1PCounter++;
			}
			if(KiritoTeamNumber == 1)
			{
				setTeam1Position();
				GUI.Label(NamePosition,"Player "+KiritoPlayerNumber,bodyStyle);
				GUI.Label(KillsPosition,KiritoNumOfKills.ToString(),bodyStyle);
				GUI.Label(DeathPosition,KiritoNumOfDeaths.ToString(),bodyStyle);
				Team1PCounter++;
			}
		}
	}
	void setTeam1Position()
	{
		if(Team1PCounter == 1)
		{
			NamePosition = new Rect(30,350,100,100);
			KillsPosition = new Rect(130,350,100,100);
			DeathPosition = new Rect(225,350,100,100);
		}
		if(Team1PCounter == 2)
		{
			NamePosition = new Rect(30,400,100,100);
			KillsPosition = new Rect(130,400,100,100);
			DeathPosition = new Rect(225,400,100,100);
		}
		if(Team1PCounter == 3)
		{
			NamePosition = new Rect(30,450,100,100);
			KillsPosition = new Rect(130,450,100,100);
			DeathPosition = new Rect(225,450,100,100);
		}
		if(Team1PCounter == 4)
		{
			NamePosition = new Rect(30,500,100,100);
			KillsPosition = new Rect(130,500,100,100);
			DeathPosition = new Rect(225,500,100,100);
		}
	}
	
	void displayTeam2()
	{
		if(AlbionTeamNumber == 2 || TemptressTeamNumber == 2 || MerliniTeamNumber == 2 || FanndisTeamNumber == 2 || KiritoTeamNumber == 2)
		{
			GUI.Label(new Rect(550,250,100,100),"Team 2",playerTagStyle);
			GUI.Label(new Rect(575,300,100,100),"Kills",bodyStyle);
			GUI.Label(new Rect(650,300,100,100),"Deaths",bodyStyle);
			if(AlbionTeamNumber == 2)
			{
				setTeam2Position();
				GUI.Label(NamePosition,"Player "+AlbionPlayerNumber,bodyStyle);
				GUI.Label(KillsPosition,AlbionNumOfKills.ToString(),bodyStyle);
				GUI.Label(DeathPosition,AlbionNumOfDeaths.ToString(),bodyStyle);
				Team2PCounter++;
			}
			if(TemptressTeamNumber == 2)
			{
				setTeam2Position();
				GUI.Label(NamePosition,"Player "+TemptressPlayerNumber,bodyStyle);
				GUI.Label(KillsPosition,TemptressNumOfKills.ToString(),bodyStyle);
				GUI.Label(DeathPosition,TemptressNumOfDeaths.ToString(),bodyStyle);
				Team2PCounter++;
			}
			if(MerliniTeamNumber == 2)
			{
				setTeam2Position();
				GUI.Label(NamePosition,"Player "+MerliniPlayerNumber,bodyStyle);
				GUI.Label(KillsPosition,MerliniNumOfKills.ToString(),bodyStyle);
				GUI.Label(DeathPosition,MerliniNumOfDeaths.ToString(),bodyStyle);
				Team2PCounter++;
			}
			if(FanndisTeamNumber == 2)
			{
				setTeam2Position();
				GUI.Label(NamePosition,"Player "+FanndisPlayerNumber,bodyStyle);
				GUI.Label(KillsPosition,FanndisNumOfKills.ToString(),bodyStyle);
				GUI.Label(DeathPosition,FanndisNumOfDeaths.ToString(),bodyStyle);
				Team2PCounter++;
			}
			if(KiritoTeamNumber == 2)
			{
				setTeam2Position();
				GUI.Label(NamePosition,"Player "+KiritoPlayerNumber,bodyStyle);
				GUI.Label(KillsPosition,KiritoNumOfKills.ToString(),bodyStyle);
				GUI.Label(DeathPosition,KiritoNumOfDeaths.ToString(),bodyStyle);
				Team2PCounter++;
			}
		}
	}
	void setTeam2Position()
	{
		if(Team2PCounter == 1)
		{
			NamePosition = new Rect(480,350,100,100);
			KillsPosition = new Rect(590,350,100,100);
			DeathPosition = new Rect(685,350,100,100);
		}
		if(Team2PCounter == 2)
		{
			NamePosition = new Rect(480,400,100,100);
			KillsPosition = new Rect(590,400,100,100);
			DeathPosition = new Rect(685,400,100,100);
		}
		if(Team2PCounter == 3)
		{
			NamePosition = new Rect(480,450,100,100);
			KillsPosition = new Rect(590,450,100,100);
			DeathPosition = new Rect(510,450,100,100);
		}
		if(Team2PCounter == 4)
		{
			NamePosition = new Rect(480,500,100,100);
			KillsPosition = new Rect(590,500,100,100);
			DeathPosition = new Rect(685,500,100,100);
		}
	}
	
	void setData()
	{
		AlbionTeamNumber = VD.AlbionTeamNumber;
		TemptressTeamNumber = VD.TemptressTeamNumber;
		MerliniTeamNumber = VD.MerliniTeamNumber;
		FanndisTeamNumber = VD.FanndisTeamNumber;
		KiritoTeamNumber = VD.KiritoTeamNumber;
		
		AlbionNumOfDeaths = VD.AlbionNumOfDeaths;
		TemptressNumOfDeaths = VD.TemptressNumOfDeaths;
		MerliniNumOfDeaths = VD.MerliniNumOfDeaths;
		FanndisNumOfDeaths = VD.FanndisNumOfDeaths;
		KiritoNumOfKills = VD.KiritoNumOfKills;
		
		AlbionNumOfKills = VD.AlbionNumOfKills;
		TemptressNumOfKills = VD.TemptressNumOfKills;
		MerliniNumOfKills = VD.MerliniNumOfKills;
		FanndisNumOfKills = VD.FanndisNumOfKills;
		KiritoNumOfKills = VD.KiritoNumOfKills;
		
		AlbionPlayerNumber = VD.AlbionPlayerNumber;
		TemptressPlayerNumber = VD.TemptressPlayerNumber;
		MerliniPlayerNumber = VD.MerliniPlayerNumber;
		FanndisPlayerNumber = VD.FanndisPlayerNumber;
		KiritoPlayerNumber = VD.KiritoPlayerNumber;
	}
}
