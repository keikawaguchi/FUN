using UnityEngine;
using System.Collections;

public class UpgradePickUp : MonoBehaviour {
	
	const string SPEED_UPGRADE_PREFAB = "Materials/SpeedUpgrade";
	const string EXPLOSION_UPGRADE_PREFAB = "Materials/ExplosionUpgrade";
	const string BOMB_UPGRADE_PREFAB = "Materials/BombUpgrade";
	
	public float upgradeRespawnTime = 30.0f;
	private int upgradeType;
	private float timeOfLastSpawn;
	
	void Start ()  {
		spawnRandomUpgrade();
	}
	
	void Update() {
		if (shouldSpawnNewUpgrade()) {
			spawnRandomUpgrade();
		}
	}
	
	public void OnTriggerEnter(Collider player) {
		if (player.tag == "Player") {
			StartCoroutine(upgradePickedUp());
		}
	}
	
	private bool shouldSpawnNewUpgrade() {
		return (Time.time - timeOfLastSpawn > upgradeRespawnTime)
			&& (renderer.enabled == false);
	}
	
	private IEnumerator upgradePickedUp() {
		GameObject t = Resources.Load ("Prefabs/Text/PopupText") as GameObject;
		GameObject text = Instantiate(t) as GameObject;	
		
		PopupText popupText = text.GetComponent<PopupText>();
		popupText.initialize();
		popupText.setDuration(3.0f);
		popupText.setPosition(transform.position.x, transform.position.z + 5);
		
		if(renderer.name == "SpeedUpgrade") {		
			popupText.setPredefinedText("PlusSpeed");	
		}
		if(renderer.name == "ExplosionUpgrade") {
			popupText.setPredefinedText("PlusExplosion");
		}
		if(renderer.name == "BombUpgrade") {
			popupText.setPredefinedText("PlusBombs");
		}
		
		renderer.enabled = false;
		gameObject.collider.enabled = false;
		
		yield return new WaitForSeconds(3);
	}
	
	private void spawnRandomUpgrade() {
		timeOfLastSpawn = Time.time;
		upgradeType = Random.Range(1,4);
		
		if( upgradeType == 1) {
			GetComponent<MeshRenderer>().renderer.material = Resources.Load (SPEED_UPGRADE_PREFAB) as Material;
			renderer.name = "SpeedUpgrade";
		}
		
		if( upgradeType == 2 ) {
			GetComponent<MeshRenderer>().renderer.material = Resources.Load (EXPLOSION_UPGRADE_PREFAB) as Material;
			renderer.name = "ExplosionUpgrade";
		}
		
		if( upgradeType == 3 ) {
			GetComponent<MeshRenderer>().renderer.material = Resources.Load (BOMB_UPGRADE_PREFAB) as Material;
			renderer.name = "BombUpgrade";
		}
		
		renderer.enabled = true;
		gameObject.collider.enabled = true;
	}
}
