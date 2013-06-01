using UnityEngine;
using System.Collections;

public class GridCollision : MonoBehaviour {
	
	private float playerHeight;
	private float playerWidth;
	
	private GridSystem gridSystem;
	private Map map;
	
	void Start() {
		playerWidth = transform.localScale.x / 2f;
		playerHeight = transform.localScale.z / 2f;
		gridSystem = GameObject.Find ("Map").GetComponent<GridSystem>();
		map = GameObject.Find ("Map").GetComponent<Map>();
	}
	
	public bool isCollidingX(float movementX) {
		if (movementX == 0) {
			return false;
		}

		Vector2 gridLocation = new Vector2(transform.position.x + movementX, transform.position.z);
			
		if (movementX > 0) {			
			gridLocation.x += playerWidth;	
		}
		if (movementX < 0) {
			gridLocation.x -= playerWidth;
		}

		if (map.isGridFull(gridLocation.x, gridLocation.y + (playerHeight - 0.1f))
		|| map.isGridFull(gridLocation.x, gridLocation.y - (playerHeight - 0.1f))
		|| map.isGridFull(gridLocation.x, gridLocation.y))
			return true;
		
		return false;
	}
	
	public bool isCollidingY(float movementY) {
		if (movementY == 0) {
			return false;
		}

		Vector2 gridLocation = new Vector2(transform.position.x, transform.position.z + movementY);
		
		if (movementY > 0) {
			gridLocation.y += playerHeight;
		}
		if (movementY < 0) {
			gridLocation.y -= playerHeight;
		}
		
		if (map.isGridFull(gridLocation.x + (playerWidth - 0.1f), gridLocation.y)
		|| map.isGridFull(gridLocation.x - (playerWidth - 0.1f), gridLocation.y)
		|| map.isGridFull(gridLocation.x, gridLocation.y))
			return true;
		
		return false;
	}
}
