using UnityEngine;
using System.Collections;

public class HammerTimeAOE : MonoBehaviour {
	private const string HAMMERTIME_PREFAB_PATH = "Prefabs/Skills/HammerTimeAOE";
	
	private const float EXPLOSION_TIME = 0.32f;
	
	private float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.time - startTime > EXPLOSION_TIME) {
			Destroy (gameObject);
		}
	}
}
