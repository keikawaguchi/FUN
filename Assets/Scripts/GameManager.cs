using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool checkForWinner(Hero hero) {
		
		int teamNum = hero.getTeamNumber();
		int numOfPlayers = 0;
		
		bool won = true;
		
		GameObject[] players;
		players = GameObject.FindGameObjectsWithTag("Player");
		
		foreach(GameObject player in players) {
			
			int playerTeamNum = player.GetComponent<Hero>().getTeamNumber();
			
			if (playerTeamNum != teamNum) {
				int playerLives = player.GetComponent<Hero>().lives;
				
				if (playerLives > 0) {
					won = false;
					break;
				}
				
			} else {
				numOfPlayers++;	
			}
		}
		
		Debug.Log (numOfPlayers);
		
		if (won) {
			if (numOfPlayers > 1) 		// team won
				Debug.Log ("Team " + teamNum + " WON!");
			else						// player won
				Debug.Log ("Player " + hero.playerNumber + " WON!");	
		}
		
		return won;
	}
}
