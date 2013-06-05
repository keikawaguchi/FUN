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
	
	private const int MAX_PLAYERS = 4;
	
	// Fonts
	public Font titleFont;
	public Font bodyFont;
	
	// GUIStyle
	private GUIStyle titleStyle;
	private GUIStyle playerTabStyle;
	private GUIStyle bodyStyle;
	private GUIStyle teamTagStyle;
	
	// texture
	private Texture2D solo;
	private Texture2D team1;
	private Texture2D team2;
	
	// champion texture
	private Texture2D champBox;
	private Texture2D albionIcon;
	private Texture2D fanndisIcon;
	private Texture2D kiritoIcon;
	private Texture2D merliniIcon;
	private Texture2D temptressIcon;
	
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
	private string[] playersSelectedChamps;  // the selected champion to be played
	
//	private ChampInfo champInfo;
	
	// Use this for initialization
	void Start () {
		instantiateVariables ();
		
		loadScripts ();
		loadTextures ();
//		loadButtons ();
		
		setControllers ();
	}
	
	void Update() {
		setPlayerJoined ();
		setSelectionConfirmed ();
	}
	
	private void OnGUI() {
		if (titleFont != null && bodyFont != null && champBox != null) {
			setGUIStyle ();
		
			// begin scaling the contents
			scale.x = Screen.width/originalWidth;  // calculate hor scale
	    	scale.y = Screen.height/originalHeight;  // calculate vert scale
	    	scale.z = 1;
	    	var saveMatrix = GUI.matrix;  // save current matrix
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
			
			displayContents ();
			
			GUI.matrix = saveMatrix;
			// end scaling the contents
		}
	}
	
	private void instantiateVariables() {
		titleStyle = new GUIStyle();
		playerTabStyle = new GUIStyle();
		bodyStyle = new GUIStyle();
		teamTagStyle = new GUIStyle();
		
		// index 0 is not used
		controllers = new XInputController[MAX_PLAYERS + 1];
		confirmSelection = new bool[MAX_PLAYERS + 1];
		isPlayerInRoom = new bool[MAX_PLAYERS + 1];
	}
	
	private void loadScripts() {
		for (int i = 1; i <= MAX_PLAYERS; i++)
			controllers[i] = gameObject.AddComponent<XInputController>();
	}
	
	private void loadTextures() {
		solo = Resources.Load (SOLO_IMAGE_PATH) as Texture2D;
		team1 = Resources.Load (TEAM1_IMAGE_PATH) as Texture2D;
		team2 = Resources.Load (TEAM2_IMAGE_PATH) as Texture2D;
		
		champBox = Resources.Load (CHAMP_BOX_IMAGE_PATH) as Texture2D;
		albionIcon = Resources.Load (ALBION_IMAGE_PATH) as Texture2D;
		fanndisIcon = Resources.Load (FANNDIS_IMAGE_PATH) as Texture2D;
		kiritoIcon = Resources.Load (KIRITO_IMAGE_PATH) as Texture2D;
		merliniIcon = Resources.Load (MERLINI_IMAGE_PATH) as Texture2D;
		temptressIcon = Resources.Load (TEMPTRESS_IMAGE_PATH) as Texture2D;
		
		confirm = Resources.Load (CONFIRM_IMAGE_PATH) as Texture2D;
		confirmed = Resources.Load (CONFIRMED_IMAGE_PATH) as Texture2D;
	}
	
	private void setControllers() {
		for (int i = 1; i <= MAX_PLAYERS; i++) {
			controllers[i].SetControllerNumber (i);
			Debug.Log ("Controller" + (i) + " #: " + controllers[i].GetControllerNumber ());
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
		playerTabStyle.font = bodyFont;
		bodyStyle.font = bodyFont;
		teamTagStyle.font = bodyFont;
		
		// font size
		titleStyle.fontSize = 50;
		playerTabStyle.fontSize = 20;
		bodyStyle.fontSize = 13;
		teamTagStyle.fontSize = 15;
		
		// font color
		titleStyle.normal.textColor = new Color(255f, 128f, 0f, 100f);
		playerTabStyle.normal.textColor = Color.white;
		bodyStyle.normal.textColor = Color.white;
		teamTagStyle.normal.textColor = Color.cyan;
	}
	
	private void displayContents() {
		// title
		string title = "Champions Selection";
		GUI.Label (new Rect(150f, 0f, 0f, 0f), "Select Champions", titleStyle);
		
		// Player 1 group
		GUI.BeginGroup (new Rect(50f, 80f, 300f, 500f));  // make a group
		
		displayChampionContents (controllers[1], 1);
		
		GUI.EndGroup ();  // end the group
		
		// Player 2 group
		GUI.BeginGroup (new Rect(50f, 300f, 300f, 500f));  // make a group
		
		displayChampionContents (controllers[2], 2);
		
		GUI.EndGroup ();  // end the group
		
		// Player 3 group
		GUI.BeginGroup (new Rect(400f, 80f, 300f, 500f));  // make a group
		
		displayChampionContents (controllers[3], 3);
		
		GUI.EndGroup ();  // end the group
		
		// Player 4 group
		GUI.BeginGroup (new Rect(400f, 300f, 300f, 500f));  // make a group
		
		displayChampionContents (controllers[4], 4);
		
		GUI.EndGroup ();  // end the group
	}
	
	private void displayChampionContents(XInputController controller, int playerNum) {
		/*
		 * Conditions:
		 * 1. No duplicates allowed
		 */
		
		GUI.Label (new Rect(0f, 0f, 0f, 0f), "Player " + playerNum, playerTabStyle);  // player label
		
		displayChampTexture (controller);
		displayTeamInfo (controller);
		displayConfirmationButton (controller);
	}
	
	private void displayChampTexture(XInputController controller) {
		Texture2D champBoxContent;
		if (!isPlayerInRoom[controller.GetControllerNumber ()])
			champBoxContent = champBox;
		else {
			champBoxContent = albionIcon;
//			if (controller.GetThumbstick ("left").x > 0)
		}
		GUI.Box (new Rect (0f, 30f, 100f, 100f), champBoxContent);
	}
	
	private void displayTeamInfo(XInputController controller) {
		GUI.Label (new Rect(0f, 125f, 100f, 50f), solo);
	}
	
	private void displayConfirmationButton(XInputController controller) {
		Texture2D confirmContent;
		if (!confirmSelection[controller.GetControllerNumber ()])
			confirmContent = confirm;
		else
			confirmContent = confirmed;
		
		GUI.Box (new Rect(0f, 150f, 100f, 25f), confirmContent);
	}
	
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
}
