using UnityEngine;
using System.Collections;

public class DestructibleWall : MonoBehaviour {
	
	private GlobalBehavior globalBehavior;
	
	// Use this for initialization
	void Start () {
		globalBehavior = GameObject.Find("Global Behavior").GetComponent<GlobalBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider collisionObject) {
		if (collisionObject.gameObject.name == "Fire(Clone)") {
			
			// check if explosion came from same row or column
			/*
			if ((collisionObject.transform.position.x == transform.position.x) || (collisionObject.transform.position.z == transform.position.z)) {
				
				// destroy wall
				Destroy(gameObject);
				
				// update grid array
				int x = globalBehavior.getXPos(transform.position.x);
				int y = globalBehavior.getYPos(transform.position.z);
				
				globalBehavior.grid[x,y] = false;
			}
			*/
		}

	}
	
	public void initialize(float x, float z) {
		transform.position = new Vector3(x, 0, z);
	}
}
