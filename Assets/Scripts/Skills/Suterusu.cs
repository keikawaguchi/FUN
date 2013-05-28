using UnityEngine;
using System.Collections;

public class Suterusu : MonoBehaviour {
	private GameObject owner;
	private Hero hero;
	private Animation ownerAnimation;
	private bool suterusuUsed;
	private float suterusuDuration;

	// Use this for initialization
	void Start () {
		transform.position = owner.transform.position;  // set the particle system position
		suterusuUsed = false;
		suterusuDuration = 0f;
		loadScripts();
	}
	
	// Update is called once per frame
	void Update () {
		ownerAnimation.setVisibility(false);  // the champ is invisible
		hero.setInvincible (true);  // the champ is invincible
		
		suterusuDuration += Time.smoothDeltaTime;  // keep a timer for invisible duration
		
		if (suterusuDuration >= 3f) {  // visible after 3 sec
			Debug.Log("Visible!");
			ownerAnimation.setVisibility(true);
			hero.setInvincible (false);
			Destroy (gameObject);
		}
	}
	
	private void loadScripts() {
		hero = owner.GetComponent<Hero>();
	}
	
	public void setOwner(GameObject owner) {
		this.owner = owner;
	}
	
	public void setAnimationScript(Animation animation) {
		ownerAnimation = animation;
	}
}
