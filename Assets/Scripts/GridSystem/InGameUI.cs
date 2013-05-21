using UnityEngine;
using System.Collections;

public class InGameUI : MonoBehaviour {
	
	public int controller;
	string MerliniPath = "Textures/Champions/Merlini";
	string TemptressPath = "Textures/Champions/Temptress";
	string AlbionPath = "Textures/Champions/Albion";
	string FanndisPath = "Textures/Champions/Fanndis";
	
	GUISkin box;//not used currently
	
	GUIStyle LivesStyle; 
	GUIStyle SkillsStyle;
	GUIStyle WordLife;
	Texture icon;
	// Use this for initialization
	void Start () {
		controller = gameObject.GetComponent<Controller>().controllerNumber;
		
		box = new GUISkin();

		LivesStyle = new GUIStyle();
		LivesStyle.fontSize = 20;
		LivesStyle.normal.textColor = Color.white;
		LivesStyle.font = Resources.Load("Fonts/Ruda-Bold") as Font;
		
		SkillsStyle = new GUIStyle();
		SkillsStyle.fontSize = 12;
		SkillsStyle.richText = true;
		SkillsStyle.normal.textColor = Color.white;
		SkillsStyle.font = Resources.Load("Fonts/Ruda-Regular") as Font;
		
		WordLife = new GUIStyle();
		WordLife.fontSize = 15;
		WordLife.richText = true;
		WordLife.normal.textColor = Color.white;
		WordLife.font = Resources.Load("Fonts/Ruda-Bold") as Font;
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
				GUI.Label(new Rect(Screen.height/6,Screen.width/50,100,40),"Blink: "+ gameObject.GetComponent<AlbionBehavior>().getblinkCD(), SkillsStyle);
				GUI.Label(new Rect(Screen.height/6,Screen.width/27,100,40),"Trap: "+ gameObject.GetComponent<AlbionBehavior>().gettrapCD(), SkillsStyle);
			}
			else if(controller == 2)
			{
				GUI.Label(new Rect(Screen.height/2,Screen.width/50,100,40),"Blink: "+ gameObject.GetComponent<AlbionBehavior>().getblinkCD(), SkillsStyle);
				GUI.Label(new Rect(Screen.height/2,Screen.width/27,100,40),"Trap: "+ gameObject.GetComponent<AlbionBehavior>().gettrapCD(), SkillsStyle);
			}
			else if(controller == 3)
			{
				GUI.Label(new Rect((int)(Screen.height/1.2),Screen.width/50,100,40),"Blink: "+ gameObject.GetComponent<AlbionBehavior>().getblinkCD(), SkillsStyle);
				GUI.Label(new Rect((int)(Screen.height/1.2),Screen.width/27,100,40),"Trap: "+ gameObject.GetComponent<AlbionBehavior>().gettrapCD(), SkillsStyle);
			}
			else if(controller == 4)
			{
				GUI.Label(new Rect((int)(Screen.height/0.88),Screen.width/50,100,40),"Blink: "+ gameObject.GetComponent<AlbionBehavior>().getblinkCD(), SkillsStyle);
				GUI.Label(new Rect((int)(Screen.height/0.88),Screen.width/27,100,40),"Trap: "+ gameObject.GetComponent<AlbionBehavior>().gettrapCD(), SkillsStyle);
			}
		}
		if(gameObject.name == "Fanndis")
		{
			icon = Resources.Load(FanndisPath) as Texture;
			if(controller == 1)
			{
				GUI.Label(new Rect(Screen.height/6,Screen.width/50,100,40),"Ice Age: "+ gameObject.GetComponent<FanndisBehavior>().geticeAgeCD(), SkillsStyle);
				GUI.Label(new Rect(Screen.height/6,Screen.width/27,100,40),"Zero Friction:"+ gameObject.GetComponent<FanndisBehavior>().getzeroFrictionCD(), SkillsStyle);
			}
			else if(controller == 2)
			{
				GUI.Label(new Rect(Screen.height/2,Screen.width/50,100,40),"Ice Age: "+ gameObject.GetComponent<FanndisBehavior>().geticeAgeCD(), SkillsStyle);
				GUI.Label(new Rect(Screen.height/2,Screen.width/27,100,40),"Zero Friction: "+ gameObject.GetComponent<FanndisBehavior>().getzeroFrictionCD(), SkillsStyle);
			}
			else if(controller == 3)
			{
				GUI.Label(new Rect((int)(Screen.height/1.2),Screen.width/50,100,40),"Ice Age: "+ gameObject.GetComponent<FanndisBehavior>().geticeAgeCD(), SkillsStyle);
				GUI.Label(new Rect((int)(Screen.height/1.2),Screen.width/27,100,40),"Zero Friction: "+ gameObject.GetComponent<FanndisBehavior>().getzeroFrictionCD(), SkillsStyle);
			}
			else if(controller == 4)
			{
				GUI.Label(new Rect((int)(Screen.height/0.86),Screen.width/50,100,40),"Ice Age: "+ gameObject.GetComponent<FanndisBehavior>().geticeAgeCD(), SkillsStyle);
				GUI.Label(new Rect((int)(Screen.height/0.86),Screen.width/27,100,40),"Zero Friction: "+ gameObject.GetComponent<FanndisBehavior>().getzeroFrictionCD(), SkillsStyle);
			}
		}
		if(gameObject.name == "Merlini")
		{
			icon = Resources.Load(MerliniPath) as Texture;
			if(controller == 1)
			{
				GUI.Label(new Rect(Screen.height/6,Screen.width/50,100,40),"Hammer Time: "+ gameObject.GetComponent<MerliniBehavior>().getHammerTimeCD(), SkillsStyle);
				GUI.Label(new Rect(Screen.height/6,Screen.width/27,100,40),"Bomb Voyage: "+ gameObject.GetComponent<MerliniBehavior>().getBombVoyageCD(), SkillsStyle);
			}
			else if(controller == 2)
			{
				GUI.Label(new Rect((int)(Screen.height/2),Screen.width/50,100,40),"Hammer Time: "+ gameObject.GetComponent<MerliniBehavior>().getHammerTimeCD(), SkillsStyle);
				GUI.Label(new Rect((int)(Screen.height/2),Screen.width/27,100,40),"Bomb Voyage: "+ gameObject.GetComponent<MerliniBehavior>().getBombVoyageCD(), SkillsStyle);
			}
			else if(controller == 3)
			{
				GUI.Label(new Rect((int)(Screen.height/1.2),Screen.width/50,100,40),"Hammer Time: "+ gameObject.GetComponent<MerliniBehavior>().getHammerTimeCD(), SkillsStyle);
				GUI.Label(new Rect((int)(Screen.height/1.2),Screen.width/27,100,40),"Bomb Voyage: "+ gameObject.GetComponent<MerliniBehavior>().getBombVoyageCD(), SkillsStyle);
			}
			else if(controller == 4)
			{
				GUI.Label(new Rect((int)(Screen.height/0.88),Screen.width/50,100,40),"Hammer Time: "+ gameObject.GetComponent<MerliniBehavior>().getHammerTimeCD(), SkillsStyle);
				GUI.Label(new Rect((int)(Screen.height/0.88),Screen.width/27,100,40),"Bomb Voyage: "+ gameObject.GetComponent<MerliniBehavior>().getBombVoyageCD(), SkillsStyle);
			}
		}
		if(gameObject.name == "Temptress")
		{
			icon = Resources.Load(TemptressPath) as Texture;
			if(controller == 1)
			{
				GUI.Label(new Rect(Screen.height/6,Screen.width/50,100,40),"Lure: "+ gameObject.GetComponent<TemptressBehavior>().getCoolDown(), SkillsStyle);
				GUI.Label(new Rect(Screen.height/6,Screen.width/27,100,40),"LoveStruck: ",SkillsStyle);
			}
			else if(controller == 2)
			{
				GUI.Label(new Rect((int)(Screen.height/3),Screen.width/50,100,40),"Lure: "+ gameObject.GetComponent<TemptressBehavior>().getCoolDown(), SkillsStyle);
				GUI.Label(new Rect((int)(Screen.height/3),Screen.width/27,100,40),"LoveStruck: ",SkillsStyle);
			}
			else if(controller == 3)
			{
				GUI.Label(new Rect((int)(Screen.height/1.2),Screen.width/50,100,40),"Lure: "+ gameObject.GetComponent<TemptressBehavior>().getCoolDown(), SkillsStyle);
				GUI.Label(new Rect((int)(Screen.height/1.2),Screen.width/27,100,40),"LoveStruck: ",SkillsStyle);
			}
			else if(controller == 4)
			{
				GUI.Label(new Rect((int)(Screen.height/0.88),Screen.width/50,100,40),"Lure: "+ gameObject.GetComponent<TemptressBehavior>().getCoolDown(), SkillsStyle);
				GUI.Label(new Rect((int)(Screen.height/0.88),Screen.width/27,100,40),"LoveStruck: ",SkillsStyle);
			}
		}
		
		if(controller == 1)
		{
			GUI.Button(new Rect(Screen.height/40,Screen.width/100,40,40),icon);
			GUI.Box(new Rect(Screen.height/150,Screen.width/190,200,50),"");
			GUI.Label(new Rect(Screen.height/10,Screen.width/150,100,40),"Lives",WordLife);
			GUI.Label(new Rect((int)(Screen.height/8.2),Screen.width/35,50,40),""+gameObject.GetComponent<Hero>().lives, LivesStyle);
		}
		else if(controller == 2)
		{
			GUI.Button(new Rect((int)(Screen.height/2.8),Screen.width/100,40,40),icon);
			GUI.Box(new Rect((int)(Screen.height/2.96),Screen.width/190,200,50),"");
			GUI.Label(new Rect((int)(Screen.height/2.3),Screen.width/150,100,40),"Lives",WordLife);
			GUI.Label(new Rect((int)(Screen.height/2.2),Screen.width/35,50,40),""+gameObject.GetComponent<Hero>().lives, LivesStyle);
		}
		else if(controller == 3)
		{
			GUI.Button(new Rect((int)(Screen.height/1.45),Screen.width/100,40,40),icon);
			GUI.Box(new Rect((int)(Screen.height/1.49),Screen.width/190,200,50),"");
			GUI.Label(new Rect((int)(Screen.height/1.3),Screen.width/150,100,40),"Lives",WordLife);
			GUI.Label(new Rect((int)(Screen.height/1.26),Screen.width/35,50,40),""+gameObject.GetComponent<Hero>().lives, LivesStyle);
		}
		else if(controller == 4)
		{
			GUI.Button(new Rect((int)(Screen.height/0.98),Screen.width/100,40,40),icon);
			GUI.Box(new Rect(Screen.height/1,Screen.width/190,200,50),"");
			GUI.Label(new Rect((int)(Screen.height/0.91),Screen.width/150,100,40),"Lives",WordLife);
			GUI.Label(new Rect((int)(Screen.height/0.89),Screen.width/35,50,40),""+gameObject.GetComponent<Hero>().lives, LivesStyle);
		}
	}
}
