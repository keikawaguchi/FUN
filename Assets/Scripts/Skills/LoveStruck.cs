using UnityEngine;
using System.Collections;

public class LoveStruck : MonoBehaviour {
	
	public int effectRadius = 3;
	public float speedMultiplier = 0.5f;
	public float duration = 3.0f;
	
	private GridSystem gridSystem;
	private Map map;

	void Start () {
		loadScripts();
	}
	
	public void execute() {
		checkForPlayer();
	}
	
	private void loadScripts() {
		gridSystem = GameObject.Find("Map").GetComponent<GridSystem>(); 
		map = GameObject.Find("Map").GetComponent<Map>();
	}
	
	private void checkForPlayer() {
		GameObject otherPlayer;
		int gridPosX = gridSystem.getXPos(transform.position.x);
		int gridPosY = gridSystem.getYPos(transform.position.z);
		
		// X direction
		for (int i = 0; i < effectRadius; i++) {
			otherPlayer = map.getObjectAtGridLocation(gridPosX + i, gridPosY);
			if(otherPlayer != null && otherPlayer != gameObject && otherPlayer.tag == "Player") {
				setSlowOnPlayer(otherPlayer);
				return;
			}	
		}
		// Y direction
		for (int i = 0; i < effectRadius; i++) {
			otherPlayer = map.getObjectAtGridLocation(gridPosX, gridPosY + i);
			if(otherPlayer != null && otherPlayer != gameObject && otherPlayer.tag == "Player") {
				setSlowOnPlayer(otherPlayer);
				return;
			}	
		}
	}
	
	private void setSlowOnPlayer(GameObject otherPlayer) {
		AlterSpeed alterSpeed;
		alterSpeed = otherPlayer.gameObject.AddComponent<AlterSpeed>();
		alterSpeed.setSpeedMultiplier(speedMultiplier);
		alterSpeed.setDurationInSeconds(duration);
	}
}
