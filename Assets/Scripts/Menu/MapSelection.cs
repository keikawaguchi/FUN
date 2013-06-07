using UnityEngine;
using System.Collections;

public class MapSelection : MonoBehaviour {
	// Fonts
	public Font titleFont;
	public Font mapTitleFont;
	
	// map grid size
	public int[,] gridSize = new int[,] {{1, 1}};
	public string[] mapTitle = new string[] {"NULL"};
	
	// GUIStyle
	private GUIStyle titleStyle;
	private GUIStyle mapTitleStyle;
	
	// scaling
	private Vector3 scale;
	private float originalWidth = 800f;
	private float originalHeight = 600f;
	
	// controllers
	private XInputController p1Controller;
	
	// save the selected champions and teams
	private PlayerControls saveSelection;
		
	private int selectedMapIndex;
	
	// Use this for initialization
	void Start () {
		initializeVariables ();
		
		loadScripts ();
		setController ();
	}
	
	// Update is called once per frame
	void Update () {
		int currentLevel = Application.loadedLevel;
		
		if (p1Controller.GetButtonPressed ("dropbomb")) {  // next
			saveSelectionInfo ();
			
			Application.LoadLevel(++currentLevel);
		}
		else if (p1Controller.GetButtonPressed ("skill2"))  // back
			Application.LoadLevel(--currentLevel);
	}
	
	private void OnGUI() {
		if (titleFont != null && mapTitleFont != null) {
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
		mapTitleStyle = new GUIStyle();
	}
	
	private void loadScripts() {
		p1Controller = gameObject.AddComponent<XInputController>();
		saveSelection = GameObject.Find("Controls").GetComponent<PlayerControls>();
	}
	
	private void setController() {
		p1Controller.SetControllerNumber (1);
	}
	
	private void setGUIStyle() {
		// font style
		titleStyle.font = titleFont;
		mapTitleStyle.font = mapTitleFont;
		
		// font size
		titleStyle.fontSize = 50;
		mapTitleStyle.fontSize = 20;
		
		// font color
		titleStyle.normal.textColor = new Color(255f, 128f, 0f, 100f);
		mapTitleStyle.normal.textColor = Color.cyan;
	}
	
	private void displayLayout() {
		// title
		string title = "Map Selection";
		GUI.Label (new Rect(250f, 0f, 0f, 0f), "Select Map", titleStyle);
		
		GUI.BeginGroup (new Rect(Screen.width/2 - 250f, Screen.height/2 - 125f, 500f, 250f));
//		GUILayout.BeginArea(new Rect(Screen.width/2 - 250f, Screen.height/2 - 125f, 500f, 250f));
		
		
		
//		GUILayout.EndArea ();
		GUI.EndGroup ();
	}
	
	private void displayMiniMaps() {
		
	}
	
	private void saveSelectionInfo() {
		saveSelection.mapInd = selectedMapIndex;
	}
}
