using UnityEngine;
using System.Collections;

public class FanndisBehavior : MonoBehaviour {
	private const string ICEAGE_SFX_PATH = "Audio/SFX/iceCracking";
	
	private const string CD_VIEWER_PREFAB_PATH = "Prefabs/Skills/CooldownViewer";
	private const string ICEAGE_PREFAB_PATH = "Prefabs/Skills/IceAge";
	
	private const string IDLE_TEXTURE_PATH = "Textures/SpriteSheets/Characters/Fanndis/FanndisIdleSpritesheet";
	private const string RUNNING_TEXTURE_PATH = "Textures/SpriteSheets/Characters/Fanndis/FanndisRunningSpritesheet";
	
	// skill cooldown times
	private const float skillOneCD = 10f;  // zero friction
	private const float skillTwoCD = 3f;  // ice age
	private const float iceAgeDuration = 3f;
	
	// skill timers
	private float zeroFrictionTimer = -99f;
	private float iceAgeTimer = -99f;
	
	private CharacterMovement characterMovement;
	private XInputController controller;
	private ZeroFriction zeroFriction;
	private float iceAgeDestroyTimer;
	private GameObject animation;
	
	private GameObject iceAgePrefab;
	private GameObject iceAge;
	private AudioClip iceAgeSFX;
	
	// view cooldown
	private GameObject skillOneCDPrefab;
	private GameObject skillOneCDViewer;
	private GameObject skillTwoCDPrefab;
	private GameObject skillTwoCDViewer;

	void Start () {
		loadResources();
		loadAnimation();
		
		skillOneCDViewer = Instantiate(skillOneCDPrefab) as GameObject;
		skillTwoCDViewer = Instantiate(skillTwoCDPrefab) as GameObject;
	}
	
	void Update () {		
		updateCDViewerPos();
		updateCDViewerColor();	
		
		// don't do anything if hero is dead
		bool isAlive = gameObject.GetComponent<Hero>().isAlive;
		if (!isAlive)
			return;

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
		iceAgePrefab = Resources.Load (ICEAGE_PREFAB_PATH) as GameObject;
		iceAgeSFX = Resources.Load (ICEAGE_SFX_PATH) as AudioClip;
		
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
			zeroFriction.triggerZeroFriction(gameObject);
			
			// keep track of cooldown timer
			zeroFrictionTimer = Time.time;
		}
	}
	
	private void iceAgeTriggered() {
		if (Time.time - iceAgeTimer > skillTwoCD) {
			
			AudioSource.PlayClipAtPoint(iceAgeSFX, transform.position, 0.6f);
			
			Vector3 blockPos = transform.position;
			Vector3 aimDirection = characterMovement.getAimDirection();
			
			int teamNum = gameObject.GetComponent<Hero>().getTeamNumber();
			
			for (int i = 0; i < 6; i++) {
				iceAge = Instantiate (iceAgePrefab) as GameObject;
				iceAge.GetComponent<IceAgeUnit>().SetOwner(gameObject);
				iceAge.GetComponent<IceAgeUnit>().SetTeamNum(teamNum);
				
				blockPos.y = -1f;	// so ice cubes show below walls and such
				iceAge.transform.position = blockPos;
				iceAge.transform.forward = aimDirection;
				
				Destroy(iceAge, iceAgeDuration);
				
				blockPos += iceAge.transform.forward * 10f;
			}
			
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
