using UnityEngine;
using System.Collections;

public class ChampSelection : MonoBehaviour {
//	private const string BUTTON_NEXT_PREFAB_PATH = "Prefabs/Menu Buttons/Next";
//	private const string BUTTON_BACK_PREFAB_PATH = "Prefabs/Menu Buttons/Back";
//	private const string XBOX_BUTTON_A_ICON_PREFAB_PATH = "Prefabs/Menu Buttons/Xbox360ButtonA";
//	private const string XBOX_BUTTON_B_ICON_PREFAB_PATH = "Prefabs/Menu Buttons/Xbox360ButtonB";
	
	public Font titleFont;
	public Font bodyFont;
	public Texture2D champBox;
	
	private GUIStyle titleStyle;
	private GUIStyle subtitleStyle;
	private GUIStyle bodyStyle;
	
	private Vector3 scale;
	private float originalWidth = 800f;
	private float originalHeight = 600f;
	
	// Use this for initialization
	void Start () {
		titleStyle = new GUIStyle();
		bodyStyle = new GUIStyle();
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
		bodyStyle.font = bodyFont;
		
		// font size
		titleStyle.fontSize = 50;
		bodyStyle.fontSize = 13;
		
		// font color
		titleStyle.normal.textColor = new Color(255f, 128f, 0f, 100f);
		bodyStyle.normal.textColor = Color.white;
		
		subtitleStyle = bodyStyle;
		subtitleStyle.fontSize = 20;
	}
	
	private void displayContents() {
		// title
		string title = "Champions Selection";
		GUI.Label (new Rect(100f, 0f, 0f, 0f), "Champions Selection", titleStyle);
		
		// Player 1 group
		GUI.BeginGroup (new Rect(50f, 80f, 300f, 500f));  // make a group
		GUI.Label (new Rect(0f, 0f, 0f, 0f), "Player 1", subtitleStyle);  // player label
		GUI.Box (new Rect (0, 30, 100, 100), champBox);
		GUI.EndGroup ();  // end the group
	}
}
