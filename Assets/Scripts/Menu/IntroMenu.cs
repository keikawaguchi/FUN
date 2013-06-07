using UnityEngine;
using System.Collections;

public class IntroMenu : MonoBehaviour {
	
	public float menuWidth;
	public float menuHeight;
	private Texture2D menuBGTexture;
	private GUIStyle style;
	
	GUIContent[] menuItems;

	// Use this for initialization
	void Start () {
		menuBGTexture = Resources.Load ("Textures/Menu/MenuButtonBG") as Texture2D;
		
		menuItems = new GUIContent[5];
		menuItems[0] = new GUIContent("Play", "play");
		menuItems[1] = new GUIContent("Tutorial", "tutorial");
		menuItems[2] = new GUIContent("Options", "options");
		menuItems[3] = new GUIContent("Credits", "credits");
		menuItems[4] = new GUIContent("Quit", "quit");
	}
	
	void OnGUI() {
		// normal gui styles
		GUI.skin.button.normal.textColor = Color.white;
		GUI.skin.button.fontSize = 25;
		GUI.skin.button.normal.background = menuBGTexture;
		
		// on hover gui styles
		GUI.skin.button.hover.textColor = Color.blue;
		GUI.skin.button.hover.background = menuBGTexture;
		
		// build menu
		GUI.SelectionGrid(new Rect(Screen.width / 2 - menuWidth / 2, Screen.height / 2 - menuHeight / 2, menuWidth, menuHeight * menuItems.Length), -1, menuItems, 1); 

	}
}
