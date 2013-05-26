using UnityEngine;
using System.Collections;

public class IceAgeUnit : MonoBehaviour {
	private const string PLAYER_TAG = "Player";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider collision) {
		// check the collision with tag and exlude the trap owner
		GameObject enemyObj = collision.gameObject;
		if (collision.tag == PLAYER_TAG && enemyObj.name != "Fanndis") {
			AlterSpeed alterSpeed = enemyObj.gameObject.AddComponent<AlterSpeed>();
			alterSpeed.Start (0.5f, 2f);
		}
	}
}
