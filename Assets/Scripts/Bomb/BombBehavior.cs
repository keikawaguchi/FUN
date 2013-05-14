using UnityEngine;
using System.Collections;

public class BombBehavior : MonoBehaviour {
	
	const string EXPLOSION_PREFAB_PATH = "Prefabs/Bomb/Explosion";
	
	public float explosionPrefabDelayInSeconds = 1.5f;
	public float bombX = 4;
	public float bombZ = 3;
	private float spawnTime;
	
	private GameObject explosionPrefab;
	
	private Map map;

	void Start() {
		loadBombPrefab();
		loadScripts();
		spawnTime = Time.time;
	}
	
	public void setter(float x, float z) {
		bombX = x;
		bombZ = z;
	}
	
	void Update() {
		if (isTimeToExplode()) {
			explode();
		}
	}
	
	public void OnTriggerEnter(Collider theCollision) {
		if(theCollision.gameObject.name == "Explosion") {
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
	
	private void loadScripts() {
		map = GameObject.Find("Map").GetComponent<Map>();
	}
	
	private void addBombToMap() {
		bool addedBombToMap;
		addedBombToMap = 
			map.addImpassableObject(transform.position.x, transform.position.z, this.gameObject);
		if (!addedBombToMap) {
			Destroy(gameObject);
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
