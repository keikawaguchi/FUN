using UnityEngine;
using System.Collections;

public class Suterusu : MonoBehaviour {
	private float SUTERUSU_DURATION = 1.5f;
	
	private GameObject owner;
	private KiritoBehavior kirito;
	private Hero hero;
	private CharacterMovement characterMovement;
	private Animation ownerAnimation;
	private bool suterusuUsed;
	private float suterusuTime;

	// Use this for initialization
	void Start () {
		transform.position = owner.transform.position;  // set the particle system position
		suterusuUsed = false;
		suterusuTime = 0f;
		loadScripts();
		
		GameObject animation = Resources.Load ("Prefabs/Animations/SmokeCloud") as GameObject;
		GameObject animationInstance = Instantiate (animation) as GameObject;
		animationInstance.GetComponent<Animation>().setCustomOffset(new Vector3(0, 25, 20));
		animationInstance.GetComponent<Animation>().setPosition(transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		ownerAnimation.setVisibility(false);  // the champ is invisible
		hero.setInvincible (true);  // the champ is invincible
		hero.setCanDropBomb (false);
		characterMovement.setMovementState (CharacterMovement.MovementState.CannotMove);
		kirito.setViewerVisibility (false);
		
		suterusuTime += Time.smoothDeltaTime;  // keep a timer for invisible duration
		
		if (suterusuTime >= SUTERUSU_DURATION) {
			Debug.Log("Visible!");
			ownerAnimation.setVisibility(true);
			hero.setInvincible (false);
			hero.setCanDropBomb (true);
			characterMovement.setMovementState (CharacterMovement.MovementState.CanMove);
			kirito.setViewerVisibility (true);
			Destroy (gameObject);
		}
	}
	
	private void loadScripts() {
		hero = owner.GetComponent<Hero>();
		characterMovement = owner.GetComponent<CharacterMovement>();
	}
	
	public void setOwner(GameObject owner) {
		this.owner = owner;
		kirito = owner.GetComponent<KiritoBehavior>();
	}
	
	public void setAnimationScript(Animation animation) {
		ownerAnimation = animation;
	}
}
