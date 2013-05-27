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
		bool won = true;
		
		GameObject[] players;
		players = GameObject.FindGameObjectsWithTag("Player");
		
		foreach(GameObject player in players) {
			
			int playerTeamNum = player.GetComponent<Hero>().getTeamNumber();
			
			Debug.Log (teamNum + " " + playerTeamNum);
			
			
			if (playerTeamNum != teamNum) {
				
				int playerLives = player.GetComponent<Hero>().lives;
				
				if (playerLives > 0) {
					won = false;
					break;
				}
			}
		}
		
		if (won) {
			// DO SOMETHING HERE LIKE CHANGE SCENES
			Debug.Log ("Team " + teamNum + " WON!");
		}
		
		return won;
	}
}
