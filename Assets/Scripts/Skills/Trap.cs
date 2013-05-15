using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {
	private const string PLAYER_TAG = "Player";
	
	private GameObject trapOwner;
	private float visibleInberval;
	private bool trapPlaced;
	
	// Use this for initialization
	void Start () {
		visibleInberval = 0f;
		trapPlaced = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!trapPlaced)  // don't know why i need this, it instantiate a lot of trap
			trapPlaced = true;
		
		visibleInberval += Time.smoothDeltaTime;
		if (visibleInberval >= 3f)
			renderer.enabled = false;
	}
	
	void OnTriggerEnter(Collider collision) {
		//TO-DO: Fix passing in the Trap owner name
//		Debug.Log ("Collision name: " + collision.gameObject.name);
//		Debug.Log ("Trap owner name: " + trapOwner.name);
		if (collision.tag == PLAYER_TAG && collision.gameObject.name != "Albion") {  // later need to check by tag
			GameObject enemyObj = GameObject.FindGameObjectWithTag (PLAYER_TAG);  // later need to find by tag
			CharacterMovement characterMove = enemyObj.GetComponent<CharacterMovement>();
			characterMove.setMovementState (CharacterMovement.MovementState.CannotMove);
			Destroy (gameObject);
		}
	}
	
	public void SetTrapOwner(GameObject owner) {
		trapOwner = owner;
	}
}
