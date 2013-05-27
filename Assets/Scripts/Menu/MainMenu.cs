using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	GUIContent[] comboBoxList;
	public ComboBox Player1;// = new ComboBox();
	public ComboBox Player2;
	public ComboBox Player3;
	public ComboBox Player4;
	
	GUIContent[] teamList;
	public ComboBox player1Team;
	public ComboBox player2Team;
	public ComboBox player3Team;
	public ComboBox player4Team;
	private GUIStyle listStyle = new GUIStyle();
	
	float originalWidth = 800;
	float originalHeight = 600;
	Vector3 scale;
	
	Vector2 scrollPosition;
	string selectedChamp;
	string Description;
	string SkillOne;
	string SkillTwo;
	string SkillOneDisc;
	string SkillTwoDisc;
	
	// Use this for initialization
	void Start () {
		scale.x = Screen.width/originalWidth; // calculate hor scale
    	scale.y = Screen.height/originalHeight; // calculate vert scale
    	scale.z = 1;
		comboBoxList = new GUIContent[4];
		comboBoxList[0] = new GUIContent("Albion");
		comboBoxList[1] = new GUIContent("Fanndis");
		comboBoxList[2] = new GUIContent("Merlini");
		comboBoxList[3] = new GUIContent("Temptress");
		
		teamList = new GUIContent[3];
		teamList[0] = new GUIContent("Solo");
		teamList[1] = new GUIContent("Team 1");
		teamList[2] = new GUIContent("Team 2");
		
		
 		var svMat = GUI.matrix; // save current matrix
		listStyle.normal.textColor = Color.white; 
		listStyle.onHover.background =
		listStyle.hover.background = new Texture2D(2, 2);
		listStyle.padding.left =
		listStyle.padding.right =
		listStyle.padding.top =
		listStyle.padding.bottom = 4;
 
		ComboBoxes();
		
		
		GUI.matrix = svMat;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI()
	{
		scale.x = Screen.width/originalWidth; // calculate hor scale
    	scale.y = Screen.height/originalHeight; // calculate vert scale
    	scale.z = 1;
    	var svMat = GUI.matrix; // save current matrix
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		
		PlayerSelections();
		Descriptions();
		
		Player1.Show();
		Player2.Show();
		Player3.Show();
		Player4.Show();
		
		player1Team.Show();
		player2Team.Show();
		player3Team.Show();
		player4Team.Show();
		GUI.matrix = svMat;
	}
	
	private void ComboBoxes()
	{
		Player1 = new ComboBox(new Rect(156,185,100,20), comboBoxList[0], comboBoxList, "button", "box", listStyle);
		Player2 = new ComboBox(new Rect(281,185,100,20), comboBoxList[1], comboBoxList, "button", "box", listStyle);
		Player3 = new ComboBox(new Rect(406,185,100,20), comboBoxList[2], comboBoxList, "button", "box", listStyle);
		Player4 = new ComboBox(new Rect(531,185,100,20), comboBoxList[3], comboBoxList, "button", "box", listStyle);
		Player1.setIndex(0);
		Player2.setIndex(1);
		Player3.setIndex(2);
		Player4.setIndex(3);
		
		player1Team = new ComboBox(new Rect(156,25,100,20),teamList[0],teamList,"button","box",listStyle);
		player2Team = new ComboBox(new Rect(281,25,100,20),teamList[0],teamList,"button","box",listStyle);
		player3Team = new ComboBox(new Rect(406,25,100,20),teamList[0],teamList,"button","box",listStyle);
		player4Team = new ComboBox(new Rect(531,25,100,20),teamList[0],teamList,"button","box",listStyle);
		player1Team.setIndex(0);
		player2Team.setIndex(0);
		player3Team.setIndex(0);
		player4Team.setIndex(0);
	}
	
	private void Champions()
	{
	}
	
	private void Descriptions()
	{		
		if(GUI.Button(new Rect(5,300,50,50),"Albion"))
		{
			selectedChamp = "Albion-The Hunter";
			Description = "In a big city which still held by the ordinary men, a scientist, though mutation of the his brain and mind,"+
				" tried to create a man who has the ability to master the two powers, "+
					"time and space, to face and defeat the fearful man - Marbas someday. Albion, "+
					"who was growing up without realizing that he was different from the other kids, " +
					"lived happily with his father - the scientist until the evil army attacked. His father hid him inside a " +
					"secret dungeon while his father with other ordinary men in the city facing the enemy bravely. The city fell. " +
					"Albion climbed out the dungeon and found out his father's body along with a letter clung tightly in his fist. " +
					"Albion discovered the secret. He is the one who is destined to save the world. So he decided to head to the north " +
					"- The Forsaken Universe of the North. The journey of unknown and danger begins! ";
			SkillOne = "Holy Trap";
			SkillTwo = "Holy Blink";
			SkillOneDisc = "Albion places a trap on the map that lasts until another champion gets caught in it." +
				"the champion gets stuned for 3 seconds. \nCooldown: 10 seconds";
			SkillTwoDisc = "Albion teleports 3 squares to the front direction of the hero. \nCooldown: 10 seconds";
		}
		if(GUI.Button(new Rect(60,300,50,50),"Fanndis"))
		{
			selectedChamp = "Fanndis-The Ice Queen";
			Description = "For thousand years, the Ice Tribe which was ruled by the beautiful queen - Fanndis. The ice queen lived on " +
				"the land of White peacefully. They draw their life and power from the coldness and the pureness of the ice. Until one day," +
				 "the sky became so dark that the whole land was cover by endless darkness. All of a sudden, it started to rain. People realized that" +
				  "what dropped from the sky was not rain or white snow but black ashes. Then the ice started to melt. The people of the Ice Tribe" +
				   "sensed that their power was growing weaker." +
				   "The war launched by Marbas was approaching. In order to clear the barrier of his army's advance, he ordered his evil army to " +
				   "burn down the whole sacred forest. And the giant fire had been lasting for months. The temperature of the world increased " +
				   "sharply within months. Fanndis realized that the only way for her tribe to survive was to master the power of space manipulation," +
				    "and move the water in Black ocean to extinguish this forest fire. Only then, she can not only save her tribe but also stop the " +
				    "advance of the evil army. Heading to the North became her only choice.";
			SkillOne = "Zero Friction";
			SkillTwo = "Ice Age";
			SkillOneDisc = "Fanndis increases her speed for 5 seconds. \nCooldown: 10 seconds";
			SkillTwoDisc = "Fanndis compresses the water vapor in the air to an impassible wall of ice to block all movement and explosion." +
				"\nCooldown: 3 seconds";
		}
		if(GUI.Button(new Rect(5,355,50,50),"Merlini"))
		{
			selectedChamp = "Merlini-The Magician";
			Description = "Merlini, although he is completely aware of his magical power, isn't quite sure where he came from." +
			 	"He had parents claiming to be his biological parents early in life, but soon was adopted by another couple who " +
			 	"claimed that he is an orphan in the first place. His origin remains a mystery for him and his friends. He doesn't" +
			 	 "even know where his magical power come from - his supposedly biological parents said he was possessed " +
			 	 "thus gave him away to other parents. His latter parents, however, said he had anomalies in his body not quite " +
			 	 "detectable by current technology. Some other sources told him he had an accident early in life, causing it to be " +
			 	 "hard for him to remember things but altering his mind to be so powerful such that it can affect reality. Regardless, " +
			 	 "this lost mysterious soul is magical." +
			 	 "Despite of this, Merlini can't use and practice his magic skills freely in his home town." +
			 	  "With his passion and desire to improve and advance his skills, he decided to board to a place that his magical" +
			 	   "mind seems to be telling him to go to for some reason. Maybe he's needed in that place, or maybe that place has" +
			 	    "something to do with his origin, aside from he can practice his skills freely. Thus, he departed for this place " +
			 	    "called FUN for fun with no pun intended.";
			SkillOne = "Hammer Time";
			SkillTwo = "Bomb Voyage";
			SkillOneDisc = "Merlini sends a hammer in the direction he's facing and stuns" +
			 	"any nearby opponent for 2 seconds. \nCooldown: 10 seconds";
			SkillTwoDisc = "Merlini places a standard bomb below every player of the map. \nCooldown: 25 seconds";
		}
		if(GUI.Button(new Rect(60,355,50,50),"Temptress"))
		{
			selectedChamp = "Temptress-The Misguided";
			Description = "Integration of the embodiment of evil and elegant, Temptress playing with " +
				"her enchanting grace to lure the innocent and greedy adventures strayed into her zone of " +
				"darkness. In the dark secrets of the enigmatic under the guise, no one has ever escaped from her clutches." +
				"Temptress was born in a peaceful village. One day, the mysterious man claiming to be the demon, Marbas, " +
				"destroyed entire village, with the exception of Temptress. She was taken to his kingdom that mutated her" +
				 "original innocence mind into a devil mind. Temptress struggled to escape from the kingdom. She had heard " +
				 "that Forsaken Universe of the North is filled with a variety of similar to her life exists that is not " +
				 "subject to any implication. Thus, she escaped from the kingdom and embarks on a journey to Forsaken Universe of the North.";
			SkillOne = "Lure";
			SkillTwo = "Love Struck";
			SkillOneDisc = "Temptress pulls an opponent within 5 units to your current location. \nCooldown: 5 seconds";
			SkillTwoDisc = "Temptress applies slow-down for 2 seconds to nearest enemy in 3 squares. \nCooldown: 10 seconds";
		}
		
		GUILayout.BeginArea(new Rect(125,325,500,300));
		scrollPosition = GUILayout.BeginScrollView(scrollPosition,GUILayout.Width(500),GUILayout.Height(300));
		GUI.skin.box.wordWrap = true;
		GUILayout.Box(Description);
		GUILayout.EndScrollView();
		GUILayout.EndArea();
		
		GUI.Label(new Rect(128,300,200,100),selectedChamp);
		GUI.Label(new Rect(600,300,200,100),SkillOne);
		//GUI.Label(new Rect(400,400,200,100),SkillTwo);
		//GUI.Label(new Rect(190,420,200,100),SkillOneDisc);
		//GUI.Label(new Rect(400,420,200,100),SkillTwoDisc);
	}
	
	private void PlayerSelections()
	{
		GUI.Box(new Rect(156,120,100,60),"Player 1");
		GUI.Box(new Rect(281,120,100,60),"Player 2");
		GUI.Box(new Rect(406,120,100,60),"Player 3");
		GUI.Box(new Rect(531,120,100,60),"Player 4");
	}
}
