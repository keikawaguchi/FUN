using UnityEngine;
using System.Collections;
using System.Collections.Specialized;
using XInputDotNetPure;

public class XInputController : MonoBehaviour {

	private const string BUTTON_BOMB = "dropbomb";
	private const string BUTTON_SKILL_1 = "skill1";
	private const string BUTTON_SKILL_2 = "skill2";
	private const string BUTTON_SKILL_3 = "skill3";
	private const string BUTTON_START = "start";
	private const string BUTTON_BACK = "back";
	
	public PlayerIndex controllerNumber;
	
	private GamePadState currentGamePadState;
	private GamePadState prevGamePadState;
	
	PlayerControls Champs;
	
	
	void Awake() {
		assignControllersToPlayers(); 
		calculateKeyboardBindings();
		Update();
	}
	
	void Update() {
		prevGamePadState = currentGamePadState;
		currentGamePadState = GamePad.GetState(controllerNumber);
	}
	
	public bool IsControllerConnected(int controllerNum) {
		return GamePad.GetState(intToPlayerIndex(controllerNum)).IsConnected;
	}
	
	public void SetControllerNumber(int num) {
		controllerNumber = intToPlayerIndex(num);
	}
	
	public int GetControllerNumber() {
		return playerIndexToInt(controllerNumber);
	}
	
	public bool GetButtonPressed(string button) {
		button = button.ToLower();
		bool buttonPressed = false;
		
		// Check keyboard
		buttonPressed = GetButtonPressedKeyboard(button);
		
		switch (button) {
		case BUTTON_BOMB:
			if (currentGamePadState.Buttons.A == ButtonState.Pressed 
				&& prevGamePadState.Buttons.A != ButtonState.Pressed) {
				buttonPressed = true;
			}
			break;
		case BUTTON_SKILL_1:
			if (currentGamePadState.Buttons.X == ButtonState.Pressed 
				&& prevGamePadState.Buttons.X != ButtonState.Pressed) {
				buttonPressed = true;
			}
			break;
		case BUTTON_SKILL_2:
			if (currentGamePadState.Buttons.B == ButtonState.Pressed 
				&& prevGamePadState.Buttons.B != ButtonState.Pressed) {
				buttonPressed = true;
			}
			break;
		case BUTTON_SKILL_3:
			if (currentGamePadState.Buttons.Y == ButtonState.Pressed 
				&& prevGamePadState.Buttons.Y != ButtonState.Pressed) {
				buttonPressed = true;
			}
			break;
		case BUTTON_START:
			if (currentGamePadState.Buttons.Start == ButtonState.Pressed
				&& prevGamePadState.Buttons.Start != ButtonState.Pressed) {
				buttonPressed = true;
			}
			break;
		case BUTTON_BACK:
			if (currentGamePadState.Buttons.Back == ButtonState.Pressed
				&& prevGamePadState.Buttons.Back != ButtonState.Pressed) {
				buttonPressed = true;
			}
			break;
		}
		
		return buttonPressed;
	}
	
	public bool GetThumbstickDirectionOnce(string direction) {
		bool hasBeenPressed = false;
		
		switch (direction.ToLower()) {
		case "up":
			if (currentGamePadState.ThumbSticks.Left.Y > 0 && prevGamePadState.ThumbSticks.Left.Y <= 0) {
				hasBeenPressed = true;
			}
			break;
		case "down":
			if (currentGamePadState.ThumbSticks.Left.Y < 0 && prevGamePadState.ThumbSticks.Left.Y >= 0) {
				hasBeenPressed = true;
			}
			break;
		case "left":
			if (currentGamePadState.ThumbSticks.Left.X < 0 && prevGamePadState.ThumbSticks.Left.X >= 0) {
				hasBeenPressed = true;
			}
			break;
		case "right":
			if (currentGamePadState.ThumbSticks.Left.X > 0 && prevGamePadState.ThumbSticks.Left.X <= 0) {
				hasBeenPressed = true;
			}
			break;
		}
		
		return hasBeenPressed;
	}
	
	public Vector2 GetThumbstick(string thumbstickSide) {
		// keyboard
		Vector2 keyboardInput = GetAxisKeyboard();
		if (keyboardInput.x != 0 || keyboardInput.y != 0)
			return keyboardInput;
		
		Vector2 thumbstickValue = new Vector2(0, 0);
		
		if (thumbstickSide == "left") {
			thumbstickValue.x = currentGamePadState.ThumbSticks.Left.X;
			thumbstickValue.y = currentGamePadState.ThumbSticks.Left.Y;
		}
		
		if (thumbstickSide == "right") {
			thumbstickValue.x = currentGamePadState.ThumbSticks.Right.X;
			thumbstickValue.y = currentGamePadState.ThumbSticks.Right.Y;
		}
		
		return thumbstickValue;
	}
		
	private PlayerIndex intToPlayerIndex(int num) {
		switch (num) {
		case 1: return PlayerIndex.One;
		case 2: return PlayerIndex.Two;
		case 3: return PlayerIndex.Three;
		case 4: return PlayerIndex.Four;
		}
		
		Debug.LogError ("Controller number " + num + " invalid");
		return PlayerIndex.One;
	}
	
	private int playerIndexToInt(PlayerIndex index) {
		switch (index) {
		case PlayerIndex.One: return 1;
		case PlayerIndex.Two: return 2;
		case PlayerIndex.Three: return 3;
		case PlayerIndex.Four: return 4;
		}
		
		Debug.LogError ("Player Index invalid");
		return 0;
	}
	
	private void assignControllersToPlayers() {
		Champs = GameObject.Find("Controls").GetComponent<PlayerControls>();
		if(gameObject.name == "Albion")
		{
			if(Champs.player1 == 0)
				controllerNumber = PlayerIndex.One;
			else if(Champs.player2 == 0)
				controllerNumber = PlayerIndex.Two;
			else if(Champs.player3 == 0)
				controllerNumber = PlayerIndex.Three;
			else if(Champs.player4 == 0)
				controllerNumber = PlayerIndex.Four;
		}
		if(gameObject.name == "Fanndis")
		{
			if(Champs.player1 == 1)
				controllerNumber = PlayerIndex.One;
			else if(Champs.player2 == 1)
				controllerNumber = PlayerIndex.Two;
			else if(Champs.player3 == 1)
				controllerNumber = PlayerIndex.Three;
			else if(Champs.player4 == 1)
				controllerNumber = PlayerIndex.Four;
		}
		if(gameObject.name == "Kirito")
		{
			if(Champs.player1 == 2)
				controllerNumber = PlayerIndex.One;
			else if(Champs.player2 == 2)
				controllerNumber = PlayerIndex.Two;
			else if(Champs.player3 == 2)
				controllerNumber = PlayerIndex.Three;
			else if(Champs.player4 == 2)
				controllerNumber = PlayerIndex.Four;
		}
		if(gameObject.name == "Merlini")
		{
			if(Champs.player1 == 3)
				controllerNumber = PlayerIndex.One;
			else if(Champs.player2 == 3)
				controllerNumber = PlayerIndex.Two;
			else if(Champs.player3 == 3)
				controllerNumber = PlayerIndex.Three;
			else if(Champs.player4 == 3)
				controllerNumber = PlayerIndex.Four;
		}
		if(gameObject.name == "Temptress")
		{
			if(Champs.player1 == 4)
				controllerNumber = PlayerIndex.One;
			else if(Champs.player2 == 4)
				controllerNumber = PlayerIndex.Two;
			else if(Champs.player3 == 4)
				controllerNumber = PlayerIndex.Three;
			else if(Champs.player4 == 4)
				controllerNumber = PlayerIndex.Four;
		}
	}
	
	
	//////////////////////////////////////////////////////////
	/// TEMPORARY KEYBOARD INPUT FUNCTIONALITY
	//////////////////////////////////////////////////////////
	
	OrderedDictionary buttons;
	
	private void calculateKeyboardBindings() {
		int controllerNum = playerIndexToInt(controllerNumber);
		buttons = new OrderedDictionary();
		buttons["dropbomb"] = "ButtonAController" + controllerNum;
		buttons["skill1"] = "ButtonXController" + controllerNum;
		buttons["skill2"] = "ButtonBController" + controllerNum;
		buttons["skill3"] = "ButtonYController" + controllerNum;
		buttons["HorizontalAxis"] = "HorizontalController" + controllerNum;
		buttons["VerticalAxis"] = "VerticalController" + controllerNum;
	}
	
	public bool GetButtonPressedKeyboard(string button) {
		if (buttons == null) {
			calculateKeyboardBindings();
		}
		return Input.GetButtonDown((string)buttons[button]);
	}
	
	public Vector2 GetAxisKeyboard() {
		if (buttons == null) {
			calculateKeyboardBindings();
		}
		return new Vector2(Input.GetAxis((string)buttons["HorizontalAxis"]), Input.GetAxis((string)buttons["VerticalAxis"]));
	}
}