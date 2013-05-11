using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {
	
	private GameObject bomb;
	
	// Use this for initialization
	void Start () {
		bomb = Resources.Load ("Prefabs/Bomb") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
		// Create bomb on spacebar down
		if (Input.GetButtonDown ("Jump")) {
			GameObject instantiateBomb = Instantiate (bomb) as GameObject;
			instantiateBomb.transform.position = new Vector3(transform.position.x, 10.0f, transform.position.z);
		}
	}
}
