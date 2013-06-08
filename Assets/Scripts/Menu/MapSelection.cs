using UnityEngine;
using System.Collections;

public class MapSelection : MonoBehaviour {
	private const string TEMP_MAP_ICON_PATH = "Textures/Maps/GoogleMapsIcon";
	
	private const int TOTAL_MAPS = 6;
	
	// Fonts
	public Font titleFont;
	
	// GUIStyle
	private GUIStyle titleStyle;
	
	// scaling
	private Vector3 scale;
	private float originalWidth = 800f;
	private float originalHeight = 600f;
	
	// controllers
	private XInputController p1Controller;
	
	// save the selected champions and teams
	private PlayerControls saveSelection;
	
	// maps info
	GUIContent[] mapMenu;
	GUIContent[] mapTitles;
	Texture2D[] mapThumbnails;
	
	private float mapMenuWidth;
	private float mapMenuHeight;
	private Vector2 mapMenuPosition;
	
	private int currentSelectedMap = 0;
	
	// Use this for initialization
	void Start () {
		initializeVariables ();
		
		loadResources ();
		setController ();
		setMaps ();
	}
	
	// Update is called once per frame
	void Update () {
		updateMenuByController ();
		handlePressedMenuButton ();
	}
	
	private void OnGUI() {
		if (titleFont != null) {
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
		
		mapMenu = new GUIContent[TOTAL_MAPS];
		mapTitles = new GUIContent[TOTAL_MAPS];
		mapThumbnails = new Texture2D[1];  // need to change to TOTAL_MAPS when all thumbnails are done!!!!!!!!!!!!!!
		
		mapMenuHeight = Screen.height * 2;	// I don't know why this is working
		mapMenuWidth = Screen.width / 2.5f;
		mapMenuPosition = new Vector2(210, 150);
		Debug.Log ("Width: " + Screen.width);
		Debug.Log ("Height: " + Screen.height);
		Debug.Log ("POS: " + mapMenuPosition);
	}
	
	private void setMaps() {
		for (int i = 0; i < TOTAL_MAPS; i++) {
			mapMenu[i] = new GUIContent(mapThumbnails[0], "Map " + (i + 1));  // need to update this!!!!!!!!!!!!
		}
	}
	
	private void loadResources() {
		p1Controller = gameObject.AddComponent<XInputController>();
		saveSelection = GameObject.Find("Controls").GetComponent<PlayerControls>();
		
		mapThumbnails[0] = Resources.Load (TEMP_MAP_ICON_PATH) as Texture2D;  // need to update this!!!!!!!!!!!!!!!
	}
	
	private void setController() {
		p1Controller.SetControllerNumber (1);
	}
	
	private void setGUIStyle() {
		// font style
		titleStyle.font = titleFont;
		
		// font size
		titleStyle.fontSize = 50;
		
		// font color
		titleStyle.normal.textColor = new Color(255f, 128f, 0f, 100f);
	}
	
	private void displayLayout() {
		// title
		string title = "Map Selection";
		GUI.Label (new Rect(250f, 0f, 0f, 0f), "Select Map", titleStyle);
		
		GUI.SelectionGrid(new Rect(mapMenuPosition.x, mapMenuPosition.y, mapMenuWidth, mapMenuHeight / mapMenu.Length), 
			currentSelectedMap, mapMenu, 3); 
	}
	
	private void updateMenuByController() {
		if (p1Controller.GetThumbstickDirectionOnce("right")) {
			currentSelectedMap--;  // i don't know why this is -- not ++
		}
		if (p1Controller.GetThumbstickDirectionOnce("left")) {
			currentSelectedMap++;
		}
		if (currentSelectedMap < 0) {
			currentSelectedMap = mapMenu.Length - 1;
		}
		if (currentSelectedMap >= mapMenu.Length) {
			currentSelectedMap = 0;
		}
	}
	
	private void handlePressedMenuButton() {
		int currentLevel = Application.loadedLevel;
		
		if (p1Controller.GetButtonPressed("a")) {  // start the game
			saveSelection.mapNum = currentSelectedMap + 1;
			Debug.Log ("Loading the game");
			Application.LoadLevel(++currentLevel);
		}
		else if (p1Controller.GetButtonPressed ("b"))  // back
			Application.LoadLevel(--currentLevel);
	}
}
