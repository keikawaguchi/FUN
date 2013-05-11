using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {
	
	const string BOMB_PREFAB_PATH = "Prefabs/Bomb";
	const string BOMB_DROP_BUTTON = "Jump";
	
	private GameObject bomb;
	
	// Use this for initialization
	void Start () {
		bomb = Resources.Load (BOMB_PREFAB_PATH) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
		// Create bomb on spacebar down
		if (Input.GetButtonDown (BOMB_DROP_BUTTON)) {
			GameObject instantiateBomb = Instantiate (bomb) as GameObject;
			instantiateBomb.transform.position = this.transform.position;
		}
	}
}
