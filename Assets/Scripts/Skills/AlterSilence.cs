using UnityEngine;
using System.Collections;

public class AlterSilence : MonoBehaviour {
	CharacterMovement characterMovement;
	
	private float startTime;
	private float duration;
//	private bool isSilenced;
	
	// Use this for initialization
	void Start () {
		startTime = Time.time;
//		setSilenced (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (isComplete ()) {
			setSilenced (false);
			Destroy(this);
			return;
		}
	}
	
	#region Public Methods
	public void Start(bool isSilenced, float duration) {
		loadScripts();
		setSilenced (isSilenced);
		this.duration = duration;
//		showPopupText();
	}
	
	public void setDurationInSeconds(float seconds) {
		duration = seconds;
	}
	#endregion
	
	private void loadScripts() {
		characterMovement = GetComponent<CharacterMovement>();
	}
	
	private void setSilenced(bool isSilenced) {
		if (isSilenced) {  // true
			Debug.Log ("AlterSilence set silenced");
			characterMovement.setSilenced (isSilenced);
		}
		else {  // false
			Debug.Log ("AlterSilence unset silenced");
			characterMovement.setSilenced (isSilenced);
		}
	}
	
	private bool isComplete() {
		return (Time.time - startTime) > duration;
	}
}
