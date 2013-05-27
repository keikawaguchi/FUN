using UnityEngine;
using System.Collections;

public class HolyTrapUnit : MonoBehaviour {
	private const string PLAYER_TAG = "Player";
	
	private GameObject owner;
	private bool trapPlaced;
	
	// Use this for initialization
	void Start () {
		trapPlaced = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!trapPlaced)  // don't know why i need this, it instantiate a lot of trap
			trapPlaced = true;
	}
	
	void OnTriggerEnter(Collider collision) {
		// check the collision with tag and exlude the trap owner
		GameObject enemyObj = collision.gameObject;
		
		int collisionTeamNum = enemyObj.GetComponent<Hero>().getTeamNumber();
		int ownerTeamNum = owner.GetComponent<Hero>().getTeamNumber();
		
		if (collision.tag == PLAYER_TAG && collisionTeamNum != ownerTeamNum) {
			AlterSpeed alterSpeed = enemyObj.gameObject.AddComponent<AlterSpeed>();
			alterSpeed.Start (0f, 2f);
			Destroy (this.gameObject);
		}
	}
	
	public void SetTrapOwner(GameObject owner) {
		this.owner = owner;
	}
}
