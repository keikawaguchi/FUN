using System.Collections;
using UnityEngine;

// a copy of GridMovement
class Enemy : MonoBehaviour {
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
	
	void Start() {
		// DELETE, temp start position at [1,1]
		transform.position = new Vector3(56.3f, 0, -79);
	}
 
    public void Update() {
        
    }
};