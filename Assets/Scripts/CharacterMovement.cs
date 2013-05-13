using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	
	public enum MovementState {
		CanMove,
		CannotMove
	}
	
	public float speed = 100f;
	private float speedModifier = 1f;	// 1 = 100% speed
	private Vector3 aimDirection;		// direction for aiming skills
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
	
	public void increaseSpeedByPercentage(float percentage) {
		if (percentage >= 10) {
			percentage /= 10f;
		}
		speedModifier += percentage;
	}
	
	public void decreaseSpeedByPercentage(float percentage) {
		if (percentage >= 10) {
			percentage /= 10f;
		}
		speedModifier -= percentage;
	}
	#endregion
	
	private void updateMovement() {
		Vector3 movement;
		movement.y = 0;
		movement.x = Input.GetAxis("Horizontal") * calculateSpeed();
		movement.z = Input.GetAxis("Vertical") * calculateSpeed();
		this.transform.Translate(movement);
	}
	
	private float calculateSpeed() {
		return speed * speedModifier * Time.smoothDeltaTime;
	}
	
	private void updateAimDirection() {
		aimDirection.y = 0;
		aimDirection.x = Input.GetAxis("Horizontal");
		aimDirection.z = Input.GetAxis("Vertical");
	}
}
