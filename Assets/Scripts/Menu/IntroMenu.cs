using UnityEngine;
using System.Collections;

public class IntroMenu : MonoBehaviour {
	
	private float menuWidth;
	private float menuHeight;
	private Vector2 menuPosition;
	
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
		updateMenuByController();
		handlePressedMenuButton();
	}
	
	void OnGUI() {
		setMenuStyle();
		buildMenu();
	}
	
	private void initMenu() {
		menuItems = new GUIContent[5];
		menuItems[0] = new GUIContent("Play", "play");
		menuItems[1] = new GUIContent("Tutorial", "tutorial");
		menuItems[2] = new GUIContent("Options", "options");
		menuItems[3] = new GUIContent("Credits", "credits");
		menuItems[4] = new GUIContent("Quit", "quit");
		
		menuHeight = Screen.height * 2;	// I don't know why this is working
		menuWidth = Screen.width / 3;
		menuPosition = new Vector2(Screen.width / 2 - menuWidth / 2, Screen.height / 2);
	}
	
	private void loadResources() {
		menuBG = Resources.Load ("Textures/Menu/MenuButtonBG") as Texture2D;	
		menuBGHover = Resources.Load ("Textures/Menu/MenuButtonBGHover") as Texture2D;
		controller = GetComponent<XInputController>();
	}
	
	private void setMenuStyle() {
		GUI.skin.button.fontSize = 25;
		
		// normal gui styles
		GUI.skin.button.normal.textColor = Color.white;	
		GUI.skin.button.normal.background = menuBG;
		
		// on hover gui styles
		GUI.skin.button.hover.textColor = Color.white;
		GUI.skin.button.hover.background = menuBGHover;
	}
	
	private void buildMenu() {
		GUI.SelectionGrid(new Rect(menuPosition.x, menuPosition.y, menuWidth, menuHeight / menuItems.Length), 
			currentSelectedItem, menuItems, 1); 

	}
	
	private void updateMenuByController() {
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
	}
	
	private void handlePressedMenuButton() {
		if (controller.GetButtonPressed("a") || controller.GetButtonPressed("x")) {
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
}
