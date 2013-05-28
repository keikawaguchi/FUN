using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool checkForWinner() {
	
		int champPlayerNum = 0;	
		int champTeamNum = 0;
		int numOfPlayers = 0;
		
		bool won = true;
		bool winnerFound = false;
		
		GameObject[] players;
		players = GameObject.FindGameObjectsWithTag("Player");
		
		foreach (GameObject player in players) {
			
			Hero champ = player.GetComponent<Hero>();
			
			// check if this champ won
			if (champ.lives > 0) {
				
				champTeamNum = champ.getTeamNumber();
				champPlayerNum = champ.playerNumber;
				numOfPlayers = 0;
				
				foreach (GameObject currPlayer in players) {
					int playerTeamNum = currPlayer.GetComponent<Hero>().getTeamNumber();
				
					if (playerTeamNum != champTeamNum) {
						int playerLives = currPlayer.GetComponent<Hero>().lives;
					
						if (playerLives > 0) {
							won = false;
							break;
						}
					
					} else
						numOfPlayers++;	
				}
				
				if (won)
					break;
			}
		}
		
		if (winnerFound) {
			if (numOfPlayers > 1) 		// team won
				Debug.Log ("Team " + champTeamNum + " WON!");
			else						// player won
				Debug.Log ("Player " + champPlayerNum + " WON!");	
		}
		
		return won;
	}
}
