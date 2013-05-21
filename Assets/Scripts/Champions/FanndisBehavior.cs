using UnityEngine;
using System.Collections;

public class FanndisBehavior : MonoBehaviour {
	// skill cooldown times
	private const float zeroFrictionCD = 1f;
	private const float iceAgeCD = 1f;
	
	// skill timers
	private float zeroFrictionTimer = -99f;
	private float iceAgeTimer = -99f;
	
	private CharacterMovement characterMovement;
	private Controller controller;
	private ZeroFriction zeroFriction;
	private IceAge iceAge;
	private float iceAgeDestroyTimer;

	void Start () {
		loadScripts();
	}
	
	void Update () {
		if (isStunned()) {
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
	private void loadScripts() {
		characterMovement = GetComponent<CharacterMovement>();
		controller = GetComponent<Controller>();
		zeroFriction = gameObject.AddComponent<ZeroFriction>();
	}
	#endregion
	
	private bool isStunned() {
		return characterMovement.isStunned();
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
}
