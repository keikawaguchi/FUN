using UnityEngine;
using System.Collections;

public class TextControl : MonoBehaviour {
	public bool isQuitButton = false;
	public bool isExplosion = false;
	public bool isLoveStruck = false;
	public bool isMapLayout = false;
	public bool isBackButton = false;

	void OnMouseEnter()
	{
		renderer.material.color = Color.green;
	}
	
	void OnMouseExit()
	{
		renderer.material.color = Color.white;
	}
	
	void OnMouseUp()
	{
		if(isQuitButton == true)
		{
			Application.Quit();
		}
		else if(isExplosion == true)
		{
			Application.LoadLevel(3);
		}
		else if(isLoveStruck == true)
		{
			Application.LoadLevel(2);
		}
		else if(isMapLayout == true)
		{
			Application.LoadLevel(1);
		}
		else if(isBackButton == true)
		{
			Application.LoadLevel(0);
		}
		else
		{
			Application.LoadLevel(3);
		}
	}
}
