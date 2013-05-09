using UnityEngine;
using System.Collections;

public class UpgradePickUp : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	public void OnTriggerEnter()
	{
		StartCoroutine(wait());
	}
	
	IEnumerator wait()
	{
		renderer.enabled = false;
		yield return new WaitForSeconds(3);
		renderer.enabled = true;
	}
}
