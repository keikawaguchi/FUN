using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	
	public enum MovementState {
		CanMove,
		CannotMove
	}
	
	public float speed = 50f;
	private float speedMultiplier = 1.0f;
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
		if (controller.GetThumbstick("right").x == 0 && controller.GetThumbstick("right").y == 0) {
			return;
		}
		aimDirection.x = controller.GetThumbstick("right").x;
		aimDirection.z = controller.GetThumbstick("right").y;
	}
	
	private void updateAnimationDirection() {
		if (controller.GetThumbstick("right").x == 0 && controller.GetThumbstick("right").y == 0) {
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
		float playerWidth = transform.localScale.x / 2f;
		float playerHeight = transform.localScale.z / 2f;
			
			// traveling right
			if (movement.x > 0) {
			
				if(map.isGridFull(transform.position.x + movement.x + playerWidth, transform.position.z + (playerHeight - 0.1f)))
					movement.x = 0;
			
				if(map.isGridFull(transform.position.x + movement.x + playerWidth, transform.position.z - (playerHeight - 0.1f)))
					movement.x = 0;
				
				if (map.isGridFull(transform.position.x + movement.x + playerWidth, transform.position.z))
 					movement.x = 0;
				
			// traveling left
			} else if (movement.x < 0) {
				
				if(map.isGridFull(transform.position.x + movement.x - playerWidth, transform.position.z + (playerHeight - 0.1f)))
					movement.x = 0;
			
				if(map.isGridFull(transform.position.x + movement.x - playerWidth, transform.position.z - (playerHeight - 0.1f)))
					movement.x = 0;
				
				if (map.isGridFull(transform.position.x + movement.x - playerWidth, transform.position.z))
 					movement.x = 0;
			}

			
			// traveling up
			if (movement.z > 0) {
				
				if (map.isGridFull(transform.position.x + (playerWidth - 0.1f), transform.position.z + movement.z + playerHeight))
					movement.z = 0;
				
				if (map.isGridFull(transform.position.x - (playerWidth - 0.1f), transform.position.z + movement.z + playerHeight))
					movement.z = 0;

				if (map.isGridFull(transform.position.x, transform.position.z + movement.z + playerHeight))
 					movement.z = 0;
				
			// traveling down
			} else if (movement.z < 0) {
				
				if (map.isGridFull(transform.position.x + (playerWidth - 0.1f), transform.position.z + movement.z - playerHeight))
					movement.z = 0;
				
				if (map.isGridFull(transform.position.x - (playerWidth - 0.1f), transform.position.z + movement.z - playerHeight))
					movement.z = 0;
				
				if (map.isGridFull(transform.position.x, transform.position.z + movement.z - playerHeight))
 					movement.z = 0;
			}
			
		
	}
	
}
