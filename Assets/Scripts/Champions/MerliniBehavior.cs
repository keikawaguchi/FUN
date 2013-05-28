using UnityEngine;
using System.Collections;

public class MerliniBehavior : MonoBehaviour {
	private const string HAMMERTIME_PREFAB_PATH = "Prefabs/Skills/HammerTime";
	private const string BOMB_PREFAB_PATH = "Prefabs/Bomb/Bomb";
	
	private CharacterMovement characterMovement;
	private Controller controller;
	
	private GameObject animation;
	private GameObject hammerPrefab;
	private GameObject hammer;
	private GameObject bomb;
	
	// skill cooldown times
	private const float hammerTimeCD = 10f;
	private const float bombVoyageCD = 25f;
	
	// skill timers
	private float hammerTimeTimer = -99f;
	private float bombVoyageTimer = -99f;
	

	void Start () {
		loadSkills();
		loadScripts();
		loadAnimation();
	}
	
	void Update () {
		if (isStunned()) {
			return;
		}
		
		if(Input.GetButtonDown(controller.getButton("Skill1"))) {
			hammerTimeButtonPress();
		}
		
		if(Input.GetButtonDown(controller.getButton("Skill2"))) {
			bombVoyageButtonPress();
		}
	}
	
	#region Initialization Methods
	private void loadSkills() {
		hammerPrefab = Resources.Load (HAMMERTIME_PREFAB_PATH) as GameObject;
		bomb = Resources.Load (BOMB_PREFAB_PATH) as GameObject;
	}
	
	private void loadScripts() {
		characterMovement = GetComponent<CharacterMovement>();
		controller = GetComponent<Controller>();
	}
	
	private void loadAnimation() {
		animation = Resources.Load ("Prefabs/Animations/Characters/Wizard") as GameObject;
		animation = Instantiate (animation) as GameObject;
		animation.GetComponent<Animation>().attachToObject (gameObject);
	}
	#endregion
	
	private bool isStunned() {
		return characterMovement.isStunned();
	}
	
	#region Merlini Skills
	private void hammerTimeButtonPress() {
		
		// check if cooldown expired
		if (Time.time - hammerTimeTimer > hammerTimeCD) {

			Debug.Log("It's hammer time!!");
			hammer = Instantiate (hammerPrefab) as GameObject;
			hammer.GetComponent<HammerTime>().SetStunOwner (gameObject);
			hammer.transform.position = transform.position;
			hammer.transform.forward = characterMovement.getAimDirection();
			
			int teamNum = gameObject.GetComponent<Hero>().getTeamNumber();
			hammer.GetComponent<HammerTime>().SetTeamNum(teamNum);
			
			// keep track of cooldown timer
			hammerTimeTimer = Time.time;
		}
	}
	
	private void bombVoyageButtonPress() {
		
		if (Input.GetButtonDown(controller.getButton("Skill2"))) {
			
			if (Time.time - bombVoyageTimer > bombVoyageCD) {
				
				GameObject[] players;
				players = GameObject.FindGameObjectsWithTag("Player");
		
				foreach(GameObject player in players) {
					GameObject instantiateBomb = Instantiate(bomb) as GameObject;
					instantiateBomb.transform.position = player.transform.position;
				}
				
				bombVoyageTimer = Time.time;
			}
		}
	}
	#endregion
	
	public float getHammerTimeCD()
	{
		if(Time.time - hammerTimeTimer > hammerTimeCD)
			return 0;
		else
			return (int)((hammerTimeCD+1) - (Time.time - hammerTimeTimer));;
	}
	public float getBombVoyageCD()
	{
		if(Time.time - bombVoyageTimer > bombVoyageCD)
			return 0;
		else
			return (int)((bombVoyageCD+1) - (Time.time - bombVoyageTimer));
	}
}
