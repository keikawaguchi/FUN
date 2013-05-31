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
	}
	
	void Update() {
		prevGamePadState = currentGamePadState;
		currentGamePadState = GamePad.GetState (controllerNumber);
	}
	
	public void SetControllerNumber(int num) {
		switch (num) {
		case 1:
			controllerNumber = PlayerIndex.One;
			break;
		case 2:
			controllerNumber = PlayerIndex.Two;
			break;
		case 3:
			controllerNumber = PlayerIndex.Three;
			break;
		case 4:
			controllerNumber = PlayerIndex.Four;
			break;	
		}
	}
	
	public int GetControllerNumber() {
		switch (controllerNumber) {
		case PlayerIndex.One:
			return 1;
		case PlayerIndex.Two:
			return 2;
		case PlayerIndex.Three:
			return 3;
		case PlayerIndex.Four:
			return 4;
		}
		return 0;
	}
	
	public bool GetButtonPressed(string button) {
		button = button.ToLower();
		bool buttonPressed = false;
		
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
	
	public Vector2 GetThumbstick(string thumbstickSide) {
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
}