using UnityEngine;
using System.Collections;

public class FanndisBehavior : MonoBehaviour {
	private const string CD_VIEWER_PREFAB_PATH = "Prefabs/Skills/CooldownViewer";
	
	// skill cooldown times
	private const float zeroFrictionCD = 10f;
	private const float iceAgeCD = 3f;
	
	// skill timers
	private float zeroFrictionTimer = -99f;
	private float iceAgeTimer = -99f;
	
	private CharacterMovement characterMovement;
	private Controller controller;
	private ZeroFriction zeroFriction;
	private IceAge iceAge;
	private float iceAgeDestroyTimer;
	
	// view cooldown
	private GameObject skillOneCD;
	private GameObject skillOneCDViewer;
	private GameObject skillTwoCD;
	private GameObject skillTwoCDViewer;

	void Start () {
		loadResources();
		
		skillOneCDViewer = Instantiate(skillOneCD) as GameObject;
		skillTwoCDViewer = Instantiate(skillTwoCD) as GameObject;
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
		
		if(Input.GetButtonDown(controller.getButton("Skill1"))) {
			zeroFictionTriggered();
		}
		
		if(Input.GetButtonDown(controller.getButton("Skill2"))) {
			iceAgeTriggered();
		}
	}
	
	#region Initialization Methods
	private void loadResources() {
		characterMovement = GetComponent<CharacterMovement>();
		controller = GetComponent<Controller>();
		zeroFriction = gameObject.AddComponent<ZeroFriction>();
		
		skillOneCD = Resources.Load (CD_VIEWER_PREFAB_PATH) as GameObject;	
		skillTwoCD = Resources.Load (CD_VIEWER_PREFAB_PATH) as GameObject;	
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
		if (Time.time - zeroFrictionTimer > zeroFrictionCD) {
			// skill 1 here
			Debug.Log("Skill One Triggered!");
			zeroFriction.triggerZeroFriction(gameObject);
			
			// keep track of cooldown timer
			zeroFrictionTimer = Time.time;
		}
	}
	
	private void iceAgeTriggered() {
		if (Time.time - iceAgeTimer > iceAgeCD) {
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
		if(Time.time - zeroFrictionTimer > zeroFrictionCD)
			return 0;
		else
			return (int)((zeroFrictionCD+1) - (Time.time - zeroFrictionTimer));;
	}
	public float geticeAgeCD()
	{
		if(Time.time - iceAgeTimer > iceAgeCD)
			return 0;
		else
			return (int)((iceAgeCD+1) - (Time.time - iceAgeTimer));
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
