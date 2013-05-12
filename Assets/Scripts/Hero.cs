using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	const string BOMB_PREFAB_PATH = "Prefabs/Bomb";
	const string BOMB_DROP_BUTTON = "Jump";
	
	private GameObject bomb;
	private GridSystem gridSystem;
	private CharacterMovement characterMovement;

	void Start () {
		loadResources();
		loadScripts();
	}
	
	void Update () {
		
		// Create bomb on spacebar down
		if (Input.GetButtonDown (BOMB_DROP_BUTTON)) {
			GameObject instantiateBomb = Instantiate (bomb) as GameObject;
			
			// place bomb in closest grid position
			int xCoord = gridSystem.getXPos(transform.position.x);
			int yCoord = gridSystem.getYPos(transform.position.z);
			
			Vector3 bombLocation = new Vector3(gridSystem.getXCoord(xCoord), 0f, gridSystem.getYCoord(yCoord));
			instantiateBomb.transform.position = bombLocation;
		}
	}
	
	#region Initialization Methods
	private void loadResources() {
		bomb = Resources.Load (BOMB_PREFAB_PATH) as GameObject;
		if (bomb == null) {
			Debug.Log("Bomb prefab is NULL");
		}
	}
	
	private void loadScripts() {
		gridSystem = GetComponent<GridSystem>();
		if (gridSystem == null) {
			Debug.Log ("Hero.cs: Grid system is null");
		}
		
		characterMovement = GetComponent<CharacterMovement>();
		if (characterMovement == null) {
			Debug.Log("CharacterMovement script is NULL");
		}
	} 
	#endregion
}
