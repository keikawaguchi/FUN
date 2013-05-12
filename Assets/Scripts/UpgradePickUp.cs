using UnityEngine;
using System.Collections;

public class UpgradePickUp : MonoBehaviour {
	
	int upgradeType;
	// Use this for initialization
	void Start () 
	{
		upgradeType = Random.Range(1,4);
		if( upgradeType == 1)
		{
			GetComponent<MeshRenderer>().renderer.material = Resources.Load ("Materials/SpeedUpgrade") as Material;
		}
		else if( upgradeType == 2 )
		{
			GetComponent<MeshRenderer>().renderer.material = Resources.Load ("Materials/ExplosionUpgrade") as Material;
		}
		else if( upgradeType == 3 )
		{
			GetComponent<MeshRenderer>().renderer.material = Resources.Load ("Materials/BombUpgrade") as Material;
		}
		renderer.enabled = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(renderer.enabled == false)
		{
			upgradeType = Random.Range(1,4);
			if( upgradeType == 1)
			{
				GetComponent<MeshRenderer>().renderer.material = Resources.Load ("Materials/SpeedUpgrade") as Material;
			}
			else if( upgradeType == 2 )
			{
				GetComponent<MeshRenderer>().renderer.material = Resources.Load ("Materials/ExplosionUpgrade") as Material;
			}
			else if( upgradeType == 3 )
			{
				GetComponent<MeshRenderer>().renderer.material = Resources.Load ("Materials/BombUpgrade") as Material;
			}
		}
	}
	
	public void OnTriggerEnter()
	{
		StartCoroutine(wait());
	}
	
	IEnumerator wait()
	{
		renderer.enabled = false;
		yield return new WaitForSeconds(30);
		renderer.enabled = true;
	}
	
	public enum upgrades
	{
		SpeedUp = 1,
		Explosion = 2,
		Bomb = 3
		
	};
}
