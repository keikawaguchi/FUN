using UnityEngine;
using System.Collections;

public class InGameUI : MonoBehaviour {
	
	public int controller;
	string MerliniPath = "Textures/Champions/Merlini";
	string TemptressPath = "Textures/Champions/Temptress";
	string AlbionPath = "Textures/Champions/Albion";
	string FanndisPath = "Textures/Champions/Fanndis";
	
	GUIStyle LivesStyle; 
	GUIStyle SkillsStyle;
	Texture icon;
	// Use this for initialization
	void Start () {
		controller = gameObject.GetComponent<Controller>().controllerNumber;
		
		LivesStyle = new GUIStyle();
		LivesStyle.fontSize = 20;
		LivesStyle.normal.textColor = Color.white;
		
		SkillsStyle = new GUIStyle();
		SkillsStyle.fontSize = 10;
		SkillsStyle.normal.textColor = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		if(gameObject.name == "Albion")
		{
			icon = Resources.Load(AlbionPath) as Texture;
			if(controller == 1)
			{
			}
			else if(controller == 2)
			{
			}
			else if(controller == 3)
			{
			}
			else if(controller == 4)
			{
			}
		}
		if(gameObject.name == "Fanndis")
		{
			icon = Resources.Load(FanndisPath) as Texture;
			/*if(controller == 1)
			{
			
			}
			else if(controller == 2)
			{
				GUI.Label(new Rect(Screen.height-95,Screen.width/150,100,40),"LS:");
			}
			else if(controller == 3)
			{
				GUI.Label(new Rect(Screen.height-95,Screen.width/150,100,40),"Lives:");
			}
			else if(controller == 4)
			{
				GUI.Label(new Rect(Screen.height-95,Screen.width/150,100,40),"Lives:");
			}*/
		}
		if(gameObject.name == "Merlini")
		{
			icon = Resources.Load(MerliniPath) as Texture;
			if(controller == 1)
			{
				GUI.Label(new Rect(Screen.height/6,Screen.width/50,100,40),"Lure:"+ gameObject.GetComponent<TemptressBehavior>().getCoolDown(), SkillsStyle);
				GUI.Label(new Rect(Screen.height/6,Screen.width/27,100,40),"LoveStruck:",SkillsStyle);
			}
			else if(controller == 2)
			{
				GUI.Label(new Rect(Screen.height/2,Screen.width/50,100,40),"Lure:"+ gameObject.GetComponent<TemptressBehavior>().getCoolDown(), SkillsStyle);
				GUI.Label(new Rect(Screen.height/2,Screen.width/27,100,40),"LoveStruck:",SkillsStyle);
			}
			else if(controller == 3)
			{
				GUI.Label(new Rect((int)(Screen.height/1.2),Screen.width/50,100,40),"Lure:"+ gameObject.GetComponent<TemptressBehavior>().getCoolDown(), SkillsStyle);
				GUI.Label(new Rect((int)(Screen.height/1.2),Screen.width/27,100,40),"LoveStruck:",SkillsStyle);
			}
			else if(controller == 4)
			{
				GUI.Label(new Rect((int)(Screen.height/0.88),Screen.width/50,100,40),"Lure:"+ gameObject.GetComponent<TemptressBehavior>().getCoolDown(), SkillsStyle);
				GUI.Label(new Rect((int)(Screen.height/0.88),Screen.width/27,100,40),"LoveStruck:",SkillsStyle);
			}
		}
		if(gameObject.name == "Temptress")
		{
			icon = Resources.Load(TemptressPath) as Texture;
			if(controller == 1)
			{
				GUI.Label(new Rect(Screen.height/6,Screen.width/50,100,40),"Lure:"+ gameObject.GetComponent<TemptressBehavior>().getCoolDown(), SkillsStyle);
				GUI.Label(new Rect(Screen.height/6,Screen.width/27,100,40),"LoveStruck:",SkillsStyle);
			}
			else if(controller == 2)
			{
				GUI.Label(new Rect(Screen.height/2,Screen.width/50,100,40),"Lure:"+ gameObject.GetComponent<TemptressBehavior>().getCoolDown(), SkillsStyle);
				GUI.Label(new Rect(Screen.height/2,Screen.width/27,100,40),"LoveStruck:",SkillsStyle);
			}
			else if(controller == 3)
			{
				GUI.Label(new Rect((int)(Screen.height/1.2),Screen.width/50,100,40),"Lure:"+ gameObject.GetComponent<TemptressBehavior>().getCoolDown(), SkillsStyle);
				GUI.Label(new Rect((int)(Screen.height/1.2),Screen.width/27,100,40),"LoveStruck:",SkillsStyle);
			}
			else if(controller == 4)
			{
				GUI.Label(new Rect((int)(Screen.height/0.88),Screen.width/50,100,40),"Lure:"+ gameObject.GetComponent<TemptressBehavior>().getCoolDown(), SkillsStyle);
				GUI.Label(new Rect((int)(Screen.height/0.88),Screen.width/27,100,40),"LoveStruck:",SkillsStyle);
			}
		}
		
		if(controller == 1)
		{
			GUI.Button(new Rect(Screen.height/70,Screen.width/120,40,40),icon);
			GUI.Label(new Rect(Screen.height/11,Screen.width/150,100,40),"Lives");
			GUI.Label(new Rect(Screen.height/11,Screen.width/35,50,40),""+gameObject.GetComponent<Hero>().lives, LivesStyle);
		}
		else if(controller == 2)
		{
			GUI.Button(new Rect(Screen.height/3,Screen.width/120,40,40),icon);
			GUI.Label(new Rect((int)(Screen.height/2.3),Screen.width/150,100,40),"Lives:");
			GUI.Label(new Rect((int)(Screen.height/2.3),Screen.width/27,50,40),""+gameObject.GetComponent<Hero>().lives, LivesStyle);
		}
		else if(controller == 3)
		{
			GUI.Button(new Rect((int)(Screen.height/1.45),Screen.width/120,40,40),icon);
			GUI.Label(new Rect((int)(Screen.height/1.3),Screen.width/150,100,40),"Lives:");
			GUI.Label(new Rect((int)(Screen.height/1.3),Screen.width/27,50,40),""+gameObject.GetComponent<Hero>().lives, LivesStyle);
		}
		else if(controller == 4)
		{
			GUI.Button(new Rect(Screen.height/1,Screen.width/120,40,40),icon);
			GUI.Label(new Rect((int)(Screen.height/0.93),Screen.width/150,100,40),"Lives:");
			GUI.Label(new Rect((int)(Screen.height/0.93),Screen.width/27,50,40),""+gameObject.GetComponent<Hero>().lives, LivesStyle);
		}
	}
}
