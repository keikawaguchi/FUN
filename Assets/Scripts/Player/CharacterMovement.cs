using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	
	public enum MovementState {
		CanMove,
		CannotMove
	}
	
	public float speed = 100f;
	private float speedModifierPercentage = 100f;
	private Vector3 aimDirection;
	private Vector3 movement;
	private MovementState currentMovementState;
	
	private Map map;
	private GridSystem gridSystem;
	
	// effects from abilities
	private float stunInterval;
	
	public void Start() {
		currentMovementState = MovementState.CanMove;
		loadScripts();
		stunInterval = 0f;
	}
	
	public void Update() {
		switch(currentMovementState) {
		case MovementState.CanMove:
			translateInputToMovement();
			stopMovementOnCollision();
			applyMovement();
			updateAimDirection();
			stunInterval = 0f;  // reset timer
			break;
		case MovementState.CannotMove:
			stunInterval += Time.smoothDeltaTime;
			if (stunInterval >= 2f)
				currentMovementState = MovementState.CanMove;
			break;
		}
	}

	#region Public Methods
	public void setMovementState(MovementState newState) {
		currentMovementState = newState;
	}
	public MovementState getMovementState() {
		return currentMovementState;
	}
	
	public Vector3 getAimDirection() {
		return aimDirection;
	}
	
	public void increaseSpeedByPercentage(float percentage) {
		speedModifierPercentage += percentage;
	}
	
	public void decreaseSpeedByPercentage(float percentage) {
		speedModifierPercentage -= percentage;
	}
	#endregion
	
	private void loadScripts() {
		map = GameObject.Find("Map").GetComponent<Map>();
		if (map == null) {
			Debug.Log("Map is null");
		}
		
		gridSystem = GameObject.Find("Map").GetComponent<GridSystem>();
		if (gridSystem == null) {
			Debug.Log("GridSystem is null");
		}
	}
	
	private void translateInputToMovement() {
		movement.y = 0;
		movement.x = Input.GetAxis("Horizontal") * calculateSpeed();
		movement.z = Input.GetAxis("Vertical") * calculateSpeed();
	}
	
	private void applyMovement() {
		transform.Translate(movement);
	}
	
	private void updateAimDirection() {
		aimDirection.y = 0;
		aimDirection.x = Input.GetAxis("Horizontal");
		aimDirection.z = Input.GetAxis("Vertical");
	}
	
	private float calculateSpeed() {
		float scaleSpeed = speedModifierPercentage / 100f;
		return speed * scaleSpeed * Time.smoothDeltaTime;
	}
	
	private void stopMovementOnCollision() {
		float playerWidth = transform.localScale.x / 2f;
		float playerHeight = transform.localScale.z / 2f;
		float blockWidth = gridSystem.getSingleGridWidth() / 2f;
		float blockHeight = gridSystem.getSingleGridHeight() / 2f;
		
		if (movement.x < 0) {
			playerWidth *= -1f;
		}
		if (movement.z < 0) {
			playerHeight *= -1f;
		}
		
		if (map.isGridFull(transform.position.x + movement.x + playerWidth, transform.position.z)) {
			movement.x = 0;
		}
		if (map.isGridFull(transform.position.x, transform.position.z + movement.z + playerHeight)) {
			movement.z = 0;
		}
	}
}
