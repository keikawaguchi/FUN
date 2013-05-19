using UnityEngine;
using System.Collections;

public class TextControl : MonoBehaviour {
	public bool isQuitButton = false;
	public bool isNextButton = false;
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
		else if(isNextButton == true)
		{
			Application.LoadLevel(1);
		}
	}
}
