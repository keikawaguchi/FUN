using UnityEngine;
using System.Collections;

public class Animation : MonoBehaviour {
	
	public GameObject objectToFollow;
	
	private Vector3 customAnimationOffset;
	
	
	void Start() {
		if (objectToFollow == null) {
			return;
		}
		transform.forward = objectToFollow.transform.forward;
		customAnimationOffset = new Vector3(0, 20, 0);
	}

	void Update () {
		if (objectToFollow == null) {
			destroyAnimation();
		}
		else {
			updatePosition();	
		}
	}
	
	public void attachToObject(GameObject obj) {
		objectToFollow = obj;
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
			return;
		}
		else {
			if (transform.localScale.x > 0) {
				return;
			}
			newScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
		}
	}
	
	public void destroyAnimation() {
		Destroy(gameObject);
	}
	
	private void updatePosition() {
		float halfAnimationHeight = transform.localScale.z / 2.0f;
		float halfFollowingHeight = objectToFollow.transform.localScale.z / 2.0f;
		
		// Position so that animation bottom lines up with objects bottom
		float zOffset = halfAnimationHeight - halfFollowingHeight;
		
		// Apply position offsets
		transform.position = objectToFollow.transform.position;
		transform.position += new Vector3(0, 0, zOffset) + customAnimationOffset;	
	}
}
