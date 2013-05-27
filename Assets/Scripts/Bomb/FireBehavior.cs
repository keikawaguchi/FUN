using UnityEngine;
using System.Collections;

public class FireBehavior : MonoBehaviour {
	
	float lifeSpanInSeconds = 0.1f;
	float spawnTime;
	
	public Hero owner;

	// Use this for initialization
	void Start () {
		spawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - spawnTime > lifeSpanInSeconds) {
			Destroy (gameObject);
		}
	}
	
	public void setHero(Hero owner) {
		this.owner = owner;	
	}
}
