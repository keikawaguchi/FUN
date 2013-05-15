using UnityEngine;
using System.Collections;
using System.Collections.Specialized;

public class Controller : MonoBehaviour {
	
	public int controllerNumber;
	
	OrderedDictionary buttons;
	
	
	void Start() {
		buttons = new OrderedDictionary();
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
		buttons["HorizontalAxis"] = "HorizontalController" + controllerNumber;
		buttons["VerticalAxis"] = "VerticalController" + controllerNumber;
	}
}
