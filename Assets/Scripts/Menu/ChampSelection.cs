using UnityEngine;
using System.Collections;

public class ChampSelection : MonoBehaviour {
	// team texture path
	private const string SOLO_IMAGE_PATH = "Textures/Menu/Solo";
	private const string TEAM1_IMAGE_PATH = "Textures/Menu/Team1";
	private const string TEAM2_IMAGE_PATH = "Textures/Menu/Team2";
	
	// champion texture path
	private const string CHAMP_BOX_IMAGE_PATH = "Textures/Menu/ChampBox";
	private const string ALBION_IMAGE_PATH = "Textures/Champions/AlbionIcon";
	private const string FANNDIS_IMAGE_PATH = "Textures/Champions/FanndisIcon";
	private const string KIRITO_IMAGE_PATH = "Textures/Champions/KiritoIcon";
	private const string MERLINI_IMAGE_PATH = "Textures/Champions/MerliniIcon";
	private const string TEMPTRESS_IMAGE_PATH = "Textures/Champions/TemptressIcon";
	
	// confirmation texture path
	private const string CONFIRM_IMAGE_PATH = "Textures/Menu/Confirm";
	private const string CONFIRMED_IMAGE_PATH = "Textures/Menu/Confirmed";
	
	private const int MAX_TEAM_TYPES = 3;
	private const int MAX_PLAYERS = 4;
	private const int TOTAL_AVAILABLE_CHAMPS = 5;
	
	// Fonts
	public Font titleFont;
	public Font bodyFont;
	
	// GUIStyle
	private GUIStyle titleStyle;
	private GUIStyle playerTagStyle;
	private GUIStyle bodyStyle;
	
	
	// selected info
	private Texture2D[] teamTextures;
	private Texture2D[] champTextures;
	private string[] champNames;
	private string[] champSkillsDescrip;
	
	private Texture2D confirm;
	private Texture2D confirmed;
	
	// scaling
	private Vector3 scale;
	private float originalWidth = 800f;
	private float originalHeight = 600f;
	
	// controllers
	private XInputController[] controllers;
	
	// player selection info
	private bool[] isPlayerInRoom;  // is the player in the room?
	private bool[] confirmButtonPressed;  // player confirmed champion selection
	private bool[] navigateLeft;
	private bool[] navigateRight;
	private bool[] navigateUp;
	private bool[] navigateDown;
	private bool[] defaultChampViewed;
	private string[] playersSelectedChamps;  // the selected champion to be played
	
	private ChampInfo champInfo;
	
	// current selected info
	private int[] currentSelectedChampIndex;
	private int[] currentSelectedTeamIndex;
	private Texture2D[] displayedChampTexture;
	private string[] displayedChampName;
	private string[] displayedChamSkillsDesctip;
	private Texture2D[] displayedTeamTexture;
	
	// save the selected champions and teams
	private PlayerControls saveSelection;
	
	// variables for checking conditions
	private int numOfConfirmedPlayers;
	private int numOfJoinedPlayers;
	private bool[] buttonXPressed;
	private bool[] buttonYPressed;
	
	// Use this for initialization
	void Start () {
		initializeVariables ();
		
		loadScripts ();
		loadTextures ();
		
		setControllers ();
		
		// construct champions info
		setChampNames ();
		setSkillsDescrip ();
		
		setDefaultChampInfo ();
	}
	
	void Update() {
		int currentLevel = Application.loadedLevel;
		
		if (controllers[1].GetButtonPressed ("dropbomb") && numOfConfirmedPlayers > 0 &&
			numOfConfirmedPlayers == numOfJoinedPlayers) {  // next
			saveSelectionInfo ();
			
			Application.LoadLevel(++currentLevel);
		}
		else if (controllers[1].GetButtonPressed ("skill2"))  // back
			Application.LoadLevel(--currentLevel);
	}
	
	private void OnGUI() {
		setPlayerJoined ();
		setSelectionConfirmed ();
		navigateState ();
		
		if (titleFont != null && bodyFont != null) {
			setGUIStyle ();
		
			// begin scaling the contents
			scale.x = Screen.width/originalWidth;  // calculate hor scale
	    	scale.y = Screen.height/originalHeight;  // calculate vert scale
	    	scale.z = 1;
	    	var saveMatrix = GUI.matrix;  // save current matrix
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
			
			displayLayout ();  // display the whole champion selection layout
			
			GUI.matrix = saveMatrix;
			// end scaling the contents
		}
	}
	
	private void initializeVariables() {
		titleStyle = new GUIStyle();
		playerTagStyle = new GUIStyle();
		bodyStyle = new GUIStyle();
		
		// index 0 is not used
		controllers = new XInputController[MAX_PLAYERS + 1];
		confirmButtonPressed = new bool[MAX_PLAYERS + 1];
		isPlayerInRoom = new bool[MAX_PLAYERS + 1];
		defaultChampViewed = new bool[MAX_PLAYERS + 1];
		navigateLeft = new bool[MAX_PLAYERS + 1];
		navigateRight = new bool[MAX_PLAYERS + 1];
		navigateUp = new bool[MAX_PLAYERS + 1];
		navigateDown = new bool[MAX_PLAYERS + 1];
		
		teamTextures = new Texture2D[MAX_TEAM_TYPES];
		champNames = new string[TOTAL_AVAILABLE_CHAMPS + 1];
		champTextures = new Texture2D[TOTAL_AVAILABLE_CHAMPS + 1];  // 0 is for default view
		champSkillsDescrip = new string[TOTAL_AVAILABLE_CHAMPS + 1];
		
		currentSelectedChampIndex = new int[MAX_PLAYERS + 1];
		currentSelectedTeamIndex = new int[MAX_PLAYERS + 1];
		displayedChampTexture = new Texture2D[MAX_PLAYERS + 1];
		displayedChampName = new string[MAX_PLAYERS + 1];
		displayedChamSkillsDesctip = new string[MAX_PLAYERS + 1];
		displayedTeamTexture = new Texture2D[MAX_PLAYERS + 1];
		
		buttonXPressed = new bool[MAX_PLAYERS + 1];
		buttonYPressed = new bool[MAX_PLAYERS + 1];
	}
	
	private void loadScripts() {
		for (int i = 1; i <= MAX_PLAYERS; i++)
			controllers[i] = gameObject.AddComponent<XInputController>();
		
		champInfo = gameObject.AddComponent<ChampInfo>();
		saveSelection = GameObject.Find("Controls").GetComponent<PlayerControls>();
	}
	
	private void loadTextures() {
		teamTextures[0] = Resources.Load (SOLO_IMAGE_PATH) as Texture2D;
		teamTextures[1] = Resources.Load (TEAM1_IMAGE_PATH) as Texture2D;
		teamTextures[2] = Resources.Load (TEAM2_IMAGE_PATH) as Texture2D;
		
		champTextures[0] = Resources.Load (CHAMP_BOX_IMAGE_PATH) as Texture2D;
		champTextures[1] = Resources.Load (ALBION_IMAGE_PATH) as Texture2D;
		champTextures[2] = Resources.Load (FANNDIS_IMAGE_PATH) as Texture2D;
		champTextures[3] = Resources.Load (KIRITO_IMAGE_PATH) as Texture2D;
		champTextures[4] = Resources.Load (MERLINI_IMAGE_PATH) as Texture2D;
		champTextures[5] = Resources.Load (TEMPTRESS_IMAGE_PATH) as Texture2D;
		
		confirm = Resources.Load (CONFIRM_IMAGE_PATH) as Texture2D;
		confirmed = Resources.Load (CONFIRMED_IMAGE_PATH) as Texture2D;
	}
	
	private void setChampNames() {
		champNames[1] = "Albion, the Hunter";
		champNames[2] = "Fanndis, the Ice Queen";
		champNames[3] = "Kirito, the Ninja Assassin";
		champNames[4] = "Merlini, the Magician";
		champNames[5] = "Temptress, the Misguided";
	}
	
	private void setSkillsDescrip() {
		champSkillsDescrip[1] = "Holy Trap - CD: 10 sec\n" +
			"Albion places a trap on the map that is visible for 1 second " +
			"by everyone and stun whoever walks through it except the champion who places the trap.\n\n" +
			"Holy Blink - CD: 10 sec\n" +
			"Albion teleports 3 squares to the front direction of the hero.";
		champSkillsDescrip[2] = "Zero Friction - CD: 10 sec\n" +
			"Fanndis sprints herself for 5 seconds.\n\n" +
			"Ice Age - CD: 3 sec\n" +
			"Fanndis compresses the water vapor in the air to an impassible mountain of ice to " +
			"block all movement and explosion; and slow down opponents by 50% for 2 seconds.";
		champSkillsDescrip[3] = "Suterusu - CD: 10 sec\n" +
			"Kirito becomes invisible and invincible for 1 seconds.\n\n" +
			"Chinmoku - CD: 15 sec\n" +
			"Kirito uses his Kiko to silence all the opponents within 3 units of his current " +
			"location where the effect stays for 5 seconds on the opponents.";
		champSkillsDescrip[4] = "Hammer Time - CD: 10 sec\n" +
			"Merlini sends a hammer in the direction he's facing and stuns any nearby opponent for " +
			"2 seconds.\n\n" +
			"Bomb Voyage - CD: 25 sec\n" +
			"Merlini places a standard bomb below every player of the map.";
		champSkillsDescrip[5] = "Lure - CD: 5 sec\n" +
			"Temptress pulls an opponent within 5 units to your current location.\n\n" +
			"Love Struck - CD: 3 sec\n" +
			"Temptress applies slow-down for 2 seconds to nearest enemy in 3 squares.";
	}
	
	private void setControllers() {
		for (int i = 1; i <= MAX_PLAYERS; i++) {
			controllers[i].SetControllerNumber (i);
			Debug.Log ("Controller" + (i) + " #: " + controllers[i].GetControllerNumber ());
		}
	}
	
	private void setDefaultChampInfo() {
		// the first champ to shown when join the room
		for (int i = 0; i <= MAX_PLAYERS; i++) {
			currentSelectedChampIndex[i] = 0;
			currentSelectedTeamIndex[i] = 0;
			displayedChampTexture[i] = champTextures[1];
			displayedChampName[i] = champNames[1];
			displayedChamSkillsDesctip[i] = champSkillsDescrip[1];
			displayedTeamTexture[i] = teamTextures[0];
		}
	}
	
	private void setGUIStyle() {
		// font style
		titleStyle.font = titleFont;
		playerTagStyle.font = bodyFont;
		bodyStyle.font = bodyFont;
		
		// font size
		titleStyle.fontSize = 50;
		playerTagStyle.fontSize = 20;
		bodyStyle.fontSize = 15;
		
		// font color
		titleStyle.normal.textColor = new Color(255f, 128f, 0f, 100f);
		playerTagStyle.normal.textColor = Color.cyan;
		bodyStyle.normal.textColor = Color.white;
		
		// wrap the text
		bodyStyle.wordWrap = true;
	}
	
	private void displayLayout() {
		// title
		string title = "Champions Selection";
		GUI.Label (new Rect(150f, 0f, 0f, 0f), "Select Champions", titleStyle);
		
		// Player 1 group
		GUI.BeginGroup (new Rect(35f, 80f, 500f, 500f));  // make a group
		
		displayChampionContents (controllers[1]);
		
		GUI.EndGroup ();  // end the group
		
		// Player 2 group
		GUI.BeginGroup (new Rect(35f, 300f, 500f, 500f));  // make a group
		
		displayChampionContents (controllers[2]);
		
		GUI.EndGroup ();  // end the group
		
		// Player 3 group
		GUI.BeginGroup (new Rect(425f, 80f, 500f, 500f));  // make a group
		
		displayChampionContents (controllers[3]);
		
		GUI.EndGroup ();  // end the group
		
		// Player 4 group
		GUI.BeginGroup (new Rect(425f, 300f, 500f, 500f));  // make a group
		
		displayChampionContents (controllers[4]);
		
		GUI.EndGroup ();  // end the group
	}
	
	private void displayChampionContents(XInputController controller) {
		/*
		 * Conditions:
		 * 1. No duplicates allowed
		 */
		int controllerNum = controller.GetControllerNumber ();
		if (controllerNum == 1)
			playerTagStyle.normal.textColor = Color.cyan;
		
		GUI.Label (new Rect(0f, 0f, 0f, 0f), "Player " + controllerNum, playerTagStyle);  // player label
		
		displayChampInfo (controller);
		displayTeamInfo (controller);
		displayConfirmationButton (controller);
	}
	
	private void displayChampInfo(XInputController controller) {
		int controllerNum = controller.GetControllerNumber ();
		Texture2D champTextureBox = displayedChampTexture[controllerNum];
		string champNameLabel = displayedChampName[controllerNum];
		string champSkillsBox = displayedChamSkillsDesctip[controllerNum];
		
		if (!isPlayerInRoom[controllerNum]) {
			champTextureBox = champTextures[0];
			champNameLabel = "";
			champSkillsBox = "";
		}
		else {
			if (currentSelectedChampIndex[controllerNum] == 0)
				currentSelectedChampIndex[controllerNum] = 1;
			int index = currentSelectedChampIndex[controllerNum];
			
			if (navigateLeft[controllerNum]) {
				index--;
				if (index < 1)
					index = TOTAL_AVAILABLE_CHAMPS;
				
				currentSelectedChampIndex[controllerNum] = index;
				
				// update champion texture
				displayedChampTexture[controllerNum] = champTextures[index];
				champTextureBox = displayedChampTexture[controllerNum];
				
				// update champion name
				displayedChampName[controllerNum] = champNames[index];
				champNameLabel = displayedChampName[controllerNum];
				
				// update chamption skills description
				displayedChamSkillsDesctip[controllerNum] = champSkillsDescrip[index];
				champSkillsBox = displayedChamSkillsDesctip[controllerNum];
				
				navigateLeft[controllerNum] = false;
			}
			else if (navigateRight[controllerNum]) {
				index++;
				index = index % TOTAL_AVAILABLE_CHAMPS;
				if (index == 0)
					index = TOTAL_AVAILABLE_CHAMPS;
				
				currentSelectedChampIndex[controllerNum] = index;
				
				// update champion texture
				displayedChampTexture[controllerNum] = champTextures[index];
				champTextureBox = displayedChampTexture[controllerNum];
				
				// update champion name
				displayedChampName[controllerNum] = champNames[index];
				champNameLabel = displayedChampName[controllerNum];
				
				// update chamption skills description
				displayedChamSkillsDesctip[controllerNum] = champSkillsDescrip[index];
				champSkillsBox = displayedChamSkillsDesctip[controllerNum];
				
				navigateRight[controllerNum] = false;
			}
		}
		
		GUI.Box (new Rect (0f, 30f, 100f, 100f), champTextureBox);  // show champion texture
		GUI.Label (new Rect(110f, 0f, 0f, 0f), champNameLabel, playerTagStyle);  // show champion name
		GUI.Box (new Rect(110f, 30f, 250f, 200f), champSkillsBox, bodyStyle);  // show champion skill description
	}
	
	private void displayTeamInfo(XInputController controller) {
		int controllerNum = controller.GetControllerNumber ();
		Texture2D teamLabel = displayedTeamTexture[controllerNum];
		int index = currentSelectedTeamIndex[controllerNum];
		
		if (!isPlayerInRoom[controllerNum]) {
			teamLabel = null;
		}
		else {
			if (navigateUp[controllerNum]) {
				index--;
				if (index < 0)
					index = MAX_TEAM_TYPES - 1;  // Team 2 is index 2 not 3
				
				Debug.Log ("Current Index: " + index);
				currentSelectedTeamIndex[controllerNum] = index;  // here is the issue!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
				Debug.Log ("Pointed Index: " + currentSelectedTeamIndex[controllerNum]);
				
				displayedTeamTexture[controllerNum] = teamTextures[index];
				teamLabel = displayedTeamTexture[controllerNum];
				
				navigateUp[controllerNum] = false;
			}
			else if (navigateDown[controllerNum]) {
				index++;
				index %= MAX_TEAM_TYPES;
				if (index < 0)
					index = MAX_TEAM_TYPES - 1;  // Team 2 is index 2 not 3
				
				currentSelectedTeamIndex[controllerNum] = index;
				
				displayedTeamTexture[controllerNum] = teamTextures[index];
				teamLabel = displayedTeamTexture[controllerNum];
				
				navigateDown[controllerNum] = false;
			}
			
			GUI.Label (new Rect(0f, 130f, 100f, 50f), teamLabel);  // show team info
		}
	}
	
	private void displayConfirmationButton(XInputController controller) {
		int controllerNum = controller.GetControllerNumber ();
		Texture2D confirmTexture;
		
		if (!isPlayerInRoom[controllerNum]) {
			confirmTexture = null;
		}
		else {
			if (!confirmButtonPressed[controllerNum]) {
				confirmTexture = confirm;
			}
			else {
				confirmTexture = confirmed;
			}
		}
		
		GUI.Label (new Rect(0f, 160f, 100f, 50f), confirmTexture);  // show confirmation status
	}
	
	private void setPlayerJoined() {
		// don't need a loop to set this to waste time complexity
		if (controllers[1].GetButtonPressed ("skill3") && !buttonYPressed[1]) {
			isPlayerInRoom[1] = true;
			buttonYPressed[1] = true;
			numOfJoinedPlayers++;
			Debug.Log ("Player 1 joined");
		}
		if (controllers[2].GetButtonPressed ("skill3") && !buttonYPressed[2]) {
			isPlayerInRoom[2] = true;
			buttonYPressed[2] = true;
			numOfJoinedPlayers++;
			Debug.Log ("Player 2 joined");
		}
		if (controllers[3].GetButtonPressed ("skill3") && !buttonYPressed[3]) {
			isPlayerInRoom[3] = true;
			buttonYPressed[3] = true;
			numOfJoinedPlayers++;
			Debug.Log ("Player 3 joined");
		}
		if (controllers[4].GetButtonPressed ("skill3") && !buttonYPressed[4]) {
			isPlayerInRoom[4] = true;
			buttonYPressed[4] = true;
			numOfJoinedPlayers++;
			Debug.Log ("Player 4 joined");
		}
	}
	
	private void setSelectionConfirmed() {
		if (controllers[1].GetButtonPressed ("skill1") && isPlayerInRoom[1] && !buttonXPressed[1]) {
			confirmButtonPressed[1] = true;
			buttonXPressed[1] = true;
			numOfConfirmedPlayers++;
		}
		if (controllers[2].GetButtonPressed ("skill1") && isPlayerInRoom[2] && !buttonXPressed[2]) {
			confirmButtonPressed[2] = true;
			buttonXPressed[2] = true;
			numOfConfirmedPlayers++;
		}
		if (controllers[3].GetButtonPressed ("skill1") && isPlayerInRoom[3] && !buttonXPressed[3]) {
			confirmButtonPressed[3] = true;
			buttonXPressed[3] = true;
			numOfConfirmedPlayers++;
		}
		if (controllers[4].GetButtonPressed ("skill1") && isPlayerInRoom[4] && !buttonXPressed[4]) {
			confirmButtonPressed[4] = true;
			buttonXPressed[4] = true;
			numOfConfirmedPlayers++;
		}
	}
	
	private void navigateState() {
		// left
		if (controllers[1].GetThumbstickDirectionOnce ("left") && isPlayerInRoom[1] && !confirmButtonPressed[1])
			navigateLeft[1] = true; 
		if (controllers[2].GetThumbstickDirectionOnce ("left") && isPlayerInRoom[2] && !confirmButtonPressed[2])
			navigateLeft[2] = true;
		if (controllers[3].GetThumbstickDirectionOnce ("left") && isPlayerInRoom[3] && !confirmButtonPressed[3])
			navigateLeft[3] = true;
		if (controllers[4].GetThumbstickDirectionOnce ("left") && isPlayerInRoom[4] && !confirmButtonPressed[4])
			navigateLeft[4] = true;
		
		// right
		
		if (controllers[1].GetThumbstickDirectionOnce ("right") && isPlayerInRoom[1] && !confirmButtonPressed[1])
			navigateRight[1] = true;
		if (controllers[2].GetThumbstickDirectionOnce ("right") && isPlayerInRoom[2] && !confirmButtonPressed[2])
			navigateRight[2] = true;
		if (controllers[3].GetThumbstickDirectionOnce ("right") && isPlayerInRoom[3] && !confirmButtonPressed[3])
			navigateRight[3] = true;
		if (controllers[4].GetThumbstickDirectionOnce ("right") && isPlayerInRoom[4] && !confirmButtonPressed[4])
			navigateRight[4] = true;
		
		// up
		if (controllers[1].GetThumbstickDirectionOnce ("up") && isPlayerInRoom[1] && !confirmButtonPressed[1])
			navigateUp[1] = true;
		if (controllers[2].GetThumbstickDirectionOnce ("up") && isPlayerInRoom[2] && !confirmButtonPressed[2])
			navigateUp[2] = true;
		if (controllers[3].GetThumbstickDirectionOnce ("up") && isPlayerInRoom[3] && !confirmButtonPressed[3])
			navigateUp[3] = true;
		if (controllers[4].GetThumbstickDirectionOnce ("up") && isPlayerInRoom[4] && !confirmButtonPressed[4])
			navigateUp[4] = true;
		
		// down
		if (controllers[1].GetThumbstickDirectionOnce ("down") && isPlayerInRoom[1] && !confirmButtonPressed[1])
			navigateDown[1] = true;
		if (controllers[2].GetThumbstickDirectionOnce ("down") && isPlayerInRoom[2] && !confirmButtonPressed[2])
			navigateDown[2] = true;
		if (controllers[3].GetThumbstickDirectionOnce ("down") && isPlayerInRoom[3] && !confirmButtonPressed[3])
			navigateDown[3] = true;
		if (controllers[4].GetThumbstickDirectionOnce ("down") && isPlayerInRoom[4] && !confirmButtonPressed[4])
			navigateDown[4] = true;
	}
	
	private void saveSelectionInfo() {
		saveSelection.player1 = currentSelectedChampIndex[1] - 1;
		saveSelection.player2 = currentSelectedChampIndex[2] - 1;
		saveSelection.player3 = currentSelectedChampIndex[3] - 1;
		saveSelection.player4 = currentSelectedChampIndex[4] - 1;
		
		saveSelection.player1TEAM = currentSelectedTeamIndex[1];
		saveSelection.player2TEAM = currentSelectedTeamIndex[2];
		saveSelection.player3TEAM = currentSelectedTeamIndex[3];
		saveSelection.player4TEAM = currentSelectedTeamIndex[4];
	}
}
