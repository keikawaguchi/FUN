using UnityEngine;
using System.Collections;

public class EnemyTemp : MonoBehaviour {
	private float maxX;
	private float minX;
	private GlobalBehavior gb;
	private bool movingLeft;
	
	// Use this for initialization
	void Start () {
		GameObject gbObj = GameObject.Find("Global Behavior");
		gb = gbObj.GetComponent<GlobalBehavior>();
		maxX = gb.getXCoord (5);
		minX = gb.getXCoord (-5);
		movingLeft = true;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		if (pos.x < maxX && !movingLeft)
			pos.x++;
		if (transform.position.x > minX && movingLeft)
			pos.x--;
		if (pos.x >= maxX) {
			pos.x--;
			movingLeft = true;
		}
		if (pos.x <= minX) {
			pos.x++;
			movingLeft = false;
		}
		
		transform.position = pos;
	}
}
