using UnityEngine;
using System.Collections;

public class IndestructubleWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void initialize(float x, float z) {
		transform.position = new Vector3(x, 0, z);
	}
}
