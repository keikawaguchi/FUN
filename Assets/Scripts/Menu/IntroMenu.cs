using UnityEngine;
using System.Collections;

public class IntroMenu : MonoBehaviour {
	
	public GUIStyle style;
	public GUIStyle hoverStyle;
	private float menuWidth;
	private float menuHeight;
	private Vector2 menuPosition;
	
	private Texture2D menuBG;
	private Texture2D menuBGHover;
	
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
		buildMenu();
	}
	
	private void initMenu() {	
		menuItems = new GUIContent[5];
		menuItems[0] = new GUIContent("Play", "play");
		menuItems[1] = new GUIContent("Tutorial", "tutorial");
		menuItems[2] = new GUIContent("Options", "options");
		menuItems[3] = new GUIContent("Credits", "credits");
		menuItems[4] = new GUIContent("Quit", "quit");
		
		menuHeight = Screen.height / 2.5f;
		menuWidth = Screen.width / 3;
		menuPosition = new Vector2(Screen.width / 2 - menuWidth / 2, Screen.height / 2.5f);
	}
	
	private void loadResources() {
		controller = GetComponent<XInputController>();
	}
	
	private void buildMenu() {
		GUIStyle style;
		for (int i = 0; i < menuItems.Length; i++) {
			style = this.style;
			if (currentSelectedItem == i) {
				style = this.hoverStyle;
			}
			GUI.Button(new Rect(menuPosition.x, menuPosition.y + (menuHeight / menuItems.Length) * i + style.margin.bottom, menuWidth, menuHeight / menuItems.Length), menuItems[i], style);
		}
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
