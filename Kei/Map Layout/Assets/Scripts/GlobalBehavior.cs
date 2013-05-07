using UnityEngine;
using System.Collections;

public class GlobalBehavior : MonoBehaviour {
	
	private Bounds mWorldBound;  // this is the world bound
	private Camera mMainCamera;
	
	// spwaning enemy ...
	public GameObject mIndestructubleWall = null;
	
	private float xMin;						// lower left hand corner x-pos
	private float yMin;						// lower left hand corner y-pos
	
	private const float wallSize = 14f;	// dimension of wall
	
	private const int gridWidth = 19;		// width of map
	private const int gridHeight = 13;		// height of map

	// Use this for initialization
	void Start () {
		mMainCamera = Camera.main;
		mWorldBound = new Bounds(Vector3.zero, Vector3.one);
		UpdateWorldWindowBound();
		
		// calculate lower left hand corner of screen
		xMin = mMainCamera.orthographicSize * mMainCamera.aspect * -1;
		yMin = mMainCamera.orthographicSize * -1;
		
		// initialize map layout
		if (null == mIndestructubleWall) 
			mIndestructubleWall = Resources.Load("Prefabs/Indestructuble Wall") as GameObject;
		
		initializeMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void initializeMap() {
		var grid = new bool[gridWidth, gridHeight];
		
		for (int x = 0; x < gridWidth; x++) {
			
			for (int y = 0; y < gridHeight; y++) {
				
				if (isEdge(x, y) || (x % 2 == 0 && y % 2 == 0)) {
					
					GameObject go = Instantiate(mIndestructubleWall) as GameObject;
					IndestructubleWall wall = go.GetComponent<IndestructubleWall>();

					wall.initialize(getXCoord(x), getYCoord(y));
				}
			}
		}
	}
	
	// converts grid x-coord into x-position
	float getXCoord(int x) { return xMin + (x * wallSize) + (wallSize / 2); }
	
	// converts g y-coord into y-position
	float getYCoord(int y) { return yMin + (y * wallSize) + (wallSize / 2); }
	
	// returns true if coordinate is an edge of the map
	bool isEdge(int x, int y) {
		
		// left or bottom edge
		if (x == 0 || y == 0)
			return true;
		
		// right or top edge
		if (x == (gridWidth - 1) || y == (gridHeight - 1))
			return true;
		
		return false;
	}
	
	public void UpdateWorldWindowBound() {
		// get the main 
		if (null != mMainCamera) {
			float maxZ = mMainCamera.orthographicSize;
			float maxX = mMainCamera.orthographicSize * mMainCamera.aspect;
			
			float sizeX = 2 * maxX;
			float sizeZ = 2 * maxZ;
			float sizeY = Mathf.Abs(mMainCamera.farClipPlane - mMainCamera.nearClipPlane);
			
			// assumes camera is looking in the negative y-axis
			Vector3 c = mMainCamera.transform.position;
			c.y -= (0.5f * sizeY);
			mWorldBound.center = c;
			mWorldBound.size = new Vector3(sizeX, sizeY, sizeZ);
			
			// DELETE
			Debug.Log("Aspect Ratio: " + mMainCamera.aspect);
			Debug.Log("X: " + sizeX);
			Debug.Log("Y: " + sizeY);
			Debug.Log("Z: " + sizeZ);
		}
	}
	
	public Bounds WorldBound { get { return mWorldBound; } }
}
