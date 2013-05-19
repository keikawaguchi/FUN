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
		
	}
	
	public void triggerZeroFriction(GameObject champion) {
		AlterSpeed alterSpeed;
		alterSpeed = champion.AddComponent<AlterSpeed>();
		alterSpeed.setSpeedMultiplier(SPEED_MULTIPLIER);
		alterSpeed.setDurationInSeconds(DURATION);
	}
}