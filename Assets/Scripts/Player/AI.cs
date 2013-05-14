using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {
	
	public enum MovementState {
		CanMove,
		CannotMove
	}
	
	private const float MAX_STUN_TIME = 4f;
	
	public float speed = 100f;
	public float directionChangetInterval = 2f;
	private float lastDirectionChangeTime;
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
		lastDirectionChangeTime = -999f;
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
		if (Time.time - lastDirectionChangeTime < directionChangetInterval){
			return;
		}
		
		lastDirectionChangeTime = Time.time;
		float randomMovement = Random.Range(-1f, 1f);
		movement.y = 0;
		movement.x = randomMovement * calculateSpeed();
		movement.z = randomMovement * calculateSpeed();
	}
	
	private void applyMovement() {
		transform.Translate(movement);
	}
	
	private void updateAimDirection() {
		float randomAim = Random.Range(-1f, 1f);
		aimDirection.y = 0;
		aimDirection.x = randomAim;
		aimDirection.z = randomAim;
	}
	
	private float calculateSpeed() {
		float scaleSpeed = speedModifierPercentage / 100f;
		return speed * scaleSpeed * Time.smoothDeltaTime;
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
