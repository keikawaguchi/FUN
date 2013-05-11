using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	
	public float speed;
	private Vector3 aimDirection;

	void Start () {
	
	}
	
	void Update () {
		updateMovement();
		updateRotation();
	}
	
	#region Public Methods
	public Vector3 getAimDirection() {
		return aimDirection;
	}
	#endregion
	
	private void updateMovement() {
		Vector3 movement;
		movement.y = 0;
		movement.x = Input.GetAxis("Horizontal") * speed * Time.smoothDeltaTime;
		movement.z = Input.GetAxis("Vertical") * speed * Time.smoothDeltaTime;
		this.transform.Translate (movement);
	}
	
	private void updateAimDirection() {
		aimDirection.y = 0;
		aimDirection.x = Input.GetAxis("Horizontal");
		aimDirection.z = Input.GetAxis("Vertical");
	}
}
