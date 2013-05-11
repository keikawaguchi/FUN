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
		Returning
	}
	
	#region Class Members
	const string LURE_UNIT_PREFAB_PATH = "Prefabs/Skill_Prefabs/LureUnit";
	
	public float distanceToTravel;
	public float grabWidth;
	public float scaleSpeed;
	private float lureUnitSpawnDelayInSeconds;
	private float numberOfLureUnitsToSpawn;
	private float timeOfLastLureUnitSpawn;
	
	private State currentState;
	private Stack lureUnitStack;
	private GameObject lureUnitPrefab;
	#endregion
	
	void Start () {
		loadLureUnitPrefab();
		currentState = State.Extending;
		lureUnitStack = new Stack();
	}
	
	void Update () {
		if (isComplete()) {
			destoryLureObject();
		}
		
		if (isTimeToSpawnLureUnit()) {
			
		}
	}
	
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
		float lureUnitHeight = lureUnitPrefab.transform.localScale.z;
		lureUnitSpawnDelayInSeconds = distanceToTravel / lureUnitHeight;
	}
	
	private void calculateNumberOfUnitsToSpawn() {
		float lureUnitHeight = lureUnitPrefab.transform.localScale.z;
		lureUnitSpawnDelayInSeconds = distanceToTravel / lureUnitHeight;
	}
	#endregion
	
	private bool isComplete() {	
		Debug.Log("Lure skill complete");
		return lureUnitStack.Count == 0;
	}
	
	private void destoryLureObject() {
		Debug.Log("Lure skill destoryed");
		Destroy(gameObject);
	}
	
	private bool isTimeToSpawnLureUnit() {
		return (Time.time - timeOfLastLureUnitSpawn) > lureUnitSpawnDelayInSeconds;
	}
	
	private bool isFullyExtended() {
		return lureUnitStack.Count >= numberOfLureUnitsToSpawn;
	}
	
	private void addLureUnit(Vector3 position) {
		Debug.Log("Lure unit added");
		GameObject newLureUnit = Instantiate(lureUnitPrefab) as GameObject;
		newLureUnit.transform.position = position;
		lureUnitStack.Push(newLureUnit);
	}
	
	private void removeLureUnit() {
		Debug.Log("Lure unit removed");
		GameObject lureUnitToRemove = lureUnitStack.Pop() as GameObject;
		Destroy(lureUnitToRemove);
	}
}
