using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {
	private float visibleInberval;
	
	// Use this for initialization
	void Start () {
		visibleInberval = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		visibleInberval += Time.smoothDeltaTime;
		if (visibleInberval >= 3f)
			renderer.enabled = false;
	}
	
	void OnTriggerEnter(Collider collision) {
		// TO-DO: Stun the enemy on top of the trap
		if (collision.gameObject.name == "Enemy") {  // later need to check by tag
			GameObject enemyObj = GameObject.Find ("Enemy");  // later need to find by tag
			CharacterMovement characterMove = enemyObj.GetComponent<CharacterMovement>();
			
			characterMove.setMovementState (CharacterMovement.MovementState.CannotMove);
			Destroy (gameObject);
		}
	}
}
