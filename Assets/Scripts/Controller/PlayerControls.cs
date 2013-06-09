using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
	
	public int player1;
	public int player2;
	public int player3;
	public int player4;
	
	public string player1SelectedChamps;
	public string player2SelectedChamps;
	public string player3SelectedChamps;
	public string player4SelectedChamps;
	
	public int player1TEAM;
	public int player2TEAM;
	public int player3TEAM;
	public int player4TEAM;
	
	public int mapNum;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevel == 1)
			Destroy(this);
	}
}
