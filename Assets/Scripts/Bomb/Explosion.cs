using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Explosion : MonoBehaviour {
	
	const string FIRE_UNIT_PREFAB_PATH = "Prefabs/Bomb/Fire";
	
	public float explosionDistanceX = 4.0f;
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
	
	private GridSystem gridSystem;
	private Map map;

	void Start() {
		gridSystem = GameObject.Find("Map").GetComponent<GridSystem>();
		map = GameObject.Find("Map").GetComponent<Map>();
		
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

			// make sure there isn't a wall on the right
			if (spawnRight) {
				fireUnitPos = transform.position;
				fireUnitPos.x += numberOfFireUnitsCreatedX * scale;
				if (!map.isGridFull(fireUnitPos.x, fireUnitPos.z))
					spawnFireUnit(fireUnitPos);
				else {
					destroyDestructible(fireUnitPos);
					spawnRight = false;
				}
			}
		
			// make sure there isn't an indestructible wall left
			if (spawnLeft) {
				fireUnitPos = transform.position;
				fireUnitPos.x -= numberOfFireUnitsCreatedX * scale;
				if (!map.isGridFull(fireUnitPos.x, fireUnitPos.z))
					spawnFireUnit(fireUnitPos);
				else {
					destroyDestructible(fireUnitPos);
					spawnLeft = false;
				}
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
				if (!map.isGridFull(fireUnitPos.x, fireUnitPos.z))
					spawnFireUnit(fireUnitPos);
				else {
					destroyDestructible(fireUnitPos);
					spawnUp = false;
				}
			}
			
			// make sure there isn't an indestructible wall down
			if (spawnDown) {
				fireUnitPos = transform.position;
				fireUnitPos.z -= numberOfFireUnitsCreatedX * scale;
				if (!map.isGridFull(fireUnitPos.x, fireUnitPos.z))
					spawnFireUnit(fireUnitPos);
				else {
					destroyDestructible(fireUnitPos);
					spawnDown = false;
				}
			}
			
			numberOfFireUnitsCreatedZ++;
		}
	}
	
	private void spawnFireUnit(Vector3 position) {
		GameObject newFireUnit = Instantiate(fireUnit) as GameObject;
		
		int x = gridSystem.getXPos(position.x);
		int y = gridSystem.getYPos(position.z);
		
		newFireUnit.transform.position = new Vector3(gridSystem.getXCoord(x), 0, gridSystem.getYCoord(y));
	}
	
	private bool explosionIsComplete() {
		return numberOfFireUnitsCreatedX > explosionDistanceX 
				&& numberOfFireUnitsCreatedZ > explosionDistanceZ;
	}
	
	private void destoryExplosionInstance() {
		Debug.Log ("Fire destroyed");
		Destroy(gameObject);
	}
	
	private void destroyDestructible(Vector3 fireUnitPos) {
		
		DestructibleWall[] walls = FindObjectsOfType(typeof(DestructibleWall)) as DestructibleWall[];
        foreach (DestructibleWall wall in walls) {
			
			int x = gridSystem.getXPos(fireUnitPos.x);
			int y  = gridSystem.getYPos(fireUnitPos.z);
						
			if (x == gridSystem.getXPos(wall.transform.position.x) && y == gridSystem.getYPos(wall.transform.position.z)) {
				wall.transform.position = new Vector3(-200f, 0, 0);
				map.grid[x,y] = false;
				break;
			}
        }
	}
}
