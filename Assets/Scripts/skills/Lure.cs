// Lure.cs
// Skill used by Temptress
// Shoots a hook that, if the front of the hook touches an enemey, it pulls 
// the enemy back to the location of the character that shoot the hook.
// Created by: Drew Harvey

using UnityEngine;
using System.Collections;

public class Lure : MonoBehaviour {
	
	private enum State {
		Extending,
		Retracting
	}
	
	#region Class Members
	const string LURE_UNIT_PREFAB_PATH = "Prefabs/Skill_Prefabs/LureUnit";
	
	public float distanceToTravel;
	public float lureUnitsSpawnedPerSecond;
	private float scaleSpeed;
	private float lureUnitSpawnDelayInSeconds;
	private float numberOfLureUnitsToSpawn;
	private float timeOfLastLureUnitSpawn;
	
	private State currentState;
	private Stack lureUnitStack;
	private GameObject lureUnitPrefab;
	#endregion
	
	void Start () {
		loadLureUnitPrefab();
		calculateLureUnitSpawnDelay();
		calculateNumberOfUnitsToSpawn();
		currentState = State.Extending;
		lureUnitStack = new Stack();
		extendLure();
	}
	
	void Update () {
		if (isComplete()) {
			destoryLureObject();
		}
		
		if (isTimeToSpawnLureUnit()) {
			if (currentState == State.Extending) {
				extendLure();
			}
			else {
				retractLure();
			}
		}
	}
	
	#region Public Methods
	public void scaleLureSpeed(float scalePercentage) {
		scaleSpeed = scalePercentage / 100.0f;
	}
	#endregion
	
	#region Initialization Methods
	private void loadLureUnitPrefab() {
		lureUnitPrefab = Resources.Load(LURE_UNIT_PREFAB_PATH) as GameObject;
		if (lureUnitPrefab == null) {
			Debug.Log ("Lure Unit Prefab loaded unsuccessfully");
		}
		else {
			Debug.Log ("Lure Unit Prefab loaded successfully");
		}
	}
	
	private void calculateLureUnitSpawnDelay() {
		lureUnitSpawnDelayInSeconds = 60f / lureUnitsSpawnedPerSecond;
	}
	
	private void calculateNumberOfUnitsToSpawn() {
		float lureUnitHeight = lureUnitPrefab.transform.localScale.z;
		lureUnitSpawnDelayInSeconds = distanceToTravel / lureUnitHeight;
	}
	#endregion
	
	private bool isComplete() {	
		return lureUnitStack.Count == 0;
	}
	
	private void destoryLureObject() {
		Debug.Log("Lure skill complete");
		Destroy(gameObject);
	}
	
	private bool isTimeToSpawnLureUnit() {
		float scaledSpeedTime = (Time.time - timeOfLastLureUnitSpawn) * scaleSpeed;
		return scaledSpeedTime > lureUnitSpawnDelayInSeconds;
	}
	
	private bool isFullyExtended() {
		return lureUnitStack.Count >= numberOfLureUnitsToSpawn;
	}
	
	private void extendLure() {
		Transform newLureUnitPosition;
		Vector3 forwardDirection;
		GameObject lastLureUnit;
		float lureUnitHeight = lureUnitPrefab.transform.localScale.z;
		
		if (lureUnitStack.Count == 0) {
			newLureUnitPosition = this.transform;
		}
		else {
			lastLureUnit = lureUnitStack.Peek() as GameObject;
			newLureUnitPosition = lastLureUnit.transform;		
		}
		
		// Calculate new forward direction for lure unit
		forwardDirection = newLureUnitPosition.transform.forward;
		forwardDirection.y = 0f;
		newLureUnitPosition.transform.forward = forwardDirection;
		
		// Calculate new position for lure unit
		newLureUnitPosition.transform.position += newLureUnitPosition.transform.forward * lureUnitHeight;
		addLureUnit(newLureUnitPosition);
	}
	
	private void retractLure() {
		removeLureUnit();
	}
	
	private void addLureUnit(Transform unitTransform) {
		Debug.Log("Lure unit added");
		timeOfLastLureUnitSpawn = Time.time;
		GameObject newLureUnit = Instantiate(lureUnitPrefab) as GameObject;
		newLureUnit.transform.forward = unitTransform.forward;
		newLureUnit.transform.position = unitTransform.transform.position;
		lureUnitStack.Push(newLureUnit);
	}
	
	private void removeLureUnit() {
		Debug.Log("Lure unit removed");
		GameObject lureUnitToRemove = lureUnitStack.Pop() as GameObject;
		Destroy(lureUnitToRemove);
	}
}
