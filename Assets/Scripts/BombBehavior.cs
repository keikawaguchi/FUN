using UnityEngine;
using System.Collections;

public class BombBehavior : MonoBehaviour {
	
	public float explosionDelayInSeconds = 0.0f;

	float spawnTime;
	
	GameObject explosion;

	// Use this for initialization
	void Start() {
		spawnTime = Time.time;
		explosion = Resources.Load("Prefabs/Explosion") as GameObject;
		if (explosion == null) Debug.Log ("Explosion is NULL");
	}
	
	// Update is called once per frame
	void Update() {
		if (Time.time - spawnTime > explosionDelayInSeconds) {
			explode();
		}
	}
	
	void explode() {
		Debug.Log("Bomb Exploded...");
		Instantiate(explosion);
		Destroy(gameObject);
	}
}
