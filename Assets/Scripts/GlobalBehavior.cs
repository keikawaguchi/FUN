using UnityEngine;
using System.Collections;

public class GlobalBehavior : MonoBehaviour {
	
	public bool isLevelOne = false;
	public bool isLevelTwo = false;
	
	private Bounds mWorldBound;  // this is the world bound
	private Camera mMainCamera;
	
	// spwaning enemy ...
	public GameObject mIndestructubleWall = null;
	public GameObject DestructubleWall = null;
	
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
		if(null == DestructubleWall)
			DestructubleWall = Resources.Load("Prefabs/Destructuble Wall") as GameObject;
		
		initializeMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// creates the basic map layout with the indestructuble walls
	void initializeMap() {
				
		var grid = new bool[gridWidth, gridHeight];
		if(isLevelOne == true)
		{
			GameObject nogo = Instantiate(DestructubleWall) as GameObject;
			IndestructubleWall daWall = nogo.GetComponent<IndestructubleWall>();
			daWall.initialize(getXCoord(3), getYCoord(3));
			GameObject wall2 = Instantiate(DestructubleWall) as GameObject;
			IndestructubleWall daWall2 = wall2.GetComponent<IndestructubleWall>();
			daWall2.initialize(getXCoord(3), getYCoord(4));
			GameObject wall3 = Instantiate(DestructubleWall) as GameObject;
			IndestructubleWall daWall3 = wall3.GetComponent<IndestructubleWall>();
			daWall3.initialize(getXCoord(3), getYCoord(2));
		}
		
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
	public float getXCoord(int x) { return xMin + (x * wallSize) + (wallSize / 2); }
	
	// converts grid y-coord into y-position
	public float getYCoord(int y) { return yMin + (y * wallSize) + (wallSize / 2); }
	
	// converts x-position to grid x-coord
	int getXPos(float x) { return Mathf.RoundToInt( (x - xMin - (wallSize / 2) ) / wallSize); }
	
	// converts y-position to grid y-coord
	int getYPos(float y) { return Mathf.RoundToInt( (y - yMin - (wallSize / 2) ) / wallSize); }
	
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
		}
	}
	
	public Bounds WorldBound { get { return mWorldBound; } }
}
