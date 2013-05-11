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
	private float scaleSpeed = 1f;
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
		
		updateState();
	}
	
	#region Public Methods
	public bool isComplete() {	
		return lureUnitStack.Count < 1;
	}
	
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
		lureUnitSpawnDelayInSeconds = 1.0f / lureUnitsSpawnedPerSecond;
	}
	
	private void calculateNumberOfUnitsToSpawn() {
		float lureUnitHeight = lureUnitPrefab.transform.localScale.z;
		numberOfLureUnitsToSpawn = distanceToTravel / lureUnitHeight;
	}
	#endregion

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
	
	private void updateState() {
		if (isFullyExtended ()) {
			currentState = State.Retracting;
		}
	}
	
	private void extendLure() {
		Transform newLureUnitPosition;
		GameObject lastLureUnit;
		
		timeOfLastLureUnitSpawn = Time.time;
		
		if (lureUnitStack.Count == 0) {
			newLureUnitPosition = this.transform;
		}
		else {
			lastLureUnit = lureUnitStack.Peek() as GameObject;
			newLureUnitPosition = lastLureUnit.transform;		
		}
		
		newLureUnitPosition.transform.forward = calculateForwardDirection(newLureUnitPosition);
		newLureUnitPosition.transform.position = calculatePosition(newLureUnitPosition);
		addLureUnit(newLureUnitPosition);
	}
	
	private void retractLure() {
		removeLureUnit();
	}
	
	private void addLureUnit(Transform unitTransform) {
		GameObject previousLureUnit;
		GameObject newLureUnit;
		
		// previous hook unit can't grab anymore
		if (!isComplete()) {
			previousLureUnit = lureUnitStack.Peek() as GameObject;
			previousLureUnit.GetComponent<LureUnit>().setToGrabPlayers(false);
		}
		
		newLureUnit = Instantiate(lureUnitPrefab) as GameObject;
		newLureUnit.transform.forward = unitTransform.forward;
		newLureUnit.transform.position = unitTransform.transform.position;
		newLureUnit.GetComponent<LureUnit>().setToGrabPlayers(true);
		lureUnitStack.Push(newLureUnit);
		Debug.Log("Lure unit added");
	}
	
	private void removeLureUnit() {
		if (isComplete()) {
			return;
		}
		
		Debug.Log("Lure unit removed");
		GameObject lureUnitToRemove = lureUnitStack.Pop() as GameObject;
		Destroy(lureUnitToRemove);
	}
	
	private Vector3 calculateForwardDirection(Transform newLureUnitPosition) {
		Vector3 forwardDirection;
		forwardDirection = newLureUnitPosition.transform.forward;
		forwardDirection.y = 0f;
		return forwardDirection;
	}
	
	private Vector3 calculatePosition(Transform newLureUnitPosition) {
		float lureUnitHeight;
		Vector3 position;
		lureUnitHeight = lureUnitPrefab.transform.localScale.z;
		position = newLureUnitPosition.transform.forward * lureUnitHeight;
		position += newLureUnitPosition.transform.position;
		return position;
	}
	
	private void showVariableValues() {
		Debug.Log("distanceToTravel: " + distanceToTravel);
		Debug.Log("lureUnitsSpawnedPerSecond: " + lureUnitsSpawnedPerSecond);
		Debug.Log("scaleSpeed: " + scaleSpeed);
		Debug.Log("lureUnitSpawnDelayInSeconds: " + lureUnitSpawnDelayInSeconds);
		Debug.Log("numberOfLureUnitsToSpaw: " + numberOfLureUnitsToSpawn);
		Debug.Log("timeOfLastLureUnitSpawn: " + timeOfLastLureUnitSpawn);
	}
}
