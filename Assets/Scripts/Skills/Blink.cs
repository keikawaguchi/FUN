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

	// Use this for initialization
	void Start () {
		heroObj = GameObject.Find ("Hero");
		GameObject mapObj = GameObject.Find ("Map");
		map = mapObj.GetComponent<Map>();
		gridSystem = mapObj.GetComponent<GridSystem>();
		characterMovement = heroObj.GetComponent<CharacterMovement>();
		
		singleGridSize = gridSystem.getSingleGridWidth();
		blinkDistance = singleGridSize * BLINK_UNITS;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		 * Conditions:
		 * 1. Out of bound
		 * 2. Jump onto walls
		 * 3. Jump onto bombs
		 */
		
		if (Input.GetButtonDown(BLINK_BUTTON)) {
			Vector3 newPos = heroObj.transform.position;
			Vector3 lastPos = heroObj.transform.position;
			Vector3 blinkDirection = characterMovement.getAimDirection();
			if (blinkDirection == Vector3.zero)
				blinkDirection = Vector3.forward;  // (0, 0, 1)
			
			newPos += blinkDirection * blinkDistance;  // blink to facing direction
			
			// check world bound with respect to x-direction
			if (newPos.x >= gridSystem.getXCoord(gridSystem.getGridWidth()))
				newPos.x = gridSystem.getXCoord(gridSystem.getGridWidth() - 1);  // clamp at bound
			if (newPos.x <= gridSystem.getXCoord(GRID_MIN_COORD))
				newPos.x = gridSystem.getXCoord(GRID_MIN_COORD + 1);
			// check world bound with repect to y-direction
			if (newPos.z >= gridSystem.getYCoord(gridSystem.getGridHeight()) - 1)
//			if (gridSystem.getYPos (newPos.z) >= gridSystem.getGridHeight())
				newPos.z = gridSystem.getYCoord(gridSystem.getGridHeight() - 2);  // clamp at bound
			if (newPos.z <= gridSystem.getYCoord(GRID_MIN_COORD))
				newPos.z = gridSystem.getYCoord(GRID_MIN_COORD + 1);
			Debug.Log ("Pos: " + newPos);
			Debug.Log("Grid Height: " + gridSystem.getYCoord (gridSystem.getGridHeight()));
//			Debug.Log ("New Pos YCoord: " + gridSystem.getYPos (newPos.z));
//			Debug.Log("Grid Height: " + gridSystem.getGridHeight());
			
			if (map.isGridFull (newPos.x, newPos.z)) {  // check whether the grid has bomb or wall or not
				if (newPos.x == lastPos.x && newPos.z > lastPos.z)  // blink towards top
					newPos.z = gridSystem.getYCoord(gridSystem.getYPos (newPos.z) - 1);
				if (newPos.x == lastPos.x && newPos.z < lastPos.z)  // blink towards bottom
					newPos.z = gridSystem.getYCoord(gridSystem.getYPos (newPos.z) + 1);
				if (newPos.x < lastPos.x && newPos.z == lastPos.z)  // blink towards left
					newPos.x = gridSystem.getXCoord(gridSystem.getXPos (newPos.x) + 1);
				if (newPos.x > lastPos.x && newPos.z == lastPos.z)  // blink towards right
					newPos.x = gridSystem.getXCoord(gridSystem.getXPos (newPos.x) - 1);
			}
			heroObj.transform.position = newPos;  // teleport to facing direction
		}
	}
}