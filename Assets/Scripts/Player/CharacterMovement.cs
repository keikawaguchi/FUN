using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	
	public enum MovementState {
		CanMove,
		CannotMove,
		Stunned
	}
	
	private const float MAX_STUN_TIME = 4f;
	
	public float speed = 50f;
	private float speedMultiplier = 1.0f;
	private Vector3 aimDirection;
	private Vector3 movement;
	private MovementState currentMovementState;
	
	private Map map;
	private GridSystem gridSystem;
	private Controller controller;
	
	private float startTime;
	private float restSeconds;
	private int roundedRestSeconds;
	private int displaySeconds;
	private int displayMinutes;
	private float countDownSeconds;
	
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
		case MovementState.Stunned:
			stunInterval += Time.smoothDeltaTime;
			if (stunInterval >= 2f)
				currentMovementState = MovementState.CanMove;
			break;
		}
	}

	#region Public Methods
	public void setMovementState(MovementState newState) {
		if (newState == MovementState.Stunned) {
			GameObject t = GameObject.Instantiate(Resources.Load("Prefabs/Text/PopupText") as GameObject) as GameObject;
			PopupText popupText = t.GetComponent<PopupText>();
			popupText.initialize();
			popupText.setPredefinedText("Stun");
			popupText.setPosition (transform.position.x, transform.position.z + 10);
		}
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
	void Awake() {
		startTime = 300f;
	}
	void OnGUI()
	{
		GUIStyle timeStyle = new GUIStyle();
		timeStyle.fontSize = 30;
		timeStyle.normal.textColor = Color.white;
		float guiTime = Time.time - startTime;
		restSeconds = countDownSeconds - (guiTime);
		roundedRestSeconds = Mathf.CeilToInt(restSeconds);
		displaySeconds = roundedRestSeconds % 60;
		displayMinutes = roundedRestSeconds / 60;
		string text = string.Format("{0:00}:{1:00}",displayMinutes,displaySeconds);		
		if(restSeconds <= 10)
		{
			timeStyle.normal.textColor = Color.red;
		}
		GUI.Label(new Rect(390,10,100,20),text,timeStyle);
		
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
