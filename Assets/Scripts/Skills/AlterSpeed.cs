using UnityEngine;
using System.Collections;

public class AlterSpeed : MonoBehaviour {
	
	CharacterMovement characterMovement;
	
	private float duration;
	private float speedMultiplier;
	private float startTime;
	
	void Start () {
		loadScripts();
		startTime = Time.time;
		setSpeed();
	}
	
	void Update () {
		if (isComplete ()) {
			unsetSpeed();
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
	
	private void loadScripts() {
		characterMovement = GetComponent<CharacterMovement>();
	}
	
	private void setSpeed() {
		float currentSpeedMultiplier = characterMovement.getSpeedMultiplier();
		characterMovement.setSpeedMultiplier(currentSpeedMultiplier + speedMultiplier);
	}
	
	private void unsetSpeed() {
		float currentSpeedMultiplier = characterMovement.getSpeedMultiplier();
		characterMovement.setSpeedMultiplier(currentSpeedMultiplier - speedMultiplier);
	}
	
	private bool isComplete() {
		return (Time.time - startTime) > duration;
	}
}
