using UnityEngine;
using System.Collections;

public class ButtonBehavior : MonoBehaviour {
	private const int MAX_PLAYERS = 4;
	
//	public bool isQuitButton = false;
//	public bool isNextButton = false;
//	public bool isBackButton = false;
	
	private XInputController p1Controller;
	
	// save the selection champions and teams
	private PlayerControls saveSelection;
	
	private int player1Champ;
	private int player2Champ;
	private int player3Champ;
	private int player4Champ;
	
	private int player1Team;
	private int player2Team;
	private int player3Team;
	private int player4Team;
	
	// Use this for initialization
	void Start () {
		loadScripts ();
		
		p1Controller.SetControllerNumber (1);
	}
	
	// Update is called once per frame
	void Update () {
		int currentLevel = Application.loadedLevel;
		
		if (p1Controller.GetButtonPressed ("dropomb")) {  // next
			
			Application.LoadLevel(++currentLevel);
		}
		else if (p1Controller.GetButtonPressed ("skill2"))  // back
			Application.LoadLevel(--currentLevel);
	}
	
	private void loadScripts() {
		p1Controller = gameObject.AddComponent<XInputController>();
	}
}
