using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private string winner;
	private bool foundWinner = false;
	
	// Use this for initialization
	void Start () {
//		SetTeams();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetTeams() {
		
		// get team settings
		PlayerControls playerControls = GameObject.Find("Controls").GetComponent<PlayerControls>();

		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players) {
			
			Hero hero = player.GetComponent<Hero>();
			
			if (hero.playerNumber == 1)
				hero.teamNumber = playerControls.player1TEAM;
			else if (hero.playerNumber == 2)
				hero.teamNumber = playerControls.player2TEAM;
			else if (hero.playerNumber == 3)
				hero.teamNumber = playerControls.player3TEAM;
			else 
				hero.teamNumber = playerControls.player4TEAM;
			
		}
	}
	
	public bool checkForWinner() {
	
		int champPlayerNum = 0;	
		int champTeamNum = 0;
		int numOfPlayers = 0;
		
		bool winnerFound = true;
		
		GameObject[] players;
		players = GameObject.FindGameObjectsWithTag("Player");
		
		foreach (GameObject player in players) {
			
			Hero champ = player.GetComponent<Hero>();
		
			// check if this champ won
			if (champ.lives > 0) {
				
				champTeamNum = champ.getTeamNumber();
				champPlayerNum = champ.playerNumber;
				numOfPlayers = 0;
				winnerFound = true;
				
				// solo player
				if (champTeamNum == 0) {
					foreach (GameObject currPlayer in players) {
						int playerLives = currPlayer.GetComponent<Hero>().lives;
					
						if (champPlayerNum != currPlayer.GetComponent<Hero>().playerNumber && playerLives > 0) {
							winnerFound = false;
							break;
						}
					}
				} else {
					foreach (GameObject currPlayer in players) {
						int playerTeamNum = currPlayer.GetComponent<Hero>().getTeamNumber();
						
						// check for same teams
						if (playerTeamNum != champTeamNum) {
							int playerLives = currPlayer.GetComponent<Hero>().lives;
						
							if (playerLives > 0) {
								winnerFound = false;
								break;
							}
						
						} else
							numOfPlayers++;	
					}
				}
				
				if (winnerFound)
					break;
			}
		}
		
		if (winnerFound) {
			if (numOfPlayers > 1) { 		// team won
				winner = "Team " + champTeamNum;
				Debug.Log ("Team " + champTeamNum + " WON!");
			}
			else {						// player won
				winner = "Player " + champPlayerNum;
				Debug.Log ("Player " + champPlayerNum + " WON!");
			}
			
			foundWinner = true;
			return winnerFound;
		}
		
		return winnerFound;
	}
	
	public string getWinner() {
		return winner;
	}
	
	void Awake() {
		DontDestroyOnLoad (this);
	}
}
