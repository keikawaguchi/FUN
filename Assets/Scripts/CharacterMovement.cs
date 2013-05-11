using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	
	public float speed;

	void Start () {
	
	}
	
	void Update () {
		updateMovement();
	}
	
	private void updateMovement() {
		Vector3 movement;
		movement.y = 0;
		movement.x = Input.GetAxis("Horizontal") * speed * Time.smoothDeltaTime;
		movement.z = Input.GetAxis("Vertical") * speed * Time.smoothDeltaTime;
		this.transform.Translate (movement);
	}
	
	private void updateRotation() {
		//transform.Rotate(0, rotation, 0);
	}
}
