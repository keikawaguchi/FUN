using UnityEngine;
using System.Collections;

public class InGameUI : MonoBehaviour {
	
	public int controller;
	string MerliniPath = "Textures/Items/merlini";
	string TemptressPath = "Textures/Items/temptress";
	string AlbionPath = "Textures/Items/albion";
	string FanndisPath = "";
	Texture icon;
	// Use this for initialization
	void Start () {
		controller = gameObject.GetComponent<Controller>().controllerNumber;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		if(gameObject.name == "Albion")
		{
			icon = Resources.Load(AlbionPath) as Texture;
		}
		if(gameObject.name == "Fanndis")
		{
			icon = Resources.Load(FanndisPath) as Texture;
		}
		if(gameObject.name == "Merlini")
		{
			icon = Resources.Load(MerliniPath) as Texture;
		}
		if(gameObject.name == "Temptress")
		{
			icon = Resources.Load(TemptressPath) as Texture;
		}
		
		if(controller == 1)
		{
			GUI.Button(new Rect(Screen.height/60,Screen.width/120,40,40),icon);
		}
		else if(controller == 2)
		{
			GUI.Button(new Rect(Screen.height/3,Screen.width/120,40,40),icon);
		}
		else if(controller == 3)
		{
			GUI.Button(new Rect(Screen.height-140,Screen.width/120,40,40),icon);
		}
		else if(controller == 4)
		{
			GUI.Button(new Rect(Screen.height-7,Screen.width/120,40,40),icon);
		}
	}
}
