using UnityEngine;
using System.Collections;

public class AlbionBehavior : MonoBehaviour {
	private Controller controller;
	private HolyTrap holyTrap;
	private HolyBlink holyBlink;
	
	// skill cooldown times
	private const float holyTrapCD = 10f;
	private const float holyBlinkCD = 10f;
	
	// skill timers
	private float holyTrapTimer = -99f;
	private float holyBlinkTimer = -99f;
	
	void Start () {
		LoadScripts();
	}
	
	void Update () {
		HolyTrapTriggered ();
		HolyBlinkTriggered ();
	}
	
	private void LoadScripts() {
		controller = GetComponent<Controller>();
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
}
