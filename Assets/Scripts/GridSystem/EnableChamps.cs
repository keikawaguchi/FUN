using UnityEngine;
using System.Collections;

public class EnableChamps : MonoBehaviour {
	PlayerControls controlNums;
	// Use this for initialization
	void Awake () {
	
		controlNums = GameObject.Find("Controls").GetComponent<PlayerControls>();
		
		if(controlNums.player1 != 0 && controlNums.player2 != 0 && controlNums.player3 != 0 && controlNums.player4 != 0)
		{
			Destroy(GameObject.Find ("Albion"));
		}
		if(controlNums.player1 != 1 && controlNums.player2 != 1 && controlNums.player3 != 1 && controlNums.player4 != 1)
		{
			Destroy(GameObject.Find ("Fanndis"));
		}
		if(controlNums.player1 != 2 && controlNums.player2 != 2 && controlNums.player3 != 2 && controlNums.player4 != 2)
		{
			Destroy(GameObject.Find ("Kirito"));
		}
		if(controlNums.player1 != 3 && controlNums.player2 != 3 && controlNums.player3 != 3 && controlNums.player4 != 3)
		{
			Destroy(GameObject.Find ("Merlini"));
		}
		if(controlNums.player1 != 4 && controlNums.player2 != 4 && controlNums.player3 != 4 && controlNums.player4 != 4)
		{
			Destroy(GameObject.Find ("Temptress"));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
