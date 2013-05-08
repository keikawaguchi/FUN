using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	
	public float explosionDistanceX = 5.0f;
	public float explosionDistanceZ = 5.0f;
	public float secondsBetweenFireSpawns = 0.5f;
	public float scale = 10.0f;
	
	float timeOfLastFireSpawn;
	int numberOfFireUnitsCreatedX;
	int numberOfFireUnitsCreatedY;
	
	GameObject fire;

	// Use this for initialization
	void Start() {
		fire = Resources.Load("Prefabs/Fire") as GameObject;
		if (fire == null) Debug.Log ("Fire is NULL");
		numberOfFireUnitsCreatedX = 0;
		numberOfFireUnitsCreatedY = 0;
		timeOfLastFireSpawn = -100.0f;	// explode on first update
	}
	
	// Update is called once per frame
	void Update() {
		if (numberOfFireUnitsCreatedX > explosionDistanceX 
			&& numberOfFireUnitsCreatedY > explosionDistanceZ) {
			Debug.Log ("Fire destroyed");
			Destroy(gameObject);
			return;
		}
		
		if ((Time.time - timeOfLastFireSpawn) > secondsBetweenFireSpawns) {
			timeOfLastFireSpawn = Time.time;
			Vector3 fireUnitPos;
			
			if (numberOfFireUnitsCreatedX < explosionDistanceX) {
			
				// right
				Debug.Log("Right explosion");
				fireUnitPos = transform.position;
				fireUnitPos.x += numberOfFireUnitsCreatedX * scale;
				spawnFireUnit(fireUnitPos);
				
				// left
				Debug.Log("Left explosion");
				fireUnitPos = transform.position;
				fireUnitPos.x -= numberOfFireUnitsCreatedX * scale;
				spawnFireUnit (fireUnitPos);
				
				numberOfFireUnitsCreatedX++;
			}
			
			if (numberOfFireUnitsCreatedY < explosionDistanceZ) {
			
				// up
				Debug.Log("Up explosion");
				fireUnitPos = transform.position;
				fireUnitPos.z += numberOfFireUnitsCreatedY * scale;
				spawnFireUnit(fireUnitPos);
				
				// down
				Debug.Log("Down explosion");
				fireUnitPos = transform.position;
				fireUnitPos.z -= numberOfFireUnitsCreatedY * scale;
				spawnFireUnit(fireUnitPos);
				
				numberOfFireUnitsCreatedY++;
			}
		}
	}
	
	void spawnFireUnit(Vector3 position) {
		GameObject fireUnit = Instantiate(fire) as GameObject;
		fireUnit.transform.position = position;
	}
}
