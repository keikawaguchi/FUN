using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	
	const string FIRE_UNIT_PREFAB_PATH = "Prefabs/Fire";
	
	public float explosionDistanceX = 2.0f;
	public float explosionDistanceZ = 3.0f;
	public float secondsBetweenFireSpawns = 0.5f;
	public float scale = 14.0f;
	
	float timeOfLastFireSpawn;
	int numberOfFireUnitsCreatedX;
	int numberOfFireUnitsCreatedZ;
	
	private bool spawnRight;
	private bool spawnLeft;
	private bool spawnUp;
	private bool spawnDown;
	
	GameObject fireUnit;
	
	private GlobalBehavior globalBehavior;

	void Start() {
		globalBehavior = GameObject.Find("Global Behavior").GetComponent<GlobalBehavior>();
		
		fireUnit = Resources.Load(FIRE_UNIT_PREFAB_PATH) as GameObject;
		if (fireUnit == null) {
			Debug.Log ("Fire is NULL");
		}
		numberOfFireUnitsCreatedX = 0;
		numberOfFireUnitsCreatedZ = 0;
		timeOfLastFireSpawn = -100.0f;	// explode on first update
		
		// attempt to spawn in all directions
		spawnRight = spawnLeft = spawnUp = spawnDown = true;
	}
	
	void Update() {
		if (explosionIsComplete()) {
			destoryExplosionInstance();
		}
		
		if (isTimeToSpawnFireUnit()) {
			spawnNewFireUnits();
		}
	}
	
	private bool isTimeToSpawnFireUnit() {
		return (Time.time - timeOfLastFireSpawn) > secondsBetweenFireSpawns;
	}
	
	private void spawnNewFireUnits() {
		timeOfLastFireSpawn = Time.time;
		spawnFireUnitsInXDirection();		
		spawnFireUnitsInZDirection();
	}
	
	private void spawnFireUnitsInXDirection() {
		Vector3 fireUnitPos;	
		if (numberOfFireUnitsCreatedX < explosionDistanceX) {		

			
			// make sure there isn't an indestructible wall right
			if (spawnRight) {
				fireUnitPos = transform.position;
				fireUnitPos.x += numberOfFireUnitsCreatedX * scale;
				if (gridEmpty(fireUnitPos))
					spawnFireUnit(fireUnitPos);
				else 
					spawnRight = false;
			}
		
			// make sure there isn't an indestructible wall left
			if (spawnLeft) {
				fireUnitPos = transform.position;
				fireUnitPos.x -= numberOfFireUnitsCreatedX * scale;
				if (gridEmpty(fireUnitPos))
					spawnFireUnit(fireUnitPos);
				else 
					spawnLeft = false;
			}
			
			numberOfFireUnitsCreatedX++;
		}	
	}
	
	private void spawnFireUnitsInZDirection() {
		Vector3 fireUnitPos;
		if (numberOfFireUnitsCreatedZ < explosionDistanceZ) {	
			
			// make sure there isn't an indestructible wall up
			if (spawnUp) {
				fireUnitPos = transform.position;
				fireUnitPos.z += numberOfFireUnitsCreatedX * scale;
				if (gridEmpty(fireUnitPos))
					spawnFireUnit(fireUnitPos);
				else 
					spawnUp = false;
			}
			
			// make sure there isn't an indestructible wall down
			if (spawnDown) {
				fireUnitPos = transform.position;
				fireUnitPos.z -= numberOfFireUnitsCreatedX * scale;
				if (gridEmpty(fireUnitPos))
					spawnFireUnit(fireUnitPos);
				else 
					spawnDown = false;
			}
			
			numberOfFireUnitsCreatedZ++;
		}
	}
	
	private void spawnFireUnit(Vector3 position) {
		GameObject newFireUnit = Instantiate(fireUnit) as GameObject;
		
		int x = globalBehavior.getXPos(position.x);
		int y = globalBehavior.getYPos(position.z);
		
		newFireUnit.transform.position = new Vector3(globalBehavior.getXCoord(x), 0, globalBehavior.getYCoord(y));
	}
	
	private bool explosionIsComplete() {
		return numberOfFireUnitsCreatedX > explosionDistanceX 
				&& numberOfFireUnitsCreatedZ > explosionDistanceZ;
	}
	
	private void destoryExplosionInstance() {
		Debug.Log ("Fire destroyed");
		Destroy(gameObject);
	}
	
	private bool gridEmpty(Vector3 fireUnitPos) {
		int x = globalBehavior.getXPos(fireUnitPos.x);
		int y = globalBehavior.getYPos(fireUnitPos.z);
		
		return !globalBehavior.grid[x,y];
	}
}
