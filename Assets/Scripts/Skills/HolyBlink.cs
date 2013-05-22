using UnityEngine;
using System.Collections;

public class HolyBlink : MonoBehaviour {
	private const string BLINK_BUTTON = "Jump";
	private const int BLINK_UNITS = 3;
	private const int GRID_MIN_COORD = 0;
	private const string BLINK_SFX_PATH = "Audio/SFX/teleport";
	
	private float singleGridSize;
	private float blinkDistance;
	private Vector3 blinkPos;
	private bool blinked;
	private GameObject heroObj;
	private AudioClip blinkSFX;
	
	private Map map;
	private GridSystem gridSystem;
	private CharacterMovement characterMovement;
	

	// Use this for initialization
	void Start () {
		GameObject mapObj = GameObject.Find ("Map");
		map = mapObj.GetComponent<Map>();
		gridSystem = mapObj.GetComponent<GridSystem>();
		characterMovement = heroObj.GetComponent<CharacterMovement>();
		blinkSFX = Resources.Load(BLINK_SFX_PATH) as AudioClip;
		
		singleGridSize = gridSystem.getSingleGridWidth();
		blinkDistance = singleGridSize * BLINK_UNITS;
		blinked = false;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		 * Conditions:
		 * 1. Out of bound
		 * 2. Jump onto walls
		 * 3. Jump onto bombs
		 */
		
		if (!blinked) {  // same here, why do i need this??????
			blinked = true;
			Vector3 newPos = heroObj.transform.position;
			Vector3 blinkDirection = characterMovement.getAimDirection();
			
			// make the 
			if (blinkDirection.x > 0)
				blinkDirection.x = 1;
			if (blinkDirection.x < 0)
				blinkDirection.x = -1;
			if (blinkDirection.z > 0)
				blinkDirection.z = 1;
			if (blinkDirection.z  < 0)
				blinkDirection.z = -1;
			
			newPos += blinkDirection * blinkDistance;  // blink to facing direction
			int blinkToGridX = gridSystem.getXPos(newPos.x);
			int blinkToGridY = gridSystem.getYPos(newPos.z);
			
			while (map.isGridFull(blinkToGridX,blinkToGridY) && blinkToGridX != 0) {
				blinkToGridX -= (int)blinkDirection.x;
				blinkToGridY -= (int)blinkDirection.z;
			}
			
			newPos.x = gridSystem.getXCoord(blinkToGridX);
			newPos.z = gridSystem.getYCoord(blinkToGridY);
			
			heroObj.transform.position = newPos;  // teleport to facing direction
			AudioSource.PlayClipAtPoint(blinkSFX, transform.position, 1.0f);
		}
	}
	
	public void SetOwner(GameObject owner) {
		heroObj = owner;
	}
}