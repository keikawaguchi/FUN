using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	
	public enum MovementState {
		CanMove,
		CannotMove
	}
	
	public float speed = 55f;
	private float speedMultiplier = 1.0f;
	private Vector3 aimDirection;
	private Vector3 movement;
	private MovementState currentMovementState;
	private Animation animation;

	private GridCollision gridCollision;
	private XInputController controller;

	// effects from abilities
	private float stunInterval;
	private bool isSilenced;
	
	public void Start() {
		currentMovementState = MovementState.CanMove;
		loadScripts();
	}
	
	public void Update() {
		if (currentMovementState == MovementState.CannotMove) {
			return;
		}
		translateInputToMovement();
		stopMovementOnCollision();
		applyMovement();
		updateAimDirection();
	}

	#region Public Methods
	public void setAnimationScript(Animation animation) {
		this.animation = animation;
	}
	
	public void setMovementState(MovementState newState) {
		currentMovementState = newState;
	}
	public MovementState getMovementState() {
		return currentMovementState;
	}
	
	public Vector3 getMoveDirection() {
		return movement;
	}
	
	public Vector3 getAimDirection() {
		return aimDirection;
	}
	
	public void setSpeedMultiplier(float multiplier) {
		speedMultiplier = multiplier;
	}
	
	public float getSpeedMultiplier() {
		return speedMultiplier;
	}
	
	public bool isStunned() {
		return speedMultiplier == 0;
	}
	
	public bool getIsSilence() {
		return isSilenced;
	}
	
	public void setSilenced(bool isSilenced) {
		this.isSilenced = isSilenced;
	}
	#endregion
	
	public void OnTriggerEnter(Collider collision) {
		if(collision.name == "SpeedUpgrade" && speed < 200) {
			speed += 15;
		}
	}
	
	private void loadScripts() {
		gridCollision = GetComponent<GridCollision>();
		controller = GetComponent<XInputController>();
	}
	
	private void translateInputToMovement() {
		movement.y = 0;
		movement.x = controller.GetThumbstick("left").x * calculateSpeed();
		movement.z = controller.GetThumbstick("left").y * calculateSpeed();
	}
	
	private void applyMovement() {
		transform.Translate(movement);
	}
	
	private void updateAimDirection() {
		aimDirection.y = 0;
		if (controller.GetThumbstick("left").x == 0 && controller.GetThumbstick("left").y == 0) {
			return;
		}
		aimDirection.x = controller.GetThumbstick("left").x;
		aimDirection.z = controller.GetThumbstick("left").y;
	}
	
	private void updateAnimationDirection() {
		if (controller.GetThumbstick("left").x == 0 && controller.GetThumbstick("left").y == 0) {
			return;
		}
		if (aimDirection.x == 0) {
			return;
		}
		if (aimDirection.x > 0) {
			animation.setMirrored (false);
		}
		else {
			animation.setMirrored (true);
		}
	}
	
	private float calculateSpeed() {
		return speed * speedMultiplier * Time.smoothDeltaTime;
	}
	
	private void stopMovementOnCollision() {
		if (movement.x == 0 && movement.z == 0) {
			return;	// for performance
		}
			
		if (gridCollision.isCollidingX(movement.x)) {
			movement.x = 0;
		}

		if (gridCollision.isCollidingY(movement.z)) {
			movement.z = 0;
		}
	}
	
}
