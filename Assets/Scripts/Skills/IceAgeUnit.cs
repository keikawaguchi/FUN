using UnityEngine;
using System.Collections;

public class IceAgeUnit : MonoBehaviour {
	private const string PLAYER_TAG = "Player";
	
	private const float SLOW_DURATION = 0.06f;
	private const float SLOW_MULTIPLIER = 0.5f;
	
	private GameObject owner;
	private int teamNum;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetOwner(GameObject owner) {
		this.owner = owner;	
	}
	
	public void SetTeamNum(int teamNum) {
		this.teamNum = teamNum;
	}
	
	void OnTriggerStay(Collider collision) {
		// check the collision with tag and exlude the trap owner
		Debug.Log ("Trigger enter!");
		GameObject enemyObj = collision.gameObject;
		Debug.Log ("Collision Tag: " + collision.tag);
		Debug.Log ("Enemy name: " + enemyObj.name);
		
		if (collision.tag == PLAYER_TAG) {
			
			if (collision.gameObject != owner && (teamNum == 0 || teamNum != collision.gameObject.GetComponent<Hero>().getTeamNumber())) {
			
				AlterSpeed alterSpeed = enemyObj.gameObject.AddComponent<AlterSpeed>();
				alterSpeed.Start (SLOW_MULTIPLIER, SLOW_DURATION);
			}
		
		}
	}
}
