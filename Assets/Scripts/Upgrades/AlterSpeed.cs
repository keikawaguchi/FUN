using UnityEngine;
using System.Collections;

public class AlterSpeed : MonoBehaviour {
	
	CharacterMovement characterMovement;
	
	private float duration = 1.0f;
	private float speedMultiplier = 1.0f;
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
	
	#region Public Methods
	public void setDurationInSeconds(float seconds) {
		duration = seconds;
	}
	
	public void setSpeedMultiplier(float multiplier) {
		speedMultiplier = multiplier;
	}
	#endregion
	
	private void loadScripts() {
		characterMovement = GetComponent<CharacterMovement>();
	}
	
	private void setSpeed() {
		Debug.Log("AlterSpeed set speed!");
		characterMovement.setSpeedMultiplier(speedMultiplier);
	}
	
	private void unsetSpeed() {
		Debug.Log("AlterSpeed unset speed!");
		characterMovement.setSpeedMultiplier(1.0f);
	}
	
	private bool isComplete() {
		return (Time.time - startTime) > duration;
	}
}
