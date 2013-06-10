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
		
		AudioClip sfx = Resources.Load ("Audio/SFX/steamBlast") as AudioClip;
		AudioSource.PlayClipAtPoint(sfx, transform.position, 1);
		
		GameObject animationInstance = Instantiate (animation) as GameObject;
		animationInstance.GetComponent<Animation>().setCustomOffset(new Vector3(0, 25, 0));
		animationInstance.GetComponent<Animation>().setPosition(transform.position);

		effectRadius = gridSystem.getSingleGridHeight() * 3;  // 3 units of effect radius
		checkForPlayersInRadius();
	}
	
	public void setOwner(GameObject owner) {
		this.owner = owner;
	}
	
	private void loadScripts() {
		gridSystem = GameObject.Find("Map").GetComponent<GridSystem>(); 
		map = GameObject.Find("Map").GetComponent<Map>();
		animation = Resources.Load ("Prefabs/Animations/PoisonCloud") as GameObject;
	}
	
	private void checkForPlayersInRadius() {
		GameObject[] players;
		players = GameObject.FindGameObjectsWithTag("Player");	
		
		float distance;
		foreach(GameObject player in players) {
			distance = Vector3.Distance (transform.position, player.transform.position);
			
			int teamNum = gameObject.GetComponent<Hero>().getTeamNumber();
			if (player != gameObject && distance < effectRadius) {
				if (teamNum == 0)
					setConfusionOnPlayer(player);
				else if (teamNum != player.GetComponent<Hero>().getTeamNumber())
					setConfusionOnPlayer(player);
			}
		}
	}
	
	private void setConfusionOnPlayer(GameObject otherPlayer) {
		AlterSpeed alterSpeed;
		alterSpeed = otherPlayer.gameObject.AddComponent<AlterSpeed>();
		alterSpeed.Start(-1.0f, CHINMOKU_EFFECT_DURATION);
	}
}
