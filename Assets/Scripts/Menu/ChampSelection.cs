using UnityEngine;
using System.Collections;

public class ChampSelection : MonoBehaviour {
//	private const string BUTTON_NEXT_PREFAB_PATH = "Prefabs/Menu Buttons/Next";
//	private const string BUTTON_BACK_PREFAB_PATH = "Prefabs/Menu Buttons/Back";
//	private const string XBOX_BUTTON_A_ICON_PREFAB_PATH = "Prefabs/Menu Buttons/Xbox360ButtonA";
//	private const string XBOX_BUTTON_B_ICON_PREFAB_PATH = "Prefabs/Menu Buttons/Xbox360ButtonB";
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
	private GUIStyle teamTagStyle;
	
	// selected info
	private Texture2D[] teamTexture;
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
	private bool[] confirmSelection;  // player confirmed champion selection
	private bool[] navigateLeft;
	private bool[] navigateRight;
	private bool[] navigateUp;
	private bool[] navigateDown;
	private bool[] defaultChampViewed;
	private string[] playersSelectedChamps;  // the selected champion to be played
	
	private ChampInfo champInfo;
	
	// current selected info
	private int[] currentSelectedIndex;
	private Texture2D[] defaultChampTextureDisplayed;
	private string[] defaultChampNameDisplayed;
	
	// Use this for initialization
	void Start () {
		initializeVariables ();
		
		loadScripts ();
		loadTextures ();
//		loadButtons ();
		
		setControllers ();
		
		// construct champions info
		setChampNames ();
		setSkillsDescrip ();
		
		setDefaultChampInfo ();
	}
	
	void Update() {
		
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
			
			displayLayout ();
			
			GUI.matrix = saveMatrix;
			// end scaling the contents
		}
	}
	
	private void initializeVariables() {
		titleStyle = new GUIStyle();
		playerTagStyle = new GUIStyle();
		bodyStyle = new GUIStyle();
		teamTagStyle = new GUIStyle();
		
		// index 0 is not used
		controllers = new XInputController[MAX_PLAYERS + 1];
		confirmSelection = new bool[MAX_PLAYERS + 1];
		isPlayerInRoom = new bool[MAX_PLAYERS + 1];
		defaultChampViewed = new bool[MAX_PLAYERS + 1];
		navigateLeft = new bool[MAX_PLAYERS + 1];
		navigateRight = new bool[MAX_PLAYERS + 1];
		navigateUp = new bool[MAX_PLAYERS + 1];
		navigateDown = new bool[MAX_PLAYERS + 1];
		
		teamTexture = new Texture2D[MAX_TEAM_TYPES];
		champNames = new string[TOTAL_AVAILABLE_CHAMPS + 1];
		champTextures = new Texture2D[TOTAL_AVAILABLE_CHAMPS + 1];  // 0 is for default view
		champSkillsDescrip = new string[TOTAL_AVAILABLE_CHAMPS + 1];
		
		currentSelectedIndex = new int[MAX_PLAYERS + 1];
		defaultChampTextureDisplayed = new Texture2D[MAX_PLAYERS + 1];
		defaultChampNameDisplayed = new string[MAX_PLAYERS + 1];
	}
	
	private void loadScripts() {
		for (int i = 1; i <= MAX_PLAYERS; i++)
			controllers[i] = gameObject.AddComponent<XInputController>();
		
		champInfo = gameObject.AddComponent<ChampInfo>();
	}
	
	private void loadTextures() {
		teamTexture[0] = Resources.Load (SOLO_IMAGE_PATH) as Texture2D;
		teamTexture[1] = Resources.Load (TEAM1_IMAGE_PATH) as Texture2D;
		teamTexture[2] = Resources.Load (TEAM2_IMAGE_PATH) as Texture2D;
		
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
		champNames[1] = "Albion";
		champNames[2] = "Fanndis";
		champNames[3] = "Kirito";
		champNames[4] = "Merlini";
		champNames[5] = "Temptress";
	}
	
	private void setSkillsDescrip() {
		champSkillsDescrip[1] = "Holy Trap - CD: 10 sec\n" +
			"Albion places a trap on the map that is visible for 1 second " +
			"by everyone, to stun whoever walks through it except the hero places the trap.\n" +
			"Holy Blink - CD: 10 sec\n" +
			"Albion teleports 3 squares to the front direction of the hero.";
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
			currentSelectedIndex[i] = 0;
			defaultChampTextureDisplayed[i] = champTextures[1];
			defaultChampNameDisplayed[i] = champNames[1];
		}
	}
	
//	private void loadButtons() {
//		GameObject nextPrefab = Resources.Load (BUTTON_NEXT_PREFAB_PATH) as GameObject;
//		GameObject backPrefab = Resources.Load (BUTTON_BACK_PREFAB_PATH) as GameObject;
//		GameObject xboxAPrefab = Resources.Load (XBOX_BUTTON_A_ICON_PREFAB_PATH) as GameObject;
//		GameObject xboxBPrefab = Resources.Load (XBOX_BUTTON_B_ICON_PREFAB_PATH) as GameObject;
//		
//		GameObject next = Instantiate (nextPrefab) as GameObject;
//		GameObject back = Instantiate (backPrefab) as GameObject;
//		GameObject xboxA = Instantiate (xboxAPrefab) as GameObject;
//		GameObject xboxB = Instantiate (xboxBPrefab) as GameObject;
//		
//		next.transform.position = new Vector3(Screen.width - 300f, 0, Screen.height - 10f);
//		back.transform.position = new Vector3(300f, 0, Screen.height - 10f);
//	}
	
	private void setGUIStyle() {
		// font style
		titleStyle.font = titleFont;
		playerTagStyle.font = bodyFont;
		bodyStyle.font = bodyFont;
		teamTagStyle.font = bodyFont;
		
		// font size
		titleStyle.fontSize = 50;
		playerTagStyle.fontSize = 20;
		bodyStyle.fontSize = 13;
		teamTagStyle.fontSize = 15;
		
		// font color
		titleStyle.normal.textColor = new Color(255f, 128f, 0f, 100f);
		playerTagStyle.normal.textColor = Color.white;
		bodyStyle.normal.textColor = Color.white;
		teamTagStyle.normal.textColor = Color.cyan;
	}
	
	private void displayLayout() {
		// title
		string title = "Champions Selection";
		GUI.Label (new Rect(150f, 0f, 0f, 0f), "Select Champions", titleStyle);
		
		// Player 1 group
		GUI.BeginGroup (new Rect(50f, 80f, 300f, 500f));  // make a group
		
		displayChampionContents (controllers[1], controllers[1].GetControllerNumber ());
		
		GUI.EndGroup ();  // end the group
		
		// Player 2 group
		GUI.BeginGroup (new Rect(50f, 300f, 300f, 500f));  // make a group
		
		displayChampionContents (controllers[2], controllers[2].GetControllerNumber ());
		
		GUI.EndGroup ();  // end the group
		
		// Player 3 group
		GUI.BeginGroup (new Rect(400f, 80f, 300f, 500f));  // make a group
		
		displayChampionContents (controllers[3], controllers[3].GetControllerNumber ());
		
		GUI.EndGroup ();  // end the group
		
		// Player 4 group
		GUI.BeginGroup (new Rect(400f, 300f, 300f, 500f));  // make a group
		
		displayChampionContents (controllers[4], controllers[4].GetControllerNumber ());
		
		GUI.EndGroup ();  // end the group
	}
	
	private void displayChampionContents(XInputController controller, int playerNum) {
		/*
		 * Conditions:
		 * 1. No duplicates allowed
		 */
		
		GUI.Label (new Rect(0f, 0f, 0f, 0f), "Player " + playerNum, playerTagStyle);  // player label
		
		displayChampInfo (controller, playerNum);
		displayTeamInfo (controller);
		displayConfirmationButton (controller);
	}
	
	private void displayChampInfo(XInputController controller, int controllerNum) {
		Texture2D champTextureBox = defaultChampTextureDisplayed[controllerNum];
		string champNameLabel = defaultChampNameDisplayed[controllerNum];
		string champSkillsBox;
		
		if (!isPlayerInRoom[controllerNum]) {
			champTextureBox = champTextures[0];
			champNameLabel = "";
		}
		else {
			if (navigateLeft[controllerNum]) {
				int index = currentSelectedIndex[controllerNum];
				index--;
				
				if (index < 1)
					index = TOTAL_AVAILABLE_CHAMPS;
				
				currentSelectedIndex[controllerNum] = index;
				
				// update champion texture
				defaultChampTextureDisplayed[controllerNum] = champTextures[index];
				champTextureBox = defaultChampTextureDisplayed[controllerNum];
				
				defaultChampNameDisplayed[controllerNum] = champNames[index];
				champNameLabel = defaultChampNameDisplayed[controllerNum];
				
				navigateLeft[controllerNum] = false;
			}
			else if (navigateRight[controllerNum]) {
				int index = currentSelectedIndex[controllerNum];
				index++;
				
				index = index % TOTAL_AVAILABLE_CHAMPS;
				
				
			}
		}
		
		GUI.Box (new Rect (0f, 30f, 100f, 100f), champTextureBox);
		GUI.Label (new Rect(110f, 0f, 0f, 0f), champNameLabel, playerTagStyle);
//		GUI.Box (new Rect(110f, 30f, 200f, 200f));
	}
	
	private void displayTeamInfo(XInputController controller) {
		GUI.Label (new Rect(0f, 125f, 100f, 50f), teamTexture[0]);
	}
	
	private void displayConfirmationButton(XInputController controller) {
		Texture2D confirmContent;
		if (!confirmSelection[controller.GetControllerNumber ()])
			confirmContent = confirm;
		else
			confirmContent = confirmed;
		
		GUI.Box (new Rect(0f, 150f, 100f, 25f), confirmContent);
	}
	
//	private void displayChampName(int index) {
//		Debug.Log ("Champ Name: " + champNames[index]);
////		GUI.Label (new Rect(130f, 0f, 0f, 0f), champInfo.getChampName (index), playerTagStyle);
//		GUI.Label (new Rect(130f, 0f, 0f, 0f), champNames[index], playerTagStyle);
//	}
	
	private void setPlayerJoined() {
		// don't need a loop to set this to waste time complexity
		if (controllers[1].GetButtonPressed ("skill3"))
			isPlayerInRoom[1] = true;
		if (controllers[2].GetButtonPressed ("skill3"))
			isPlayerInRoom[2] = true;
		if (controllers[3].GetButtonPressed ("skill3"))
			isPlayerInRoom[3] = true;
		if (controllers[4].GetButtonPressed ("skill3"))
			isPlayerInRoom[4] = true;
	}
	
	private void setSelectionConfirmed() {
		if (controllers[1].GetButtonPressed ("skill1") && isPlayerInRoom[1])
			confirmSelection[1] = true;
		if (controllers[2].GetButtonPressed ("skill1") && isPlayerInRoom[2])
			confirmSelection[2] = true;
		if (controllers[3].GetButtonPressed ("skill1") && isPlayerInRoom[3])
			confirmSelection[3] = true;
		if (controllers[4].GetButtonPressed ("skill1") && isPlayerInRoom[4])
			confirmSelection[4] = true;
	}
	
	private void navigateState() {
		// left
		if (controllers[1].GetThumbstickDirectionOnce ("left") && isPlayerInRoom[1])
			navigateLeft[1] = true;
		if (controllers[2].GetThumbstick ("left").x < 0 && isPlayerInRoom[2])
			navigateLeft[2] = true;
		if (controllers[3].GetThumbstick ("left").x < 0 && isPlayerInRoom[3])
			navigateLeft[3] = true;
		if (controllers[4].GetThumbstick ("left").x < 0 && isPlayerInRoom[4])
			navigateLeft[4] = true;
		
		// right
		if (controllers[1].GetThumbstick ("left").x > 0 && isPlayerInRoom[1])
			navigateRight[1] = true;
		if (controllers[2].GetThumbstick ("left").x > 0 && isPlayerInRoom[2])
			navigateRight[2] = true;
		if (controllers[3].GetThumbstick ("left").x > 0 && isPlayerInRoom[3])
			navigateRight[3] = true;
		if (controllers[4].GetThumbstick ("left").x > 0 && isPlayerInRoom[4])
			navigateRight[4] = true;
		
		// up
		if (controllers[1].GetThumbstick ("left").y > 0 && isPlayerInRoom[1])
			navigateUp[1] = true;
		if (controllers[2].GetThumbstick ("left").x > 0 && isPlayerInRoom[2])
			navigateUp[2] = true;
		if (controllers[3].GetThumbstick ("left").x > 0 && isPlayerInRoom[3])
			navigateUp[3] = true;
		if (controllers[4].GetThumbstick ("left").x > 0 && isPlayerInRoom[4])
			navigateUp[4] = true;
		
		// down
		if (controllers[1].GetThumbstick ("left").y < 0 && isPlayerInRoom[1])
			navigateDown[1] = true;
		if (controllers[2].GetThumbstick ("left").x < 0 && isPlayerInRoom[2])
			navigateDown[2] = true;
		if (controllers[3].GetThumbstick ("left").x < 0 && isPlayerInRoom[3])
			navigateDown[3] = true;
		if (controllers[4].GetThumbstick ("left").x < 0 && isPlayerInRoom[4])
			navigateDown[4] = true;
	}
}
