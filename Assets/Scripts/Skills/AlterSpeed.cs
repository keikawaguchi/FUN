using UnityEngine;
using System.Collections;

public class AlterSpeed : MonoBehaviour {
	
	private float duration;
	private float speedMultiplier;
	private float startTime;
	
	
	void Start () {
		startTime = Time.time;
	}
	
	void Update () {
		if (isComplete ()) {
			Destroy(this);
			return;
		}
	}
	
	public void setDurationInSeconds(float seconds) {
		duration = seconds;
	}
	
	public void setSpeedMultiplier(float multiplier) {
		speedMultiplier = multiplier;
	}
	
	private bool isComplete() {
		return (Time.time - startTime) > duration;
	}
}
