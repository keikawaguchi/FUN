using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public int winner;
	private bool foundWinner = false;
	public bool winnerIsTeam;
	
	// Use this for initialization
	void Start () {
		SetTeams();
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevel == 1)
			Destroy(this);
	}
	
	public void SetTeams() {
		
		// get team settings
		GameObject controls = GameObject.Find("Controls") as GameObject;
		if (controls == null) {
			return;
		}
		
		PlayerControls playerControls = controls.GetComponent<PlayerControls>();

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
		
		// attach ring corresponding to team to each player
		foreach (GameObject player in players) {
			Hero hero = player.GetComponent<Hero>();
			
			if (hero.teamNumber == 1) {
				GameObject animation = Resources.Load ("Prefabs/Team/RedTeamRing") as GameObject;
				animation = Instantiate (animation) as GameObject;
				animation.GetComponent<Animation>().attachToObject (player);
				animation.GetComponent<Animation>().setCustomOffset(new Vector3(0, -1, 4));
			} else if (hero.teamNumber == 2) {
				GameObject animation = Resources.Load ("Prefabs/Team/BlueTeamRing") as GameObject;
				animation = Instantiate (animation) as GameObject;
				animation.GetComponent<Animation>().attachToObject (player);
				animation.GetComponent<Animation>().setCustomOffset(new Vector3(0, -1, 4));
			}
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
				winner = champTeamNum;
				Debug.Log ("Team " + champTeamNum + " WON!");
				winnerIsTeam = true;
			} else {						// player won
				winner = champPlayerNum;
				Debug.Log ("Player " + champPlayerNum + " WON!");
				winnerIsTeam = false;
			}
			GameObject.Find("Mule").GetComponent<VictoryData>().GetData();
			foundWinner = true;
			return winnerFound;
		}
		
		return winnerFound;
	}
	
	public int getWinner() {
		return winner;
	}
	
	void Awake() {
		DontDestroyOnLoad (this);
	}
}
