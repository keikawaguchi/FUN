using UnityEngine;
using System.Collections;

public class DestructibleWall : MonoBehaviour {
	
	private Map map;
	private GridSystem gridSystem;
	
	// Use this for initialization
	void Start () {
		loadScripts();
	}
	
	void OnTriggerEnter(Collider collisionObject) {
		if (collisionObject.gameObject.name == "Fire(Clone)") {
			
			// check if explosion came from same row or column
			/*
			if ((collisionObject.transform.position.x == transform.position.x) || (collisionObject.transform.position.z == transform.position.z)) {
				
				// destroy wall
				Destroy(gameObject);
				
				// update grid array
				int x = map.getXPos(transform.position.x);
				int y = map.getYPos(transform.position.z);
				
				map.grid[x,y] = false;
			}
			*/
		}

	}
	
	private void loadScripts() {
		map = GameObject.Find("Map").GetComponent<Map>();
		gridSystem = GameObject.Find("Map").GetComponent<GridSystem>();
	}
	
	public void initialize(float x, float z) {
		transform.position = new Vector3(x, 0, z);
	}
}
