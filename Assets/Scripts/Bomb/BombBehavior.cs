using UnityEngine;
using System.Collections;

public class BombBehavior : MonoBehaviour {
	
	const string EXPLOSION_PREFAB_PATH = "Prefabs/Bomb/Explosion";
	
	public float explosionPrefabDelayInSeconds = 1.5f;
	private float spawnTime;
	
	GameObject explosionPrefab;
	
	public float bombX = 4;
	public float bombZ = 3;
	
	void Start() {
		loadBombPrefab();
		spawnTime = Time.time;
	}
	
	public void setter(float x, float z)
	{
		bombX = x;
		bombZ = z;
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
		explosionPrefab = Resources.Load(EXPLOSION_PREFAB_PATH) as GameObject;
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
		Explosion boom;
		Debug.Log("Bomb Exploded...");
		GameObject explosion = Instantiate(explosionPrefab) as GameObject;
		boom = explosion.GetComponent<Explosion>();
		boom.setter(bombX,bombZ);
		explosion.transform.position = this.transform.position;
		Destroy(gameObject);
	}
}
