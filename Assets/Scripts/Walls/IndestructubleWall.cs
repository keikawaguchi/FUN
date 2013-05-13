using UnityEngine;
using System.Collections;

public class IndestructubleWall : MonoBehaviour {

	void Start () {

	}
	
	void Update () {
	
	}
	
	public void initialize(float x, float z) {
		transform.position = new Vector3(x, 0, z);
	}

}
