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
	
	private Map map;
	private GridSystem gridSystem;
	private Controller controller;

	// effects from abilities
	private float stunInterval;
	
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
	public void setMovementState(MovementState newState) {
		currentMovementState = newState;
	}
	public MovementState getMovementState() {
		return currentMovementState;
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
	#endregion
	
	public void OnTriggerEnter(Collider collision) {
		if(collision.name == "SpeedUpgrade" && speed < 200) {
			speed += 15;
		}
	}
	
	private void loadScripts() {
		map = GameObject.Find("Map").GetComponent<Map>();
		gridSystem = GameObject.Find("Map").GetComponent<GridSystem>();
		controller = GetComponent<Controller>();
	}
	
	private void translateInputToMovement() {
		movement.y = 0;
		movement.x = controller.getAxis("Horizontal") * calculateSpeed();
		movement.z = controller.getAxis("Vertical") * calculateSpeed();
	}
	
	private void applyMovement() {
		transform.Translate(movement);
	}
	
	private void updateAimDirection() {
		aimDirection.y = 0;
		if (controller.getAxis("Horizontal") == 0 && controller.getAxis("Vertical") == 0) {
			return;
		}
		aimDirection.x = controller.getAxis("Horizontal");
		aimDirection.z = controller.getAxis("Vertical");
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
	void OnGUI()
	{
		if(gameObject.name == "Temptress")
		{
			GUI.Box(new Rect(5,5,200,50),"Temptress");
			GUI.Label(new Rect(5,15,100,20),"Lives: " + gameObject.GetComponent<Hero>().lives.ToString());
			GUI.Label (new Rect(5,35,120,50),"Lure Cool Down: " + gameObject.GetComponent<TemptressBehavior>().getCoolDown().ToString());
		}
		else if(gameObject.name == "Albion")
		{
			GUI.Box(new Rect(653,5,200,50),"Albion");
			GUI.Label(new Rect(653,25,100,20),"Lives: " + gameObject.GetComponent<Hero>().lives.ToString());
		}
	}
}
