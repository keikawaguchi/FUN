using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour {
	
	float startPosition = 100f;
	
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
	
	
	//Fonts
	public Font titleFont;
	public Font bodyFont;
	
	// GUIStyle
	private GUIStyle titleStyle;
	private GUIStyle playerTagStyle;
	private GUIStyle bodyStyle;
	
	float originalWidth = 800;
	float originalHeight = 600;
	Vector3 scale;
	// Use this for initialization
	void Start () {
		titleStyle = new GUIStyle();
		playerTagStyle = new GUIStyle();
		bodyStyle = new GUIStyle();
		GameObject managerObj = GameObject.Find ("Game Manager");
		manager = managerObj.GetComponent<GameManager>();
		VD = GameObject.Find("Mule").GetComponent<VictoryData>();
		
		setData();
		setGUIStyle();
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
		
		GUI.Label(new Rect(100,100,100,100),winnerPlayerNum+" Victory",titleStyle);
		
		if(AlbionTeamNumber == 0 || TemptressTeamNumber == 0 || MerliniTeamNumber == 0 || FanndisTeamNumber == 0 || KiritoTeamNumber==0)
		{
			startPosition+=50;
			GUI.Label(new Rect(100,startPosition,100,100),"Solo",playerTagStyle);
			startPosition+=10;
			if(AlbionTeamNumber == 0)
			{
				GUI.Label(new Rect(100,startPosition,100,100),"Player "+AlbionPlayerNumber,bodyStyle);
				startPosition+=10;
			}
			if(TemptressTeamNumber == 0)
			{
				GUI.Label(new Rect(100,startPosition,100,100),"Player "+TemptressPlayerNumber,bodyStyle);
				startPosition+=10;
			}
			if(MerliniTeamNumber == 0)
			{
				GUI.Label(new Rect(100,startPosition,100,100),"Player "+MerliniPlayerNumber,bodyStyle);
				startPosition+=10;
			}
			if(FanndisTeamNumber == 0)
			{
				GUI.Label(new Rect(100,startPosition,100,100),"Player "+FanndisPlayerNumber,bodyStyle);
				startPosition+=10;
			}
			if(KiritoTeamNumber == 0)
			{
				GUI.Label(new Rect(100,startPosition,100,100),"Player "+KiritoPlayerNumber,bodyStyle);
				startPosition+=10;
			}
		}
		
		GUI.matrix = svMat;
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
	
	private void setGUIStyle() {
		
		// font style
		titleStyle.font = titleFont;
		playerTagStyle.font = bodyFont;
		bodyStyle.font = bodyFont;
		
		// font size
		titleStyle.fontSize = 50;
		playerTagStyle.fontSize = 30;
		bodyStyle.fontSize = 15;
		
		// font color
		titleStyle.normal.textColor = new Color(255f, 128f, 0f, 100f);
		playerTagStyle.normal.textColor = Color.cyan;
		bodyStyle.normal.textColor = Color.white;
		
		// wrap the text
		bodyStyle.wordWrap = true;
	}
}
