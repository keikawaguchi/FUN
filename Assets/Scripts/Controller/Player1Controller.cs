using UnityEngine;
using System.Collections;
using System.Collections.Specialized;

public class Player1Controller : MonoBehaviour {
	
	OrderedDictionary buttons;
	
	void Start() {
		buttons = new OrderedDictionary();
		buttons["drop_bomb"] = "Jump";
		buttons["skill_1"] = "Fire1";
		buttons["skill_2"] = "Fire2";
	}
	
	public string getButton(string action) {
		return (string)buttons[action];
	}
}
