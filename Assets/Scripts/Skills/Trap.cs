using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {
	private const string PLAYER_TAG = "Player";
	
	private GameObject trapOwner;
	private bool trapPlaced;
	
	// Use this for initialization
	void Start () {
		trapPlaced = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!trapPlaced)  // don't know why i need this, it instantiate a lot of trap
			trapPlaced = true;
	}
	
	void OnTriggerEnter(Collider collision) {
		
	}
	
	public void SetTrapOwner(GameObject owner) {
		trapOwner = owner;
	}
}
