using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
	
//	private IEnumerator Wait(float sec) {
//		yield return new WaitForSeconds(sec);
//	}
}
