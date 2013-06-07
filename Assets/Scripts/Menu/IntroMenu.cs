using UnityEngine;
using System.Collections;

public class IntroMenu : MonoBehaviour {
	
	public float menuWidth;
	public float menuHeight;
	
	private Texture2D menuBG;
	private Texture2D menuBGHover;
	private GUIStyle style;
	
	private int currentSelectedItem = 0;
	GUIContent[] menuItems;
	
	private XInputController controller;

	// Use this for initialization
	void Start () {
		loadResources();
		initMenu();
	}
	
	void Update() {
		if (controller.GetThumbstickDirectionOnce("down")) {
			currentSelectedItem++;
		}
		if (controller.GetThumbstickDirectionOnce("up")) {
			currentSelectedItem--;
		}
		if (currentSelectedItem < 0) {
			currentSelectedItem = menuItems.Length - 1;
		}
		if (currentSelectedItem >= menuItems.Length) {
			currentSelectedItem = 0;
		}
		
		if (controller.GetButtonPressed("a") || controller.GetButtonPressed("x")) {
			Debug.Log ("Button pressed");
			switch (menuItems[currentSelectedItem].tooltip) {
			case "play":
				Debug.Log ("Loading champion selection screen");
				Application.LoadLevel("Champ Selection");
				break;
			case "quit":
				Application.Quit();
				break;
			}
		}
	}
	
	void OnGUI() {
		setMenuStyle();
		buildMenu();
		Debug.Log ("Current item: " + menuItems[currentSelectedItem].tooltip);
	}
	
	private void initMenu() {
		menuItems = new GUIContent[5];
		menuItems[0] = new GUIContent("Play", "play");
		menuItems[1] = new GUIContent("Tutorial", "tutorial");
		menuItems[2] = new GUIContent("Options", "options");
		menuItems[3] = new GUIContent("Credits", "credits");
		menuItems[4] = new GUIContent("Quit", "quit");
	}
	
	private void loadResources() {
		menuBG = Resources.Load ("Textures/Menu/MenuButtonBG") as Texture2D;	
		menuBGHover = Resources.Load ("Textures/Menu/MenuButtonBGHover") as Texture2D;
		controller = GetComponent<XInputController>();
	}
	
	private void setMenuStyle() {
		// normal gui styles
		GUI.skin.button.normal.textColor = Color.white;
		GUI.skin.button.fontSize = 25;
		GUI.skin.button.normal.background = menuBG;
		
		// on hover gui styles
		GUI.skin.button.hover.textColor = Color.white;
		GUI.skin.button.hover.background = menuBGHover;
	}
	
	private void buildMenu() {
		GUI.SelectionGrid(new Rect(Screen.width / 2 - menuWidth / 2, Screen.height / 2 - menuHeight / 2, menuWidth, menuHeight * menuItems.Length), 
			currentSelectedItem, menuItems, 1); 
	}
}
