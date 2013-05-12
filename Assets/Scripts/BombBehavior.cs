using UnityEngine;
using System.Collections;

public class BombBehavior : MonoBehaviour {
	
	const string EXPLOSION_PREFAB_PATH = "Prefabs/Explosion";
	
	public float explosionPrefabDelayInSeconds = 1.5f;
	private float spawnTime;
	
	GameObject explosionPrefab;

	
	void Start() {
		loadBombPrefab();
		spawnTime = Time.time;
	}

	void Update() {
		if (isTimeToExplode()) {
			explode();
		}
	}
	
	public void OnTriggerEnter(Collider theCollision)
	{
		if(theCollision.gameObject.name == "Explosion")
		{
			explode();
		}
	}
	
	#region Initialization Methods
	private void loadBombPrefab() {
		explosionPrefab = Resources.Load("Prefabs/Explosion") as GameObject;
		if (explosionPrefab == null) {
			Debug.Log ("Explosion loaded unsuccessfully");
		}
		else {
			Debug.Log ("Explosion loaded successfully");
		}
	}
	#endregion
	
	private bool isTimeToExplode() {
		return (Time.time - spawnTime) > explosionPrefabDelayInSeconds;
	}
	
	void explode() {
		Debug.Log("Bomb Exploded...");
		GameObject explosion = Instantiate(explosionPrefab) as GameObject;
		explosion.transform.position = this.transform.position;
		Destroy(gameObject);
	}
}
