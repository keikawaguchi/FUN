using UnityEngine;
using System.Collections;

public class AlterSpeed : MonoBehaviour {
	
	CharacterMovement characterMovement;
	
	public bool isActive = false;
	private float duration;
	private float speedMultiplier;
	private float startTime;
	
	void Start () {
		loadScripts();
		startTime = Time.time;
	}
	
	void Update () {
		Debug.Log("AlterSpeed attached to: " + gameObject.name);
		if (!isActive) {
			return;
		}
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
	
	public void activate() {
		isActive = true;
		setSpeed();
	}
	
	private void loadScripts() {
		characterMovement = GetComponent<CharacterMovement>();
		if (characterMovement == null) {
			Debug.LogError ("AlterSpeed: No hero to attach!");
		}
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
