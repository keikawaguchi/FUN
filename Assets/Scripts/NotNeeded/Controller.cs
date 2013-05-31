using UnityEngine;
using System.Collections;
using System.Collections.Specialized;

public class Controller : MonoBehaviour {
	
	public int controllerNumber;
	
	OrderedDictionary buttons;
	PlayerControls Champs;
	
	void Awake() {
		buttons = new OrderedDictionary();
		calculateKeyBindings();
		Champs = GameObject.Find("Controls").GetComponent<PlayerControls>();
		if(gameObject.name == "Albion")
		{
			if(Champs.player1 == 0)
				controllerNumber = 1;
			else if(Champs.player2 == 0)
				controllerNumber = 2;
			else if(Champs.player3 == 0)
				controllerNumber = 3;
			else if(Champs.player4 == 0)
				controllerNumber = 4;
		}
		if(gameObject.name == "Fanndis")
		{
			if(Champs.player1 == 1)
				controllerNumber = 1;
			else if(Champs.player2 == 1)
				controllerNumber = 2;
			else if(Champs.player3 == 1)
				controllerNumber = 3;
			else if(Champs.player4 == 1)
				controllerNumber = 4;
		}
		if(gameObject.name == "Kirito")
		{
			if(Champs.player1 == 2)
				controllerNumber = 1;
			else if(Champs.player2 == 2)
				controllerNumber = 2;
			else if(Champs.player3 == 2)
				controllerNumber = 3;
			else if(Champs.player4 == 2)
				controllerNumber = 4;
		}
		if(gameObject.name == "Merlini")
		{
			if(Champs.player1 == 3)
				controllerNumber = 1;
			else if(Champs.player2 == 3)
				controllerNumber = 2;
			else if(Champs.player3 == 3)
				controllerNumber = 3;
			else if(Champs.player4 == 3)
				controllerNumber = 4;
		}
		if(gameObject.name == "Temptress")
		{
			if(Champs.player1 == 4)
				controllerNumber = 1;
			else if(Champs.player2 == 4)
				controllerNumber = 2;
			else if(Champs.player3 == 4)
				controllerNumber = 3;
			else if(Champs.player4 == 4)
				controllerNumber = 4;
		}
		calculateKeyBindings();
	}
	
	public void setControllerNumber(int number) {
		controllerNumber = number;
		calculateKeyBindings();
	}
	
	public string getButton(string action) {
		return (string)buttons[action];
	}
	
	public float getAxis(string direction) {
		return Input.GetAxis((string)buttons[direction + "Axis"]);
	}
	
	private void calculateKeyBindings() {
		buttons["DropBomb"] = "ButtonAController" + controllerNumber;
		buttons["Skill1"] = "ButtonXController" + controllerNumber;
		buttons["Skill2"] = "ButtonBController" + controllerNumber;
		buttons["Skill3"] = "ButtonYController" + controllerNumber;
		buttons["HorizontalAxis"] = "HorizontalController" + controllerNumber;
		buttons["VerticalAxis"] = "VerticalController" + controllerNumber;
	}
}
