using System.Collections;
using UnityEngine;
 
class GridMove : MonoBehaviour {
    private float moveSpeed = 50f;
    private float gridSize = 14f;
    private enum Orientation {
        Horizontal,
        Vertical
    };
    private Orientation gridOrientation = Orientation.Horizontal;
    private bool allowDiagonals = false;
    private bool correctDiagonalSpeed = true;
    private Vector2 input;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor;
	private GlobalBehavior globalBehavior;
	
	void Start() {
		globalBehavior = GameObject.Find("Global Behavior").GetComponent<GlobalBehavior>();
		
		// CHANGE ME - should pass in 
		transform.position = new Vector3(-112.3f, 0, -79f);
	}
 
    public void Update() {
		bool toMove = true;
		
		if (isMoving)
			Debug.Log(transform.forward);
		
        if (!isMoving) {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (!allowDiagonals) {
				
				// check for which direction you're going
                if (Mathf.Abs(input.x) > Mathf.Abs(input.y)) {
					input.y = 0;
					
					// determine if hero is going left or right
					if (System.Math.Sign(input.x) > 0) {
						if ( isGridFull((transform.position.x + gridSize), transform.position.z) )
							toMove = false;
					} else {
						if ( isGridFull((transform.position.x - gridSize), transform.position.z) )
							toMove = false;
					}
                } else {
                    input.x = 0;
					
					// determine if hero is going up or down
					if (System.Math.Sign(input.y) > 0) {
						if ( isGridFull(transform.position.x, (transform.position.z + gridSize)) )
							toMove = false;
					} else {
						if ( isGridFull(transform.position.x, (transform.position.z - gridSize)) )
							toMove = false;
					}
					
                }
            }
 
            if (input != Vector2.zero && toMove) {
                StartCoroutine(move(transform));
            }
        }
    }
	
	public bool isGridFull(float x, float y) {
		int xCoord = globalBehavior.getXPos(x);
		int yCoord = globalBehavior.getYPos(y);
		
		return globalBehavior.grid[xCoord, yCoord];
	}
 
    public IEnumerator move(Transform transform) {
        isMoving = true;
        startPosition = transform.position;
        t = 0;
 
        if(gridOrientation == Orientation.Horizontal) {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y, startPosition.z + System.Math.Sign(input.y) * gridSize);
        } else {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
        }
 
        if(allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0) {
            factor = 0.7071f;
        } else {
            factor = 1f;
        }
 		
		// rotate hero to look at travel direction
		transform.LookAt(endPosition);
		
        while (t < 1f) {
            t += Time.deltaTime * (moveSpeed/gridSize) * factor;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }
 
        isMoving = false;
        yield return 0;
    }
};