using UnityEngine;
using System.Collections;

public class Animation : MonoBehaviour {
	
	public GameObject objectToFollow;
	private string alignment;
	private Vector3 customAnimationOffset;
	
	
	void Start() {
		if (objectToFollow == null) {
			return;
		}
		transform.forward = objectToFollow.transform.forward;
		customAnimationOffset = new Vector3(0, 0, 0);
		if (alignment == null) {
			alignment = "bottom";
		}
	}

	void Update () {
		if (objectToFollow == null) {
			destroyAnimation();
		}
		else {
			updatePosition();
			updateAnimationDirection();
		}
	}
	
	public void attachToObject(GameObject obj) {
		objectToFollow = obj;
	}
	
	public bool setAlignment(string alignment) {
		alignment = alignment.ToLower();
		if (alignment == "center" ||
			alignment == "top" ||
			alignment == "bottom" ||
			alignment == "left" ||
			alignment == "right") {
			this.alignment = alignment;
			return true;
		}

		return false;
	}
	
	public void setVisibility(bool isVisible) {
		renderer.enabled = isVisible;
	}
	
	public void setMirrored(bool isMirrored) {
		Vector3 newScale;
		
		if (isMirrored) {
			if (transform.localScale.x < 0) {
				return;
			}
			newScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
		}
		else {
			if (transform.localScale.x > 0) {
				return;
			}
			newScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
		}
		
		transform.localScale = newScale;
	}
	
	public void destroyAnimation() {
		Destroy(gameObject);
	}
	
	private void updatePosition() {
		float xOffset = 0f;
		float yOffset = 20f;
		float zOffset = 0f;
		float halfAnimationHeight = transform.localScale.z / 2.0f;
		float halfFollowingHeight = objectToFollow.transform.localScale.z / 2.0f;
		
		if (alignment == "top") {
			zOffset = halfAnimationHeight - halfFollowingHeight;
		}
		if (alignment == "bottom") {
			zOffset = halfAnimationHeight - halfFollowingHeight;
		}
		
		transform.position = objectToFollow.transform.position;
		transform.position += new Vector3(xOffset, yOffset, zOffset) + customAnimationOffset;	
	}
	
	private void updateAnimationDirection() {
		Vector3 aimDirection;
		CharacterMovement characterMovement;
		
		characterMovement = objectToFollow.GetComponent<CharacterMovement>();
		if (characterMovement == null) {
			return;
		}
		
		aimDirection = characterMovement.getAimDirection();
		if (aimDirection.x == 0) {
			return;
		}
		if (aimDirection.x > 0) {
			setMirrored(false);
		}
		else {
			setMirrored(true);
		}
	}
}
