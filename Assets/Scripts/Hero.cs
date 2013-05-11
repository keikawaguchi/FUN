using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	const string BOMB_PREFAB_PATH = "Prefabs/Bomb";
	const string BOMB_DROP_BUTTON = "Jump";
	
	private GameObject bomb;
	private GlobalBehavior globalBehavior;
	
	private CharacterMovement characterMovement;

	void Start () {
		loadResources();
		loadScripts();
		
		globalBehavior = GameObject.Find("Global Behavior").GetComponent<GlobalBehavior>();
	}
	
	void Update () {
		
		// Create bomb on spacebar down
		if (Input.GetButtonDown (BOMB_DROP_BUTTON)) {
			GameObject instantiateBomb = Instantiate (bomb) as GameObject;
			
			// place bomb in closest grid position
			int xCoord = globalBehavior.getXPos(transform.position.x);
			int yCoord = globalBehavior.getYPos(transform.position.z);
			
			Vector3 bombLocation = new Vector3(globalBehavior.getXCoord(xCoord), 0f, globalBehavior.getYCoord(yCoord));
			instantiateBomb.transform.position = bombLocation;
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
