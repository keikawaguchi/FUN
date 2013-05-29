using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour {
	
	private GameManager manager;
	private GUIText winner;
	private string winnerName;
	float originalWidth = 800;
	float originalHeight = 600;
	Vector3 scale;
	// Use this for initialization
	void Start () {
		GameObject managerObj = GameObject.Find ("Game Manager");
		manager = managerObj.GetComponent<GameManager>();
		
		loadText ();
	}
	
	// Update is called once per frame
	void Update () {
		scale.x = Screen.width/originalWidth; // calculate hor scale
    	scale.y = Screen.height/originalHeight; // calculate vert scale
    	scale.z = 1;
    	var svMat = GUI.matrix; // save current matrix
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		winnerName = manager.getWinner ();
		winner.text = winnerName;
		GUI.matrix = svMat;
	}
	
	private void loadText() {
		GameObject winnerObj = GameObject.Find ("Winner");
		winner = winnerObj.GetComponent<GUIText>();
	}
}
