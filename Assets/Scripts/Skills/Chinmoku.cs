using UnityEngine;
using System.Collections;

public class Chinmoku : MonoBehaviour {
	private float effectRadius;
	
	private GridSystem gridSystem;
	private Map map;
	
	// Use this for initialization
	void Start () {
		loadScripts();
		effectRadius = gridSystem.getSingleGridHeight() * 5;  // 5 units of effect radius
	}
	
	public void execute() {
		checkForPlayersInRadius();
	}
	
	private void loadScripts() {
		gridSystem = GameObject.Find("Map").GetComponent<GridSystem>(); 
		map = GameObject.Find("Map").GetComponent<Map>();
	}
	
	private void checkForPlayersInRadius() {	
		GameObject[] players;
		players = GameObject.FindGameObjectsWithTag("Player");	
		
		float distance;
		foreach(GameObject player in players) {
			distance = Vector3.Distance (transform.position, player.transform.position);
			
			int teamNum = gameObject.GetComponent<Hero>().getTeamNumber();
			
			if (player != gameObject && distance < effectRadius)
				if (teamNum != player.GetComponent<Hero>().getTeamNumber())
					setSilenceOnPlayer(player);
		}
	}
	
	private void setSilenceOnPlayer(GameObject player) {
		
	}
}
