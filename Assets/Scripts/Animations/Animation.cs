using UnityEngine;
using System.Collections;

public class Animation : MonoBehaviour {
	
	public GameObject objectToFollow;
	
	void Start() {
		if (objectToFollow == null) {
			return;
		}
		transform.forward = objectToFollow.transform.forward;
	}

	void Update () {
		if (objectToFollow == null) {
			Destroy(this);
			return;
		}
		transform.position = objectToFollow.transform.position;
		transform.position = new Vector3(transform.position.x, 20, transform.position.z);
	}
	
	public void attachToObject(GameObject obj) {
		objectToFollow = obj;
	}
}
