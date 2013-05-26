using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	GUIContent[] comboBoxList;
	public ComboBox Player1;// = new ComboBox();
	public ComboBox Player2;
	public ComboBox Player3;
	public ComboBox Player4;
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
 		var svMat = GUI.matrix; // save current matrix
		listStyle.normal.textColor = Color.white; 
		listStyle.onHover.background =
		listStyle.hover.background = new Texture2D(2, 2);
		listStyle.padding.left =
		listStyle.padding.right =
		listStyle.padding.top =
		listStyle.padding.bottom = 4;
 
		Player1 = new ComboBox(new Rect(15, 170, 150, 20), comboBoxList[0], comboBoxList, "button", "box", listStyle);
		Player2 = new ComboBox(new Rect(15,470,150, 20), comboBoxList[1], comboBoxList, "button", "box", listStyle);
		Player3 = new ComboBox(new Rect(635,170,150, 20), comboBoxList[2], comboBoxList, "button", "box", listStyle);
		Player4 = new ComboBox(new Rect(635,470,150, 20), comboBoxList[3], comboBoxList, "button", "box", listStyle);
		
		Player1.setIndex(0);
		Player2.setIndex(1);
		Player3.setIndex(2);
		Player4.setIndex(3);
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
		GUI.Box(new Rect(15,20,150,140),"Player 1");
		GUI.Box(new Rect(15,320,150,140),"Player 2");
		GUI.Box(new Rect(635,20,150,140),"Player 3");
		GUI.Box(new Rect(635,320,150,140),"Player 4");
		
		GUI.Box(new Rect(185,20,430,170),"Champions");
		GUI.Box(new Rect(185,225,430,270),"Description");
		
		if(GUI.Button(new Rect(190,60,100,100),"Albion"))
		{
			selectedChamp = "Albion-The Hunter";
			Description = "In a big city which still held by the ordinary men, a scientist, though mutation of the his brain and mind,"+
				" tried to create a man who has the ability to master the two powers, "+
					"time and space, to face and defeat the fearful man – Marbas someday. Albion, "+
					"who was growing up without realizing that he was different from the other kids, " +
					"lived happily with his father – the scientist until the evil army attacked. His father hid him inside a " +
					"secret dungeon while his father with other ordinary men in the city facing the enemy bravely. The city fell. " +
					"Albion climbed out the dungeon and found out his father’s body along with a letter clung tightly in his fist. " +
					"Albion discovered the secret. He is the one who is destined to save the world. So he decided to head to the north " +
					"– The Forsaken Universe of the North. The journey of unknown and danger begins! ";
			SkillOne = "Holy Trap";
			SkillTwo = "Holy Blink";
			SkillOneDisc = "Albion places a trap on the map that lasts until another champion gets caught in it." +
				"the champion gets stuned for 3 seconds. Cooldown: 50 seconds";
			SkillTwoDisc = "Albion teleports 3 squares to the front direction of the hero. Cooldown: 30 seconds";
		}
		if(GUI.Button(new Rect(296,60,100,100),"Fanndis"))
		{
			selectedChamp = "Fanndis-The Ice Queen";
			Description = "For thousand years, the Ice Tribe which was ruled by the beautiful queen – Fanndis. The ice queen lived on " +
				"the land of White peacefully. They draw their life and power from the coldness and the pureness of the ice. Until one day," +
				 "the sky became so dark that the whole land was cover by endless darkness. All of a sudden, it started to rain. People realized that" +
				  "what dropped from the sky was not rain or white snow but black ashes. Then the ice started to melt. The people of the Ice Tribe" +
				   "sensed that their power was growing weaker." +
				   "The war launched by Marbas was approaching. In order to clear the barrier of his army’s advance, he ordered his evil army to " +
				   "burn down the whole sacred forest. And the giant fire had been lasting for months. The temperature of the world increased " +
				   "sharply within months. Fanndis realized that the only way for her tribe to survive was to master the power of space manipulation," +
				    "and move the water in Black ocean to extinguish this forest fire. Only then, she can not only save her tribe but also stop the " +
				    "advance of the evil army. Heading to the North became her only choice.";
			SkillOne = "Zero Friction";
			SkillTwo = "Ice Age";
			SkillOneDisc = "Fanndis increases her speed for 5 seconds. Cooldown: 10 seconds";
			SkillTwoDisc = "Fanndis compresses the water vapor in the air to an impassible wall of ice to block all movement and explosion." +
				"Cooldown: 10 seconds";
		}
		if(GUI.Button(new Rect(403,60,100,100),"Merlini"))
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
			 	"any nearby opponent for 2 seconds. Cooldown: 10 seconds";
			SkillTwoDisc = "Merlini places a standard bomb below every player of the map. Cooldown: 25 seconds";
		}
		if(GUI.Button(new Rect(509,60,100,100),"Temptress"))
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
			SkillOneDisc = "Temptress pulls an opponent within 5 units to your current location. Cooldown: 30 seconds";
			SkillTwoDisc = "Temptress applies slow-down for 2 seconds to nearest enemy in 3 squares. Cooldown: 45 seconds";
		}
		GUI.Label(new Rect(190,250,200,100),selectedChamp);
		
		GUILayout.BeginArea(new Rect(190,270,425,125));
		scrollPosition = GUILayout.BeginScrollView(scrollPosition,GUILayout.Width(425),GUILayout.Height(125));
		GUI.skin.box.wordWrap = true;
		GUILayout.Box(Description);
		GUILayout.EndScrollView();
		GUILayout.EndArea();
		
		//GUI.Label(new Rect(190,270,425,125),Description);
		GUI.Label(new Rect(190,400,200,100),SkillOne);
		GUI.Label(new Rect(400,400,200,100),SkillTwo);
		GUI.Label(new Rect(190,420,200,100),SkillOneDisc);
		GUI.Label(new Rect(400,420,200,100),SkillTwoDisc);
		Player1.Show();
		Player2.Show();
		Player3.Show();
		Player4.Show();
		GUI.matrix = svMat;
	}
}
