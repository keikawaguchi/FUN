using UnityEngine;
using System.Collections;

// source: http://forum.unity3d.com/threads/36503-Grappling-Hook

public class Grapple : MonoBehaviour {
	private float lineWidth = 1f;  // line width
	private LineRenderer line;  // get line going
	GameObject hero;
	
	// Use this for initialization
	void Start () {
		line = gameObject.AddComponent<LineRenderer>();
		line.SetWidth (lineWidth, lineWidth);
    	line.SetVertexCount(2);
    	line.material.color = Color.black;
		line.renderer.enabled = true;  // make line visible
		
		hero = GameObject.Find("Hero");
	}
	
	// Update is called once per frame
	void Update () {
		
		DrawGrapple ();
		
		// if the hook is more than 10 units, it's destroyed
//		if (Vector3.Distance(transform.position, hero.transform.position) > 10)
//			Destroy(gameObject);
	}
	
	private void DrawGrapple() {
		Vector3 grappleStartPos = hero.transform.position;
		grappleStartPos.z = hero.transform.position.z + 50f;
		
		// set starting point of line to this object, in this case grappling hook prefab
		line.SetPosition (0, grappleStartPos);  // shoot straight up along with z-axis
		Debug.Log ("Grapple Pos: " + grappleStartPos);
		// set the ending point of the line to the shooterobject
		line.SetPosition (1, hero.transform.position);
		Debug.Log ("Hero Pos: " + hero.transform.position);
	}
	
	private void OnCollisionEnter(Collision collision) {
		// when the spawned grappling hook makes contact with a rigidbody, in this case 
		// the Ceiling ojbect, we turn off the grappling hook's gravity,and other 
		// kinematic forces, thus "freezing" it in space making it look like it sticks
    	rigidbody.isKinematic = true;
		Destroy (gameObject);
	}
}
