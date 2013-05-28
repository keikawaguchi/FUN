using UnityEngine;
using System.Collections;

public class CooldownViewer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void updateCDViewerPosition(Vector3 heroPosition, bool isLeftViewer) {
		Vector3 viewerPos = heroPosition;
		
		if (isLeftViewer) {
			viewerPos.z -= 4;
			viewerPos.x -= 2;
			viewerPos.y += 25;
		} else {	
			viewerPos.z -= 4;
			viewerPos.x += 2;
			viewerPos.y += 25;
		}
		
		transform.position = viewerPos;	
	}
	
	public void updateCDViewerColor(bool isOnCD) {
		if (isOnCD) 
			renderer.material.color = Color.red;
		else
			renderer.material.color = Color.green;
	}
}
