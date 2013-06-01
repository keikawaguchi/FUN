using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	
	public enum MovementState {
		CanMove,
		CannotMove
	}
	
	public float speed = 55f;
	private float speedMultiplier = 1.0f;
	private float playerHeight;
	private float playerWidth;
	private Vector3 aimDirection;
	private Vector3 movement;
	private MovementState currentMovementState;
	private Animation animation;
	
	private Map map;
	private GridSystem gridSystem;
	private XInputController controller;

	// effects from abilities
	private float stunInterval;
	private bool isSilenced;
	
	public void Start() {
		currentMovementState = MovementState.CanMove;
		playerWidth = transform.localScale.x / 2f;
		playerHeight = transform.localScale.z / 2f;
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
		map = GameObject.Find("Map").GetComponent<Map>();
		gridSystem = GameObject.Find("Map").GetComponent<GridSystem>();
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
		if (controller.GetThumbstick("left").x == 0 && controller.GetThumbstick("right").y == 0) {
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
			
		if (isCollidingX()) {
			movement.x = 0;
		}

		if (isCollidingY()) {
			movement.z = 0;
		}
	}
	
	private bool isCollidingX() {
		if (movement.x == 0) {
			return false;
		}

		Vector2 gridLocation = new Vector2(0, 0);
			
		if (movement.x > 0) {			
			gridLocation.x = transform.position.x + movement.x + playerWidth;	
		}
		if (movement.x < 0) {
			gridLocation.x = transform.position.x + movement.x - playerWidth;
		}

		if (map.isGridFull(gridLocation.x, transform.position.z + (playerHeight - 0.1f))
		|| map.isGridFull(gridLocation.x, transform.position.z - (playerHeight - 0.1f))
		|| map.isGridFull(gridLocation.x, transform.position.z))
			return true;
		
		return false;
	}
	
	private bool isCollidingY() {
		if (movement.z == 0) {
			return false;
		}

		Vector2 gridLocation = new Vector2(0, 0);
		
		if (movement.z > 0) {
			gridLocation.y = transform.position.z + movement.z + playerHeight;
		}
		if (movement.z < 0) {
			gridLocation.y = transform.position.z + movement.z - playerHeight;
		}
		
		if (map.isGridFull(transform.position.x + (playerWidth - 0.1f), gridLocation.y)
		|| map.isGridFull(transform.position.x - (playerWidth - 0.1f), gridLocation.y)
		|| map.isGridFull(transform.position.x, gridLocation.y))
			return true;
		
		return false;
	}
	
}
