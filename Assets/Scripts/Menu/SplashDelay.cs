using UnityEngine;
using System.Collections;

public class SplashDelay : MonoBehaviour {
	
	public float delayTime = 5;
	
	private float startTime;
	
	void Start() {
		startTime = Time.time;
	}
	
	void Update() {

		if ( Time.time - startTime > delayTime )
			Application.LoadLevel( 1 );	
	}
	
}

