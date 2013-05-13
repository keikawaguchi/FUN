using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	const string BOMB_PREFAB_PATH = "Prefabs/Bomb/Bomb";
	const string BOMB_DROP_BUTTON = "Jump";
	
	public float dropBombCoolDownSeconds = 1.5f;
	private float timeOfLastBombDrop;
	public float bombX = 4;
	public float bombZ = 3;
	
	private GameObject bomb;
	private GridSystem gridSystem;

	void Start () {
		loadResources();
		loadScripts();
		timeOfLastBombDrop = -999f;
	}
	
	void Update () {	
		handleControllerInput();
	}
	
	public void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "KillsPlayer") {
			Debug.Log("Player DEAD");
			renderer.material.color = new Color(1.0f, 0.0f, 0.0f);
		}
		
		if(collider.name == "BombUpgrade")
		{
			if(dropBombCoolDownSeconds > 0)
				dropBombCoolDownSeconds-=.5f;
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
		gridSystem = GameObject.Find("Map").GetComponent<GridSystem>();
		if (gridSystem == null) {
			Debug.Log ("Hero.cs: Grid system is null");
		}
	} 
	#endregion
	
	private void handleControllerInput() {
		if (Input.GetButtonDown (BOMB_DROP_BUTTON)) {
			dropBomb();
		}
	}
	
	private void dropBomb() {
		if (Time.time - timeOfLastBombDrop < dropBombCoolDownSeconds) {
			return;
		}
		GameObject instantiateBomb = Instantiate (bomb) as GameObject;

		// place bomb in closest grid position
		int xCoord = gridSystem.getXPos(transform.position.x);
		int yCoord = gridSystem.getYPos(transform.position.z);
		Vector3 bombLocation = new Vector3(gridSystem.getXCoord(xCoord), 0f, gridSystem.getYCoord(yCoord));
		instantiateBomb.transform.position = bombLocation;
		
		timeOfLastBombDrop = Time.time;
	}
}
