using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour {
	
	private GameManager manager;
	private GUIText winner;
	private string winnerName;
	
	// Use this for initialization
	void Start () {
		GameObject managerObj = GameObject.Find ("Game Manager");
		manager = managerObj.GetComponent<GameManager>();
		
		loadText ();
	}
	
	// Update is called once per frame
	void Update () {
		winnerName = manager.getWinner ();
		winner.text = winnerName;
	}
	
	private void loadText() {
		GameObject winnerObj = GameObject.Find ("Winner");
		winner = winnerObj.GetComponent<GUIText>();
	}
}
