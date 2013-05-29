using UnityEngine;
using System.Collections;

public class InGameUI : MonoBehaviour {
	
	public int controller;
	string MerliniPath = "Textures/Champions/MerliniIcon";
	string TemptressPath = "Textures/Champions/TemptressIcon";
	string AlbionPath = "Textures/Champions/AlbionIcon";
	string FanndisPath = "Textures/Champions/Fanndis";
	string KiritoPath = "Textures/Champions/KiritoIcon";
	
	GUIStyle LivesStyle; 
	GUIStyle SkillsStyle;
	GUIStyle WordLife;
	Texture icon;
	
	float originalWidth = 800;
	float originalHeight = 600;
	Vector3 scale;
	// Use this for initialization
	void Start () {
		controller = gameObject.GetComponent<Controller>().controllerNumber;

		LivesStyle = new GUIStyle();
		LivesStyle.fontSize = 20;
		LivesStyle.normal.textColor = Color.white;
		LivesStyle.font = Resources.Load("Fonts/Orbitron Medium") as Font;
		
		SkillsStyle = new GUIStyle();
		SkillsStyle.fontSize = 11;
		SkillsStyle.richText = true;
		SkillsStyle.normal.textColor = Color.white;
		SkillsStyle.font = Resources.Load("Fonts/Orbitron Medium") as Font;
		
		WordLife = new GUIStyle();
		WordLife.fontSize = 13;
		WordLife.richText = true;
		WordLife.normal.textColor = Color.white;
		WordLife.font = Resources.Load("Fonts/Orbitron Medium") as Font;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		scale.x = Screen.width/originalWidth; // calculate hor scale
    	scale.y = Screen.height/originalHeight; // calculate vert scale
    	scale.z = 1;
    	var svMat = GUI.matrix; // save current matrix
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);

		if(controller == 1)
		{
			GUI.Box(new Rect(5,3,195,50),"");
			GUI.Button(new Rect(15,8,40,40),icon);
			GUI.Label(new Rect(55,8,100,40),"LIVES",WordLife);
			GUI.Label(new Rect(70,25,50,40),""+gameObject.GetComponent<Hero>().lives, LivesStyle);
		}
		else if(controller == 2)
		{
			GUI.Box(new Rect(205,3,195,50),"");
			GUI.Button(new Rect(215,8,40,40),icon);
			GUI.Label(new Rect(255,8,100,40),"LIVES",WordLife);
			GUI.Label(new Rect(270,25,50,40),""+gameObject.GetComponent<Hero>().lives, LivesStyle);
		}
		else if(controller == 3)
		{
			GUI.Box(new Rect(405,3,195,50),"");
			GUI.Button(new Rect(415,8,40,40),icon);
			GUI.Label(new Rect(455,8,100,40),"LIVES",WordLife);
			GUI.Label(new Rect(470,25,50,40),""+gameObject.GetComponent<Hero>().lives, LivesStyle);
		}
		else if(controller == 4)
		{
			GUI.Box(new Rect(605,3,195,50),"");
			GUI.Button(new Rect(615,8,40,40),icon);
			GUI.Label(new Rect(655,8,100,40),"LIVES",WordLife);
			GUI.Label(new Rect(670,25,50,40),""+gameObject.GetComponent<Hero>().lives, LivesStyle);
		}
		if(gameObject.name == "Albion")
		{
			icon = Resources.Load(AlbionPath) as Texture;
			if(controller == 1)
			{
				GUI.Label(new Rect(95,30,100,40),"Holy Blink: "+ gameObject.GetComponent<AlbionBehavior>().getblinkCD(), SkillsStyle);
				GUI.Label(new Rect(95,20,100,40),"Holy Trap: "+ gameObject.GetComponent<AlbionBehavior>().gettrapCD(), SkillsStyle);
			}
			else if(controller == 2)
			{
				GUI.Label(new Rect(295,30,100,40),"Holy Blink: "+ gameObject.GetComponent<AlbionBehavior>().getblinkCD(), SkillsStyle);
				GUI.Label(new Rect(295,20,100,40),"Holy Trap: "+ gameObject.GetComponent<AlbionBehavior>().gettrapCD(), SkillsStyle);
			}
			else if(controller == 3)
			{
				GUI.Label(new Rect(495,30,100,40),"Holy Blink: "+ gameObject.GetComponent<AlbionBehavior>().getblinkCD(), SkillsStyle);
				GUI.Label(new Rect(495,20,100,40),"Holy Trap: "+ gameObject.GetComponent<AlbionBehavior>().gettrapCD(), SkillsStyle);
			}
			else if(controller == 4)
			{
				GUI.Label(new Rect(695,30,100,40),"Holy Blink: "+ gameObject.GetComponent<AlbionBehavior>().getblinkCD(), SkillsStyle);
				GUI.Label(new Rect(695,20,100,40),"Holy Trap: "+ gameObject.GetComponent<AlbionBehavior>().gettrapCD(), SkillsStyle);
			}
		}
		if(gameObject.name == "Fanndis")
		{
			icon = Resources.Load(FanndisPath) as Texture;
			if(controller == 1)
			{
				GUI.Label(new Rect(95,30,100,40),"Ice Age: "+ gameObject.GetComponent<FanndisBehavior>().geticeAgeCD(), SkillsStyle);
				GUI.Label(new Rect(95,20,100,40),"Zero Friction:"+ gameObject.GetComponent<FanndisBehavior>().getzeroFrictionCD(), SkillsStyle);
			}
			else if(controller == 2)
			{
				GUI.Label(new Rect(295,30,100,40),"Ice Age: "+ gameObject.GetComponent<FanndisBehavior>().geticeAgeCD(), SkillsStyle);
				GUI.Label(new Rect(295,20,100,40),"Zero Friction: "+ gameObject.GetComponent<FanndisBehavior>().getzeroFrictionCD(), SkillsStyle);
			}
			else if(controller == 3)
			{
				GUI.Label(new Rect(495,30,100,40),"Ice Age: "+ gameObject.GetComponent<FanndisBehavior>().geticeAgeCD(), SkillsStyle);
				GUI.Label(new Rect(495,20,100,40),"Zero Friction: "+ gameObject.GetComponent<FanndisBehavior>().getzeroFrictionCD(), SkillsStyle);
			}
			else if(controller == 4)
			{
				GUI.Label(new Rect(695,30,100,40),"Ice Age: "+ gameObject.GetComponent<FanndisBehavior>().geticeAgeCD(), SkillsStyle);
				GUI.Label(new Rect(695,20,100,40),"Zero Friction: "+ gameObject.GetComponent<FanndisBehavior>().getzeroFrictionCD(), SkillsStyle);
			}
		}
		if(gameObject.name == "Merlini")
		{
			icon = Resources.Load(MerliniPath) as Texture;
			if(controller == 1)
			{
				GUI.Label(new Rect(95,20,100,40),"Hammer Time: "+ gameObject.GetComponent<MerliniBehavior>().getHammerTimeCD(), SkillsStyle);
				GUI.Label(new Rect(95,30,100,40),"Bomb Voyage: "+ gameObject.GetComponent<MerliniBehavior>().getBombVoyageCD(), SkillsStyle);
			}
			else if(controller == 2)
			{
				GUI.Label(new Rect(295,20,100,40),"Hammer Time: "+ gameObject.GetComponent<MerliniBehavior>().getHammerTimeCD(), SkillsStyle);
				GUI.Label(new Rect(295,30,100,40),"Bomb Voyage: "+ gameObject.GetComponent<MerliniBehavior>().getBombVoyageCD(), SkillsStyle);
			}
			else if(controller == 3)
			{
				GUI.Label(new Rect(495,20,100,40),"Hammer Time: "+ gameObject.GetComponent<MerliniBehavior>().getHammerTimeCD(), SkillsStyle);
				GUI.Label(new Rect(495,30,100,40),"Bomb Voyage: "+ gameObject.GetComponent<MerliniBehavior>().getBombVoyageCD(), SkillsStyle);
			}
			else if(controller == 4)
			{
				GUI.Label(new Rect(695,20,100,40),"Hammer Time: "+ gameObject.GetComponent<MerliniBehavior>().getHammerTimeCD(), SkillsStyle);
				GUI.Label(new Rect(695,30,100,40),"Bomb Voyage: "+ gameObject.GetComponent<MerliniBehavior>().getBombVoyageCD(), SkillsStyle);
			}
		}
		if(gameObject.name == "Temptress")
		{
			icon = Resources.Load(TemptressPath) as Texture;
			if(controller == 1)
			{
				GUI.Label(new Rect(95,20,100,40),"Lure: "+ gameObject.GetComponent<TemptressBehavior>().getLureCD(), SkillsStyle);
				GUI.Label(new Rect(95,30,100,40),"LoveStruck: "+gameObject.GetComponent<TemptressBehavior>().getLSCD(),SkillsStyle);
			}
			else if(controller == 2)
			{
				GUI.Label(new Rect(295,20,100,40),"Lure: "+ gameObject.GetComponent<TemptressBehavior>().getLureCD(), SkillsStyle);
				GUI.Label(new Rect(295,30,100,40),"LoveStruck: "+gameObject.GetComponent<TemptressBehavior>().getLSCD(),SkillsStyle);
			}
			else if(controller == 3)
			{
				GUI.Label(new Rect(495,20,100,40),"Lure: "+ gameObject.GetComponent<TemptressBehavior>().getLureCD(), SkillsStyle);
				GUI.Label(new Rect(495,30,100,40),"LoveStruck: "+gameObject.GetComponent<TemptressBehavior>().getLSCD(),SkillsStyle);
			}
			else if(controller == 4)
			{
				GUI.Label(new Rect(695,20,100,40),"Lure: "+ gameObject.GetComponent<TemptressBehavior>().getLureCD(), SkillsStyle);
				GUI.Label(new Rect(695,30,100,40),"LoveStruck: "+gameObject.GetComponent<TemptressBehavior>().getLSCD(),SkillsStyle);
			}
		}
		if(gameObject.name == "Kirito")
		{
			icon = Resources.Load(KiritoPath) as Texture;
			if(controller == 1)
			{
				GUI.Label(new Rect(95,20,100,40),"Suterusu: "+ gameObject.GetComponent<KiritoBehavior>().getSuterusuCD(), SkillsStyle);
				GUI.Label(new Rect(95,30,100,40),"Chinmoku: "+gameObject.GetComponent<KiritoBehavior>().getChinmokuCD(),SkillsStyle);
			}
			else if(controller == 2)
			{
				GUI.Label(new Rect(295,20,100,40),"Suterusu: "+ gameObject.GetComponent<KiritoBehavior>().getSuterusuCD(), SkillsStyle);
				GUI.Label(new Rect(295,30,100,40),"Chinmoku: "+gameObject.GetComponent<KiritoBehavior>().getChinmokuCD(),SkillsStyle);
			}
			else if(controller == 3)
			{
				GUI.Label(new Rect(495,20,100,40),"Suterusu: "+ gameObject.GetComponent<KiritoBehavior>().getSuterusuCD(), SkillsStyle);
				GUI.Label(new Rect(495,30,100,40),"Chinmoku: "+gameObject.GetComponent<KiritoBehavior>().getChinmokuCD(),SkillsStyle);
			}
			else if(controller == 4)
			{
				GUI.Label(new Rect(695,20,100,40),"Suterusu: "+ gameObject.GetComponent<KiritoBehavior>().getSuterusuCD(), SkillsStyle);
				GUI.Label(new Rect(695,30,100,40),"Chinmoku: "+gameObject.GetComponent<KiritoBehavior>().getChinmokuCD(),SkillsStyle);
			}
		}
		 GUI.matrix = svMat;
	}
}
