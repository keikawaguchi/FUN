using UnityEngine;
using System.Collections;

public class LSEnemy : MonoBehaviour {
	
	public static GameObject theHero;
	public float speed = 60f;
	bool slowDown;
	float SpaceBar;
	
	
	// Use this for initialization
	void Start () 
	{
		Bounds theBound = LSBehaviour.loveBehavior.GetWorldBound;
		Vector3 location;
		Vector3 location2 = new Vector3(Random.value, 0f, Random.value);
		location2.Normalize();
		location.y = 0f;
		location.x = Random.Range(theBound.min.x, theBound.max.x);
		location.z = Random.Range(theBound.min.z, theBound.max.z);
		transform.position = location;
		transform.forward = location2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(LSBehaviour.loveBehavior.ObjectCollideWorldBound(transform.collider.bounds) != LSBehaviour.WorldBoundStatus.Inside)
		{
			NewDirection();
		}
		Vector3 position = theHero.transform.position - transform.position;
		
		if((Vector3.Dot(position, theHero.transform.forward) < 0f) && (position.sqrMagnitude < 900f) && SpaceBarPressed() == true)
		{
			speed = 10f;
		}
		else
		{
			speed = 60f;
			pressSpaceBar(false);
		}
		Transform temp = transform;
		temp.position += (Vector3)((Time.smoothDeltaTime*speed) * transform.forward);
		if((SpaceBar == 0f) && (Input.GetAxis("Jump") > 0f))
		{
			pressSpaceBar(!slowDown);
		}
		SpaceBar = Input.GetAxis("Jump");
	}
	
	public bool SpaceBarPressed()
	{
		return slowDown;
	}
	public void pressSpaceBar(bool param)
	{
		slowDown = param;
	}
	
	private void NewDirection() 
	{
		Vector3 location;
		location.y = 0f;
		location.x = LSBehaviour.loveBehavior.GetWorldBound.min.x + 
			(Random.Range((float) 0.1f, (float) 0.9f) * LSBehaviour.loveBehavior.GetWorldBound.max.x);
		location.z = LSBehaviour.loveBehavior.GetWorldBound.min.z + 
			(Random.Range((float) 0.1f, (float) 0.9f) * LSBehaviour.loveBehavior.GetWorldBound.max.z);
		transform.forward = location - transform.position;
	}
}
