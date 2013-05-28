using UnityEngine;
using System.Collections;

public class Chinmoku : MonoBehaviour {
	private float CHINMOKU_EFFECT_DURATION = 5F;
	
	private float effectRadius;
	
	private GridSystem gridSystem;
	private Map map;
	private GameObject owner;
	private GameObject animation;
	
	// Use this for initialization
	void Start () {
		
	}
	
	public void execute() {
		loadScripts();
		
		GameObject animationInstance = Instantiate (animation) as GameObject;
		animationInstance.GetComponent<Animation>().attachToObject(gameObject);
		animationInstance.GetComponent<Animation>().setAlignment("center");
		
		effectRadius = gridSystem.getSingleGridHeight() * 3;  // 3 units of effect radius
		checkForPlayersInRadius();
	}
	
	public void setOwner(GameObject owner) {
		this.owner = owner;
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
					setSilenceOnPlayer(player);
		}
	}
	
	private void setSilenceOnPlayer(GameObject otherPlayer) {
		AlterSilence alterSilence;
		alterSilence = otherPlayer.AddComponent<AlterSilence>();
		alterSilence.Start (true, CHINMOKU_EFFECT_DURATION);
	}
}
