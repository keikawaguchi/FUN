using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire3"))
		{
			transform.position = new Vector3(-112.3f, 0, -79f);
		}
	}
}
