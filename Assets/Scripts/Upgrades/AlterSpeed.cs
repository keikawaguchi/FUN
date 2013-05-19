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
	public void Start(float speedMultiplier, float duration) {
		this.speedMultiplier = speedMultiplier;
		this.duration = duration;
		showPopupText();
	}
	
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
	
	private void showPopupText() {
		GameObject t = Resources.Load ("Prefabs/Text/PopupText") as GameObject;
		GameObject text = Instantiate(t) as GameObject;	
		
		PopupText popupText = text.GetComponent<PopupText>();
		popupText.initialize();
		popupText.setDuration(duration);
		popupText.setPosition(transform.position.x, transform.position.z + 7);
		
		if (speedMultiplier == 0) {
			popupText.setPredefinedText("Stun");
		}
		else if (speedMultiplier < 1) {
			popupText.setPredefinedText("MinusSpeed");
		}
		else if (speedMultiplier > 1) {
			popupText.setPredefinedText("PlusSpeed");
		}
		else if (speedMultiplier < 0) {
			popupText.setPredefinedText("Confused");
		}
	}
}
