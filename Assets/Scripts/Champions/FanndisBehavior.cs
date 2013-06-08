using UnityEngine;
using System.Collections;

public class FanndisBehavior : MonoBehaviour {
	private const string CD_VIEWER_PREFAB_PATH = "Prefabs/Skills/CooldownViewer";
	
	private const string IDLE_TEXTURE_PATH = "Textures/SpriteSheets/Characters/Fanndis/FanndisIdleSpritesheet";
	private const string RUNNING_TEXTURE_PATH = "Textures/SpriteSheets/Characters/Fanndis/FanndisRunningSpritesheet";
	
	// skill cooldown times
	private const float skillOneCD = 10f;  // zero friction
	private const float skillTwoCD = 3f;  // ice age
	
	// skill timers
	private float zeroFrictionTimer = -99f;
	private float iceAgeTimer = -99f;
	
	private CharacterMovement characterMovement;
	private XInputController controller;
	private ZeroFriction zeroFriction;
	private IceAge iceAge;
	private float iceAgeDestroyTimer;
	private GameObject animation;
	
	// view cooldown
	private GameObject skillOneCDPrefab;
	private GameObject skillOneCDViewer;
	private GameObject skillTwoCDPrefab;
	private GameObject skillTwoCDViewer;

	void Start () {
		loadResources();
		loadAnimation();
		
		
		GameObject a = Resources.Load ("Prefabs/Team/RedTeamRing") as GameObject;
		a = Instantiate (a) as GameObject;
		a.GetComponent<Animation>().attachToObject (gameObject);
		a.GetComponent<Animation>().setCustomOffset(new Vector3(0, -1, 5));
		
		skillOneCDViewer = Instantiate(skillOneCDPrefab) as GameObject;
		skillTwoCDViewer = Instantiate(skillTwoCDPrefab) as GameObject;
	}
	
	void Update () {
		updateCDViewerPos();
		updateCDViewerColor();		

		if (isStunned()) {
			return;
		}
		
		if (isSilence ()) {
			return;
		}
		
		if(controller.GetButtonPressed("Skill1")) {
			zeroFictionTriggered();
		}
		
		if(controller.GetButtonPressed("Skill2")) {
			iceAgeTriggered();
		}
	}
	
	#region Initialization Methods
	private void loadResources() {
		characterMovement = GetComponent<CharacterMovement>();
		controller = GetComponent<XInputController>();
		zeroFriction = gameObject.AddComponent<ZeroFriction>();
		
		skillOneCDPrefab = Resources.Load (CD_VIEWER_PREFAB_PATH) as GameObject;	
		skillTwoCDPrefab = Resources.Load (CD_VIEWER_PREFAB_PATH) as GameObject;	
	}
	
	private void loadAnimation() {
		animation = Resources.Load ("Prefabs/Animations/Characters/Fanndis") as GameObject;
		animation = Instantiate (animation) as GameObject;
		animation.GetComponent<Animation>().attachToObject (gameObject);
		
		Texture idle = Resources.Load (IDLE_TEXTURE_PATH) as Texture;
		Texture running = Resources.Load (RUNNING_TEXTURE_PATH) as Texture;
		animation.GetComponent<Animation>().setIdleTexture(idle);
		animation.GetComponent<Animation>().setRunningTexture(running);
	}
	#endregion
	
	private bool isStunned() {
		return characterMovement.isStunned();
	}
	
	private bool isSilence() {
		return characterMovement.getIsSilence ();
	}
	
	#region Character Skills
	private void zeroFictionTriggered() {
		if (Time.time - zeroFrictionTimer > skillOneCD) {
			// skill 1 here
			Debug.Log("Skill One Triggered!");
			zeroFriction.triggerZeroFriction(gameObject);
			
			// keep track of cooldown timer
			zeroFrictionTimer = Time.time;
		}
	}
	
	private void iceAgeTriggered() {
		if (Time.time - iceAgeTimer > skillTwoCD) {
			// skill 2 here
			Debug.Log("Skill Two Triggered!");
			iceAge = gameObject.AddComponent<IceAge>();
			iceAge.setOwner (gameObject);
			
			// keep track of cooldown timer
			iceAgeTimer = Time.time;
		}
	}
	#endregion
	
	public float getzeroFrictionCD()
	{
		if(Time.time - zeroFrictionTimer > skillOneCD)
			return 0;
		else
			return (int)((skillOneCD+1) - (Time.time - zeroFrictionTimer));;
	}
	public float geticeAgeCD()
	{
		if(Time.time - iceAgeTimer > skillTwoCD)
			return 0;
		else
			return (int)((skillTwoCD+1) - (Time.time - iceAgeTimer));
	}
	
	public float getSkillOneCD() {
		return skillOneCD;
	}
	
	public float getSkillTwoCD() {
		return skillTwoCD;
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
		viewer.updateCDViewerColor( (getzeroFrictionCD() != 0) );
			
		viewer = skillTwoCDViewer.GetComponent<CooldownViewer>();
		viewer.updateCDViewerColor( (geticeAgeCD() != 0) );
	}
	#endregion
}
