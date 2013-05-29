using UnityEngine;
using System.Collections;

public class AlterSilence : MonoBehaviour {
	CharacterMovement characterMovement;
	
	private float startTime;
	private float duration;
	private GameObject silenceAnimation;
	
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (isComplete ()) {
			setSilenced (false);
			silenceAnimation.GetComponent<Animation>().destroyAnimation();
			Destroy(this);
			return;
		}
	}
	
	#region Public Methods
	public void Start(bool isSilenced, float duration) {
		loadScripts();
		setSilenced (isSilenced);
		this.duration = duration;
		displaySilenceIcon ();
	}
	
	public void setDurationInSeconds(float seconds) {
		duration = seconds;
	}
	#endregion
	
	private void loadScripts() {
		characterMovement = GetComponent<CharacterMovement>();
	}
	
	private void setSilenced(bool isSilenced) {
		if (isSilenced) {
			Debug.Log ("AlterSilence set silenced");
			characterMovement.setSilenced (isSilenced);
		}
		else { 
			Debug.Log ("AlterSilence unset silenced");
			characterMovement.setSilenced (isSilenced);
		}
	}
	
	private void displaySilenceIcon() {
		silenceAnimation = Resources.Load ("Prefabs/Icons/SilencedIcon") as GameObject;
		silenceAnimation = Instantiate (silenceAnimation) as GameObject;
		silenceAnimation.GetComponent<Animation>().attachToObject(gameObject);
		silenceAnimation.GetComponent<Animation>().setAlignment("top");
		silenceAnimation.GetComponent<Animation>().setCustomOffset(new Vector3(0, 10, 10));
	}
	
	private bool isComplete() {
		return (Time.time - startTime) > duration;
	}
}
