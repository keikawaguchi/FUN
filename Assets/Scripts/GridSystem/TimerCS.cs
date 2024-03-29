/**
 * TIMER (C SHARP)
 * Copyright (c) gameDev7
 *
 * This is an HH:MM:SS count down/up timer
 * Includes functions for pausing timer
 *
 * TIP: Search where to place your functions
 * 1. Press Cntrl/Cmd + F
 * 2a. Type UP for count up timer (CAPS)
 * 2b. Type DOWN for count down timer (CAPS)
 * 3. Check Match whole word only
 * 4. Check Match case
 * 5. Click Find Next
 */

using UnityEngine;
using System.Collections;

public class TimerCS : MonoBehaviour {
	/// INPUT VARIBLES
	public GUIStyle timerStyle;	
		
	private float timer = 0f;
	private float sec = 0f;
	private float min = 0f;
	private float hrs = 0f;	
	
	/// DISPLAY VARIABLES
	private string strHours = "00";
	private string strMinutes = "00";
	private string strSeconds = "00";
	
	private string strHrs = "00";
	private string strMin = "00";
	private string strSec = "00";
	
	float originalWidth = 800;
	float originalHeight = 600;
	Vector3 scale;
	
	public int mapID;
	// Use this for initialization
	void Start () {		
		timerStyle.fontSize = 45;
		timerStyle.normal.textColor = Color.white;
		timerStyle.font = Resources.Load("Fonts/Ministry") as Font;
	}	
	// Update is called once per frame
    void Update()
    {
		mapID = GameObject.Find("Map").GetComponent<Map>().mapID;
		if(mapID == 4)
			timerStyle.normal.textColor = Color.black;
		else
			timerStyle.normal.textColor = Color.white;
		CountUp();
    }
	
	void CountUp() {
		timer += Time.deltaTime;
		
		if(timer >= 1f) {
			sec++;
			timer = 0f;
		}
		
		if(sec >= 60) {
			min++;
			sec = 0f;
		}
		
		if(min >= 60) {
			hrs++;
			min = 0f;
		}
	}
	
	void FormatTimer () {
		if(sec < 10) {
			strSec = "0" + sec.ToString();
		} else {
			strSec = sec.ToString();
		}//end if
		
		if(min < 10) {
			strMin = "0" + min.ToString();
		} else {
			strMin = min.ToString();
		}
	}
	
	void OnGUI () {
		FormatTimer();	
		scale.x = Screen.width/originalWidth; // calculate hor scale
    	scale.y = Screen.height/originalHeight; // calculate vert scale
    	scale.z = 1;
    	var svMat = GUI.matrix; // save current matrix
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		GUI.Label(new Rect(370,59,100,20),string.Format("{0:00}:{1:00}",min, sec), timerStyle);
		GUI.matrix = svMat;
	}
	
	public bool timeForUpgrade()
	{
		if(sec == 0 || sec == 00 || sec == 30)
			return true;
		else
			return false;
	}
}
