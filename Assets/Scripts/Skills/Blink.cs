using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour {
	private const string BLINK_BUTTON = "Jump";
	private const int BLINK_UNITS = 3;
	private const int GRID_MIN_COORD = 0;
	
	private GameObject heroObj;
	private Map map;
	private GridSystem gridSystem;
	private CharacterMovement characterMovement;
	private float singleGridSize;
	private float blinkDistance;
	private Vector3 blinkPos;
	private bool blinked;

	// Use this for initialization
	void Start () {
		GameObject mapObj = GameObject.Find ("Map");
		map = mapObj.GetComponent<Map>();
		gridSystem = mapObj.GetComponent<GridSystem>();
		characterMovement = heroObj.GetComponent<CharacterMovement>();
		
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
			Vector3 lastPos = heroObj.transform.position;
			Vector3 blinkDirection = characterMovement.getAimDirection();
			
			// makes the 
			if (blinkDirection.x > 0)
				blinkDirection.x = 1;
			if (blinkDirection.x < 0)
				blinkDirection.x = -1;
			if (blinkDirection.z > 0)
				blinkDirection.z = 1;
			if (blinkDirection.z  < 0)
				blinkDirection.z = -1;
			
			newPos += blinkDirection * blinkDistance;  // blink to facing direction
			
			// check world bound with respect to x-direction
			if (newPos.x >= gridSystem.getXCoord(gridSystem.getGridWidth()) - 1)
				newPos.x = gridSystem.getXCoord(gridSystem.getGridWidth() - 2);  // clamp at bound
			if (newPos.x <= gridSystem.getXCoord(GRID_MIN_COORD))
				newPos.x = gridSystem.getXCoord(GRID_MIN_COORD + 1);
			// check world bound with repect to y-direction
			if (newPos.z >= gridSystem.getYCoord(gridSystem.getGridHeight()) - 1)
				newPos.z = gridSystem.getYCoord(gridSystem.getGridHeight() - 2);  // clamp at bound
			if (newPos.z <= gridSystem.getYCoord(GRID_MIN_COORD))
				newPos.z = gridSystem.getYCoord(GRID_MIN_COORD + 1);
			
			if (map.isGridFull (newPos.x, newPos.z)) {  // check whether the grid has bomb or wall or not
				if (newPos.x == lastPos.x && newPos.z > lastPos.z)  // blink towards top
					newPos.z = gridSystem.getYCoord(gridSystem.getYPos (newPos.z) - 1);
				if (newPos.x == lastPos.x && newPos.z < lastPos.z)  // blink towards bottom
					newPos.z = gridSystem.getYCoord(gridSystem.getYPos (newPos.z) + 1);
				if (newPos.x < lastPos.x && newPos.z == lastPos.z)  // blink towards left
					newPos.x = gridSystem.getXCoord(gridSystem.getXPos (newPos.x) + 1);
				if (newPos.x > lastPos.x && newPos.z == lastPos.z)  // blink towards right
					newPos.x = gridSystem.getXCoord(gridSystem.getXPos (newPos.x) - 1);
				newPos.x = gridSystem.getXCoord(gridSystem.getXPos(newPos.x));
				newPos.z = gridSystem.getYCoord(gridSystem.getYPos(newPos.z));
			}
			heroObj.transform.position = newPos;  // teleport to facing direction
		}
	}
	
	public void SetGameObject(GameObject obj) {
		heroObj = obj;
	}
}