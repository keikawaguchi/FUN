using UnityEngine;
using System.Collections;

public class Animation : MonoBehaviour {
	
	private bool isFollowingObject = false;
	private GameObject objectToFollow;
	private string alignment;
	private Vector3 customAnimationOffset;
	
	private Texture runningTexture;
	private Texture idleTexture;
	
	CharacterMovement characterMovement;
	
	
	void Start() {
		if (customAnimationOffset == null) {
			customAnimationOffset = new Vector3(0, 0, 0);
		}
		if (alignment == null) {
			alignment = "bottom";
		}
		if (objectToFollow != null) {
			transform.forward = objectToFollow.transform.forward;
		}
	}

	void Update () {
		if (isFollowingObject) {
			if (objectToFollow == null) {
				destroyAnimation();
				return;
			}
		}
		if (objectToFollow != null) {
			updatePosition();
			updateAnimationDirection();
		}
		updateTexture();		
	}
	
	public void attachToObject(GameObject obj) {
		isFollowingObject = true;
		objectToFollow = obj;
		characterMovement = objectToFollow.GetComponent<CharacterMovement>();
	}
	
	public void setPosition(Vector3 position) {
		transform.position = position + customAnimationOffset;
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
	
	public void setIdleTexture(Texture texture) {
		// Debug.Log ("Set Idle Texture");
		idleTexture = texture;
	}
	
	public void setRunningTexture(Texture texture) {
		// Debug.Log ("Set Running Texture");
		runningTexture = texture;
	}

	public void startIdleAnimation() {
		if (idleTexture == null) {
			return;
		}
		renderer.material.mainTexture = idleTexture;
	}
	
	public void startRunningAnimation() {
		if (runningTexture == null) {
			return;
		}
		renderer.material.mainTexture = runningTexture;
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
	
	public void setCustomOffset(Vector3 offset) {
		customAnimationOffset = offset;
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
			zOffset = halfAnimationHeight + halfFollowingHeight;
		}
		if (alignment == "bottom") {
			zOffset = halfAnimationHeight - halfFollowingHeight;
		}
		
		transform.position = objectToFollow.transform.position;
		transform.position += new Vector3(xOffset, yOffset, zOffset);
		transform.position += customAnimationOffset;	
	}
	
	private void updateAnimationDirection() {
		if (characterMovement == null) {
			return;
		}
		
		Vector3 aimDirection = characterMovement.getAimDirection();
		
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
	
	private void updateTexture() {
		if (characterMovement == null) {
			return;
		}
		
		Vector3 moveDirection = characterMovement.getMoveDirection();
		
		if (moveDirection.x == 0 && moveDirection.z == 0) {
			if (renderer.material.mainTexture != idleTexture) {
				startIdleAnimation();
			}
			return;
		}
		
		else {
			if (renderer.material.mainTexture != runningTexture) {
				startRunningAnimation();
			}
		}
	}
}
