using UnityEngine;
using System.Collections;

public class ButtonBehavior : MonoBehaviour {
	private const int MAX_PLAYERS = 4;
	
	public bool isQuitButton = false;
	public bool isNextButton = false;
	public bool isBackButton = false;
	
	private string[] playersSelectedChamps;

	// Use this for initialization
	void Start () {
		playersSelectedChamps = new string[MAX_PLAYERS];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
