using UnityEngine;
using System.Collections;

public class ZeroFriction : MonoBehaviour {
	private const float SPEED_MULTIPLIER = 1.75f;
	private const float DURATION = 2f;
	private const string ZERO_FRICTION_SFX_PATH = "Audio/SFX/speedUp";
	
	private AudioClip zeroFrictionSFX;
	
	
	// Use this for initialization
	void Start () {
		zeroFrictionSFX = Resources.Load(ZERO_FRICTION_SFX_PATH) as AudioClip;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void triggerZeroFriction(GameObject champion) {
		AlterSpeed alterSpeed;
		alterSpeed = champion.AddComponent<AlterSpeed>();
		alterSpeed.Start(SPEED_MULTIPLIER, DURATION);
		AudioSource.PlayClipAtPoint (zeroFrictionSFX, transform.position, 0.5f);
	}
}
