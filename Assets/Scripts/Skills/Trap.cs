using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {
	private string collisionObjectName;
	private float visibleInberval;
	private bool trapPlaced;
	
	// Use this for initialization
	void Start () {
		visibleInberval = 0f;
		trapPlaced = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!trapPlaced) {  // don't know why i need this, it instantiate a lot of trap
			collisionObjectName = "EnemyForTest";  // how do i know who's colliding?
			
			trapPlaced = true;
		}
		visibleInberval += Time.smoothDeltaTime;
		if (visibleInberval >= 3f) {
			renderer.enabled = false;
		}
	}
	
	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.name == collisionObjectName) {  // later need to check by tag
			GameObject enemyObj = GameObject.Find (collisionObjectName);  // later need to find by tag
			CharacterMovement characterMove = enemyObj.GetComponent<CharacterMovement>();
			characterMove.setMovementState (CharacterMovement.MovementState.CannotMove);
			Destroy (gameObject);
		}
	}
}
