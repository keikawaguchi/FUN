using UnityEngine;
using System.Collections;
using System.Collections.Specialized;

public class XboxController : MonoBehaviour {
	
	public int controllerNumber;
	
	OrderedDictionary buttons;
	
	
	void Start() {
		buttons = new OrderedDictionary();
	}
	
	public void setControllerNumber(int number) {
		controllerNumber = number;
		calculateKeyBindings();
	}
	
	public string getButton(string action) {
		return (string)buttons[action];
	}
	
	public float getAxisX() {
		
	}
	
	private void calculateKeyBindings() {
		buttons["DropBomb"] = "JumpXbox" + controllerNumber;
		buttons["Skill1"] = "Fire1Xbox" + controllerNumber;
		buttons["Skill2"] = "Fire2Xbox" + controllerNumber;
		buttons["AxisX"] = "HorizontalXbox" + controllerNumber;
		buttons["AxisY"] = "VerticalXbox" + controllerNumber;
	}
}
