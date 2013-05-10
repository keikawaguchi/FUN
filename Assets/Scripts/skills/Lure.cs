// Lure.cs
// Skill used by Temptress
// Shoots a hook that, if the front of the hook touches an enemey, it pulls 
// the enemy back to the location of the character that shoot the hook.
// Created by: Drew Harvey

using UnityEngine;
using System.Collections;

public class Lure : MonoBehaviour {
	
	#region Class Members
	const string LURE_UNIT_PREFAB_PATH = "Prefabs/Skill_Prefabs/LureUnit";
	
	public float distance;
	public float width;
	public float speed;
	public float coolDownInSeconds;
	
	private Stack lureUnitStack;
	
	private GameObject lureUnitPrefab;
	#endregion
	
	void Start () {
		loadLureUnitPrefab();
		lureUnitStack = new Stack();
	}
	
	void Update () {
		
	}
	
	private void loadLureUnitPrefab() {
		lureUnitPrefab = Resources.Load(LURE_UNIT_PREFAB_PATH) as GameObject;
		if (lureUnitPrefab == null) {
			Debug.Log ("Lure Unit loaded unsuccessfully");
		}
		else {
			Debug.Log ("Lure Unit loaded successfully");
		}
	}
	
	private void addLureUnit() {
		GameObject newLureUnit = Instantiate(lureUnitPrefab) as GameObject;
		lureUnitStack.Push(newLureUnit);
	}
	
	private void removeLureUnit() {
		GameObject lureUnitToRemove = lureUnitStack.Pop() as GameObject;
		Destroy(lureUnitToRemove);
	}
}
