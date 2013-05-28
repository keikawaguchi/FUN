using UnityEngine;
using System.Collections;

public class AlbionBehavior : MonoBehaviour {
	private const string CD_VIEWER_PREFAB_PATH = "Prefabs/Skills/CooldownViewer";
	
	private Controller controller;
	private HolyTrap holyTrap;
	private HolyBlink holyBlink;
	private GameObject animation;
	
	// skill cooldown times
	private const float holyTrapCD = 10f;
	private const float holyBlinkCD = 15f;
	
	// skill timers
	private float holyTrapTimer = -99f;
	private float holyBlinkTimer = -99f;
	
	// view cooldown
	private GameObject skillOneCD;
	private GameObject skillOneCDViewer;
	private GameObject skillTwoCD;
	private GameObject skillTwoCDViewer;
	
	void Start () {
		LoadScripts();
		loadSkills();
		loadAnimation();
		
		skillOneCDViewer = Instantiate(skillOneCD) as GameObject;
		skillTwoCDViewer = Instantiate(skillTwoCD) as GameObject;
	}
	
	void Update () {
		updateCDViewerPos();
		updateCDViewerColor();
		
		HolyTrapTriggered ();
		HolyBlinkTriggered ();
	}
	
	private void LoadScripts() {
		controller = GetComponent<Controller>();
	}
	
	private void loadSkills() {
		skillOneCD = Resources.Load (CD_VIEWER_PREFAB_PATH) as GameObject;	
		skillTwoCD = Resources.Load (CD_VIEWER_PREFAB_PATH) as GameObject;	
	}
	
	private void loadAnimation() {	
		animation = Resources.Load ("Prefabs/Animations/Characters/Hunter") as GameObject;
		animation = Instantiate (animation) as GameObject;
		animation.GetComponent<Animation>().attachToObject (gameObject);
	}
	
	private void HolyTrapTriggered() {
		if (Input.GetButtonDown (controller.getButton ("Skill1"))) {
			if (Time.time - holyTrapTimer > holyTrapCD) {
				holyTrap = gameObject.AddComponent<HolyTrap>();
				holyTrap.SetTrapOwner(gameObject);
				
				holyTrapTimer = Time.time;
			}
		}
	}
	
	private void HolyBlinkTriggered() {
		if (Input.GetButtonDown (controller.getButton ("Skill2"))) {
			if (Time.time - holyBlinkTimer > holyBlinkCD) {
				holyBlink = gameObject.AddComponent<HolyBlink>();
				holyBlink.SetOwner(gameObject);
			
				holyBlinkTimer = Time.time;
			}
		}
	}
	
	public int gettrapCD()
	{
		if(Time.time - holyTrapTimer > holyTrapCD)
			return 0;
		else
			return (int)((holyTrapCD+1) - (Time.time - holyTrapTimer));
	}
	
	public int getblinkCD()
	{
		if(Time.time - holyBlinkTimer > holyBlinkCD)
			return 0;
		else
			return (int)((holyBlinkCD+1) - (Time.time - holyBlinkTimer));
	}
	
	public GameObject getAnimationObject() {
		return animation;
	}
	
	#region Cooldown Viewer Methods
	private void updateCDViewerPos() {
		CooldownViewer viewer = skillOneCDViewer.GetComponent<CooldownViewer>();
		viewer.updateCDViewerPosition(transform.position, true);
		viewer = skillTwoCDViewer.GetComponent<CooldownViewer>();
		viewer.updateCDViewerPosition(transform.position, false);
	}
	
	private void updateCDViewerColor() {
		
		CooldownViewer viewer = skillOneCDViewer.GetComponent<CooldownViewer>();
		viewer.updateCDViewerColor( (gettrapCD() != 0) );
			
		viewer = skillTwoCDViewer.GetComponent<CooldownViewer>();
		viewer.updateCDViewerColor( (getblinkCD() != 0) );
	}
	#endregion
}
