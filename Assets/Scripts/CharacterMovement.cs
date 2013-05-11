using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	
	public enum MovementState {
		CanMove,
		CannotMove
	}
	
	public float speed;
	private Vector3 aimDirection;
	MovementState currentMovementState;
	
	
	public void Start() {
		currentMovementState = MovementState.CanMove;
	}
	
	public void Update() {
		if (currentMovementState == MovementState.CanMove) {
			updateMovement();
			updateAimDirection();
		}
	}

	#region Public Methods
	public void setState(MovementState newState) {
		currentMovementState = newState;
	}
	
	public Vector3 getAimDirection() {
		return aimDirection;
	}
	#endregion
	
	private void updateMovement() {
		Vector3 movement;
		movement.y = 0;
		movement.x = Input.GetAxis("Horizontal") * speed * Time.smoothDeltaTime;
		movement.z = Input.GetAxis("Vertical") * speed * Time.smoothDeltaTime;
		this.transform.Translate (movement);
	}
	
	private void updateAimDirection() {
		aimDirection.y = 0;
		aimDirection.x = Input.GetAxis("Horizontal");
		aimDirection.z = Input.GetAxis("Vertical");
	}
}
