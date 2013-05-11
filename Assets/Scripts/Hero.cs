using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	const string BOMB_PREFAB_PATH = "Prefabs/Bomb";
	const string BOMB_DROP_BUTTON = "Jump";
	
	private GameObject bomb;
	
	private CharacterMovement characterMovement;

	void Start () {
		loadResources();
		loadScripts();
	}
	
	void Update () {
		
		// Create bomb on spacebar down
		if (Input.GetButtonDown (BOMB_DROP_BUTTON)) {
			GameObject instantiateBomb = Instantiate (bomb) as GameObject;
			instantiateBomb.transform.position = this.transform.position;
		}
	}
	
	private void loadResources() {
		bomb = Resources.Load (BOMB_PREFAB_PATH) as GameObject;
		if (bomb == null) {
			Debug.Log("Bomb prefab is NULL");
		}
	}
	
	
	private void loadScripts() {
		characterMovement = GetComponent<CharacterMovement>();
		if (characterMovement == null) {
			Debug.Log("CharacterMovement script is NULL");
		}
	} 
}
