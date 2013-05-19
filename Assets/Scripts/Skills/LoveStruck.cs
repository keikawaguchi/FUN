using UnityEngine;
using System.Collections;

public class LoveStruck : MonoBehaviour {
	
	public float effectRadius = 35.0f;
	public float speedMultiplier = 0.5f;
	public float duration = 3.0f;
	
	private GridSystem gridSystem;
	private Map map;

	void Start () {
		loadScripts();
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
			if (player != gameObject && distance < effectRadius) {
				setSlowOnPlayer(player);
				showPopupText(player.transform.position);
			}
		}
	}
	
	private void setSlowOnPlayer(GameObject otherPlayer) {
		AlterSpeed alterSpeed;
		alterSpeed = otherPlayer.gameObject.AddComponent<AlterSpeed>();
		alterSpeed.setSpeedMultiplier(speedMultiplier);
		alterSpeed.setDurationInSeconds(duration);
	}
	
	private void showPopupText(Vector3 position) {
		GameObject t = Resources.Load ("Prefabs/Text/PopupText") as GameObject;
		GameObject text = Instantiate(t) as GameObject;		
		PopupText popupText = text.GetComponent<PopupText>();
		popupText.initialize();
		popupText.setPosition(position.x, position.z + 7);
		popupText.setPredefinedText("MinusSpeed");
	}
}
