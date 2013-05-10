using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	
	const string FIRE_UNIT_PREFAB_PATH = "Prefabs/Fire";
	
	public float explosionDistanceX = 5.0f;
	public float explosionDistanceZ = 5.0f;
	public float secondsBetweenFireSpawns = 0.5f;
	public float scale = 10.0f;
	
	float timeOfLastFireSpawn;
	int numberOfFireUnitsCreatedX;
	int numberOfFireUnitsCreatedZ;
	
	GameObject fireUnit;

	void Start() {
		fireUnit = Resources.Load(FIRE_UNIT_PREFAB_PATH) as GameObject;
		if (fireUnit == null) {
			Debug.Log ("Fire is NULL");
		}
		numberOfFireUnitsCreatedX = 0;
		numberOfFireUnitsCreatedZ = 0;
		timeOfLastFireSpawn = -100.0f;	// explode on first update
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
	}
	
	private void spawnFireUnitsInZDirection() {
		Vector3 fireUnitPos;
		if (numberOfFireUnitsCreatedZ < explosionDistanceZ) {	
			
			// up
			Debug.Log("Up explosion");
			fireUnitPos = transform.position;
			fireUnitPos.z += numberOfFireUnitsCreatedZ * scale;
			spawnFireUnit(fireUnitPos);	
			
			// down
			Debug.Log("Down explosion");
			fireUnitPos = transform.position;
			fireUnitPos.z -= numberOfFireUnitsCreatedZ * scale;
			spawnFireUnit(fireUnitPos);
			
			numberOfFireUnitsCreatedZ++;
		}
	}
	
	private void spawnFireUnit(Vector3 position) {
		GameObject newFireUnit = Instantiate(fireUnit) as GameObject;
		newFireUnit.transform.position = position;
	}
	
	private bool explosionIsComplete() {
		return numberOfFireUnitsCreatedX > explosionDistanceX 
				&& numberOfFireUnitsCreatedZ > explosionDistanceZ;
	}
	
	private void destoryExplosionInstance() {
		Debug.Log ("Fire destroyed");
		Destroy(gameObject);
	}
}
