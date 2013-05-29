using UnityEngine;
using System.Collections;

public class TextControl : MonoBehaviour {
	public bool isQuitButton = false;
	public bool isNextButton = false;
	public bool isBackButton = false;
	
	int player1;
	int player2;
	int player3;
	int player4;
	
	int player1TEAM;
	int player2TEAM;
	int player3TEAM;
	int player4TEAM;
	
	PlayerSelectionMenu listOfChamps;
	PlayerControls controlSave; 
	float originalWidth = 800;
	float originalHeight = 600;
	Vector3 scale;
	
	float timeSinceStart = -99;
	float maxTime = 2f;
	
	void OnMouseEnter()
	{
		renderer.material.color = Color.green;
	}
	
	void OnMouseExit()
	{
		renderer.material.color = Color.white;
	}
	
	void OnMouseUp()
	{
		if(isQuitButton == true)
		{
			Application.Quit();
		}
		else if(isNextButton == true)
		{
			listOfChamps = GameObject.Find("Manager").GetComponent<PlayerSelectionMenu>();
			controlSave = GameObject.Find("Controls").GetComponent<PlayerControls>();
			player1 = listOfChamps.Player1.SelectedItemIndex;
			player2 = listOfChamps.Player2.SelectedItemIndex;
			player3 = listOfChamps.Player3.SelectedItemIndex;
			player4 = listOfChamps.Player4.SelectedItemIndex;
			
			player1TEAM = listOfChamps.player1Team.SelectedItemIndex;
			player2TEAM = listOfChamps.player2Team.SelectedItemIndex;
			player3TEAM = listOfChamps.player3Team.SelectedItemIndex;
			player4TEAM = listOfChamps.player4Team.SelectedItemIndex;
			
			if(player1 == player2 || player1 == player3 || player1 == player4 || player2 == player3 || player2 == player4 || player3 == player4)
			{
				timeSinceStart = Time.time;
			}
			else
			{
				controlSave.player1 = player1;
				controlSave.player2 = player2;
				controlSave.player3 = player3;
				controlSave.player4 = player4;
				
				controlSave.player1TEAM = player1TEAM;
				controlSave.player2TEAM = player2TEAM;
				controlSave.player3TEAM = player3TEAM;
				controlSave.player4TEAM = player4TEAM;
				Application.LoadLevel(2);
			}
		}
		else if (isBackButton) {
			Application.LoadLevel(1);
		}
	}
	
	void OnGUI()
	{
		scale.x = Screen.width/originalWidth; // calculate hor scale
    	scale.y = Screen.height/originalHeight; // calculate vert scale
    	scale.z = 1;
    	var svMat = GUI.matrix; // save current matrix
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		if(Time.time - timeSinceStart < maxTime)
			GUI.Label(new Rect(300,250,200,200),"No Duplicate Champions Allowed!");
		else
			GUI.Label(new Rect(300,250,200,100),"");
		GUI.matrix = svMat;
	}
}
