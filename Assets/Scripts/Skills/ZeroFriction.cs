using UnityEngine;
using System.Collections;

public class ZeroFriction : MonoBehaviour {
	private const float SPEED_MULTIPLIER = 2f;
	private const float DURATION = 5f;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		AlterSpeed alterSpeed;
		alterSpeed = gameObject.AddComponent<AlterSpeed>();
		alterSpeed.setSpeedMultiplier(SPEED_MULTIPLIER);
		alterSpeed.setDurationInSeconds(DURATION);
	}
}
