using UnityEngine;
using System.Collections;

public class EnemyTemp : MonoBehaviour {
	public enum MovementState {
		CanMove,
		CannotMove
	}
	
	private float maxX;
	private float minX;
	private GlobalBehavior gb;
	private bool movingLeft;
	private MovementState currentState;
	private float stunInterval;
	
	// Use this for initialization
	void Start () {
		currentState = MovementState.CanMove;
		GameObject gbObj = GameObject.Find("Global Behavior");
		gb = gbObj.GetComponent<GlobalBehavior>();
		maxX = gb.getXCoord (15);
		minX = gb.getXCoord (3);
		movingLeft = true;
		stunInterval = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentState) {
			case MovementState.CanMove:
				Move ();
				break;
			case MovementState.CannotMove:
				CannotMove ();
				break;
		}
	}
	
	private void Move() {
		Vector3 pos = transform.position;
		// moving back and forth
		if (pos.x < maxX && !movingLeft)
			pos.x++;
		if (transform.position.x > minX && movingLeft)
			pos.x--;
		if (pos.x >= maxX) {
			pos.x--;
			movingLeft = true;
		}
		if (pos.x <= minX) {
			pos.x++;
			movingLeft = false;
		}
		transform.position = pos;
		stunInterval = 0f;
	}
	
	private void CannotMove() {
		stunInterval += Time.smoothDeltaTime;
		transform.position = transform.position;
		if (stunInterval >= 2f)
			currentState = MovementState.CanMove;
	}
	
	public void SetMovementState(MovementState state) {
		currentState = state;
	}
}
