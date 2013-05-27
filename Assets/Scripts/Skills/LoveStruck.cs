using UnityEngine;
using System.Collections;

public class LoveStruck : MonoBehaviour {
	
	public float effectRadius = 35.0f;
	public float speedMultiplier = 0.5f;
	public float duration = 3.0f;
	
	private GameObject animation;
	private GridSystem gridSystem;
	private Map map;

	void Start () {
		loadScripts();
	}
	
	public void execute() {
		GameObject animationInstance = Instantiate (animation) as GameObject;
		animationInstance.GetComponent<Animation>().attachToObject(gameObject);
		checkForPlayersInRadius();
	}
	
	private void loadScripts() {
		gridSystem = GameObject.Find("Map").GetComponent<GridSystem>(); 
		map = GameObject.Find("Map").GetComponent<Map>();
		animation = Resources.Load ("Prefabs/Animations/LoveStruck-Animation") as GameObject;
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
					setSlowOnPlayer(player);
		}
	}
	
	private void setSlowOnPlayer(GameObject otherPlayer) {
		AlterSpeed alterSpeed;
		alterSpeed = otherPlayer.gameObject.AddComponent<AlterSpeed>();
		alterSpeed.Start(speedMultiplier, duration);
	}
}
