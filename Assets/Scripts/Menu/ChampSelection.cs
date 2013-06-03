using UnityEngine;
using System.Collections;

public class ChampSelection : MonoBehaviour {
//	private const string BUTTON_NEXT_PREFAB_PATH = "Prefabs/Menu Buttons/Next";
//	private const string BUTTON_BACK_PREFAB_PATH = "Prefabs/Menu Buttons/Back";
//	private const string XBOX_BUTTON_A_ICON_PREFAB_PATH = "Prefabs/Menu Buttons/Xbox360ButtonA";
//	private const string XBOX_BUTTON_B_ICON_PREFAB_PATH = "Prefabs/Menu Buttons/Xbox360ButtonB";
	private const string SOLO_IMAGE_PATH = "Textures/Menu/Solo";
	private const string TEAM1_IMAGE_PATH = "Textures/Menu/Team1";
	private const string TEAM2_IMAGE_PATH = "Textures/Menu/Team2";
	
	public Font titleFont;
	public Font bodyFont;
	public Texture2D champBox;
	
	private GUIStyle titleStyle;
	private GUIStyle playerTabStyle;
	private GUIStyle bodyStyle;
	private GUIStyle teamTagStyle;
	
	private Texture2D solo;
	private Texture2D team1;
	private Texture2D team2;
	
	private Vector3 scale;
	private float originalWidth = 800f;
	private float originalHeight = 600f;
	
	// Use this for initialization
	void Start () {
		titleStyle = new GUIStyle();
		playerTabStyle = new GUIStyle();
		bodyStyle = new GUIStyle();
		teamTagStyle = new GUIStyle();
		loadTextures ();
		
//		loadButtons ();
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
	
	private void loadTextures() {
		solo = Resources.Load (SOLO_IMAGE_PATH) as Texture2D;
		team1 = Resources.Load (TEAM1_IMAGE_PATH) as Texture2D;
		team2 = Resources.Load (TEAM2_IMAGE_PATH) as Texture2D;
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
		GUI.Label (new Rect(0f, 0f, 0f, 0f), "Player 1", playerTabStyle);  // player label
		GUI.Box (new Rect (0, 30, 100, 100), champBox);
		GUI.Label (new Rect(0f, 135f, 100f, 50f), team2);
		GUI.EndGroup ();  // end the group
	}
}
