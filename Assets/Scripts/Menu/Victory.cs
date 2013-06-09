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
	
	XInputController controllerOne;
	
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
		winnerPlayerNum = VD.winnerNum;
		controllerOne = GameObject.Find("VictoryManager").GetComponent<XInputController>();
		
		setData();
	}
	void Update()
	{
		if(controllerOne.GetButtonPressed("dropbomb"))
		{
			Application.LoadLevel(1);
		}
	}
	// Update is called once per frame
	void OnGUI () {
		scale.x = Screen.width/originalWidth; // calculate hor scale
    	scale.y = Screen.height/originalHeight; // calculate vert scale
    	scale.z = 1;
    	var svMat = GUI.matrix; // save current matrix
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		if(winnerPlayerNum == 0)
			GUI.Label(new Rect(250,100,100,100),"No Winner!",titleStyle);
		else if(VD.winnerIsTeam == true)
			GUI.Label(new Rect(195,100,100,100),"Team "+winnerPlayerNum+" Victory!",titleStyle);
		else
			GUI.Label(new Rect(195,100,100,100),"Player "+winnerPlayerNum+" Victory!",titleStyle);
		
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
			GUI.Label(new Rect(349,225,100,100),"Solo",playerTagStyle);
			GUI.Label(new Rect(368,275,100,100),"Kills",bodyStyle);
			GUI.Label(new Rect(448,275,100,100),"Deaths",bodyStyle);
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
			NamePosition = new Rect(270,325,100,100);
			KillsPosition = new Rect(390,325,100,100);
			DeathPosition = new Rect(480,325,100,100);
		}
		if(SoloPCounter == 2)
		{
			NamePosition = new Rect(270,375,100,100);
			KillsPosition = new Rect(390,375,100,100);
			DeathPosition = new Rect(480,375,100,100);
		}
		if(SoloPCounter == 3)
		{
			NamePosition = new Rect(270,425,100,100);
			KillsPosition = new Rect(390,425,100,100);
			DeathPosition = new Rect(480,425,100,100);
		}
		if(SoloPCounter == 4)
		{
			NamePosition = new Rect(270,475,100,100);
			KillsPosition = new Rect(390,475,100,100);
			DeathPosition = new Rect(480,475,100,100);
		}
	}
	
	void displayTeam1()
	{
		if(AlbionTeamNumber == 1 || TemptressTeamNumber == 1 || MerliniTeamNumber == 1 || FanndisTeamNumber == 1 || KiritoTeamNumber == 1)
		{
			GUI.Label(new Rect(83,225,100,100),"Team 1",playerTagStyle);
			GUI.Label(new Rect(102,275,100,100),"Kills",bodyStyle);
			GUI.Label(new Rect(170,275,100,100),"Deaths",bodyStyle);
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
			NamePosition = new Rect(5,325,100,100);
			KillsPosition = new Rect(120,325,100,100);
			DeathPosition = new Rect(200,325,100,100);
		}
		if(Team1PCounter == 2)
		{
			NamePosition = new Rect(5,375,100,100);
			KillsPosition = new Rect(120,375,100,100);
			DeathPosition = new Rect(200,375,100,100);
		}
		if(Team1PCounter == 3)
		{
			NamePosition = new Rect(5,425,100,100);
			KillsPosition = new Rect(120,425,100,100);
			DeathPosition = new Rect(200,425,100,100);
		}
		if(Team1PCounter == 4)
		{
			NamePosition = new Rect(5,475,100,100);
			KillsPosition = new Rect(120,475,100,100);
			DeathPosition = new Rect(200,475,100,100);
		}
	}
	
	void displayTeam2()
	{
		if(AlbionTeamNumber == 2 || TemptressTeamNumber == 2 || MerliniTeamNumber == 2 || FanndisTeamNumber == 2 || KiritoTeamNumber == 2)
		{
			GUI.Label(new Rect(616,225,100,100),"Team 2",playerTagStyle);
			GUI.Label(new Rect(647,275,100,100),"Kills",bodyStyle);
			GUI.Label(new Rect(715,275,100,100),"Deaths",bodyStyle);
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
			NamePosition = new Rect(545,325,100,100);
			KillsPosition = new Rect(667,325,100,100);
			DeathPosition = new Rect(745,325,100,100);
		}
		if(Team2PCounter == 2)
		{
			NamePosition = new Rect(545,375,100,100);
			KillsPosition = new Rect(667,375,100,100);
			DeathPosition = new Rect(745,375,100,100);
		}
		if(Team2PCounter == 3)
		{
			NamePosition = new Rect(545,425,100,100);
			KillsPosition = new Rect(667,425,100,100);
			DeathPosition = new Rect(745,425,100,100);
		}
		if(Team2PCounter == 4)
		{
			NamePosition = new Rect(545,475,100,100);
			KillsPosition = new Rect(667,475,100,100);
			DeathPosition = new Rect(745,475,100,100);
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
