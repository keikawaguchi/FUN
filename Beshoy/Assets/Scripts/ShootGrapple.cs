using UnityEngine;
using System.Collections;

// source: http://forum.unity3d.com/threads/36503-Grappling-Hook

public class ShootGrapple : MonoBehaviour {
	private GameObject grapple;
	private Vector3 shootDirection;
	private float shootForceX;
	private float shootForceY;
	private float shootForceZ;
	private bool grappleExist;  // only one grapple allowed at a time
	
	// Use this for initialization
	void Start () {
		grapple = Resources.Load ("Prefabs/Grapple") as GameObject;
		//shootDirection = new Vector3(10f, 0f, 0f);
		shootForceX = 300f;
		shootForceY = 0f;
		shootForceZ = 300f;
		grappleExist = false;
	}
	
	// Update is called once per frame
	void Update () {
		// on space bar down
		if (Input.GetButtonDown ("Jump")) {
			if(!grappleExist)
				SpawnGrapple ();
		}
	}
	
	private void SpawnGrapple() {
		// spawn the grappling hook prefab
		GameObject instantiateGrapple = Instantiate (grapple) as GameObject;
		instantiateGrapple.transform.position = transform.position;
		// shoot the spawned grappling hook with the forces set in the variables
		instantiateGrapple.rigidbody.AddForce (shootForceX, shootForceY, shootForceZ);
		grappleExist = true;
	}
	
	public void setGrappleExistence(bool existence) {
		grappleExist = existence;
	}
}
