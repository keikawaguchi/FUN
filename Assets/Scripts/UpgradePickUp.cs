using UnityEngine;
using System.Collections;

public class UpgradePickUp : MonoBehaviour {
	
	bool showing = true;
	int upgradeType;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		upgradeType = Random.Range(1,3);
		if( upgradeType == 1)
		{
			GetComponent<MeshRenderer>().material.mainTexture = Resources.Load ("Materials/SpeedUpgrade") as Texture;
		}
		else if( upgradeType == 2 )
		{
			GetComponent<MeshRenderer>().material.mainTexture = Resources.Load ("Materials/ExplosionUpgrade") as Texture;
		}
		else if( upgradeType == 3 )
		{
			GetComponent<MeshRenderer>().material.mainTexture = Resources.Load ("Materials/BombUpgrade") as Texture;
		}
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
	
	public enum upgrades
	{
		SpeedUp = 1,
		Explosion = 2,
		Bomb = 3
		
	};
}
