using UnityEngine;
using System.Collections;

public class DestructibleWall : MonoBehaviour {
	
	private Map map;
	private GridSystem gridSystem;
	
	// Use this for initialization
	void Start () {
		loadScripts();
	}
	
	private void loadScripts() {
		map = GameObject.Find("Map").GetComponent<Map>();
		gridSystem = GameObject.Find("Map").GetComponent<GridSystem>();
	}
	
	public void initialize(float x, float z) {
		transform.position = new Vector3(x, 0, z);
	}
}
