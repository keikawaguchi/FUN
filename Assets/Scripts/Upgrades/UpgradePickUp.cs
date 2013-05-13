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
			renderer.name = "SpeedUpgrade";
			renderer.enabled = true;
		}
		else if( upgradeType == 2 )
		{
			GetComponent<MeshRenderer>().renderer.material = Resources.Load ("Materials/ExplosionUpgrade") as Material;
			renderer.name = "ExplosionUpgrade";
			renderer.enabled = true;
		}
		else if( upgradeType == 3 )
		{
			GetComponent<MeshRenderer>().renderer.material = Resources.Load ("Materials/BombUpgrade") as Material;
			renderer.name = "BombUpgrade";
			renderer.enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	public void OnTriggerEnter(Collider player)
	{
		if(player.name == "Temptress")
			StartCoroutine(wait());
	}
	
	public IEnumerator wait()
	{
		renderer.enabled = false;
		
		yield return new WaitForSeconds(30);
		
		upgradeType = Random.Range(1,4);
		if( upgradeType == 1)
		{
			GetComponent<MeshRenderer>().renderer.material = Resources.Load ("Materials/SpeedUpgrade") as Material;
			renderer.name = "SpeedUpgrade";
			renderer.enabled = true;
		}
		else if( upgradeType == 2 )
		{
			GetComponent<MeshRenderer>().renderer.material = Resources.Load ("Materials/ExplosionUpgrade") as Material;
			renderer.name = "ExplosionUpgrade";
			renderer.enabled = true;
		}
		else if( upgradeType == 3 )
		{
			GetComponent<MeshRenderer>().renderer.material = Resources.Load ("Materials/BombUpgrade") as Material;
			renderer.name = "BombUpgrade";
			renderer.enabled = true;
		}
		
	}
}
