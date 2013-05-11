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
	
	public bool[,] grid;

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
				
		grid = new bool[gridWidth, gridHeight];
		
		// initialize grid array
		for (int x = 0; x < gridWidth; x++)
			for (int y = 0; y < gridHeight; y++)
				grid[x,y] = false;
		
		
		
		if(isLevelOne == true) {
			
			for(int i = 0; i <= 17; i++) {
				
				if( i == 4 || i == 6 || i == 8 || i == 10 || i == 12 || i == 14) {
					for( int m = 1; m <= 11; m+=2) {
						GameObject go = Instantiate(DestructubleWall) as GameObject;
						IndestructubleWall wall = go.GetComponent<IndestructubleWall>();
						wall.initialize(getXCoord(i), getYCoord(m));
						
						grid[i,m] = true;
					}
				}
				
				if( i == 5 || i == 7 || i == 11 || i == 13) {
					for( int m = 1; m <= 11; m++) {
						GameObject go = Instantiate(DestructubleWall) as GameObject;
						IndestructubleWall wall = go.GetComponent<IndestructubleWall>();
						wall.initialize(getXCoord(i), getYCoord(m));
						
						grid[i,m] = true;
					}
				}
				
				if( i == 1 || i == 17) {
					for( int m = 4; m <= 8; m++) {
						GameObject go = Instantiate(DestructubleWall) as GameObject;
						IndestructubleWall wall = go.GetComponent<IndestructubleWall>();
						wall.initialize(getXCoord(i), getYCoord(m));
						
						grid[i,m] = true;
					}
				}
				
				if( i == 9 ) {
					for( int m = 1; m <= 4; m++) {
						GameObject go = Instantiate(DestructubleWall) as GameObject;
						IndestructubleWall wall = go.GetComponent<IndestructubleWall>();
						wall.initialize(getXCoord(i), getYCoord(m));
						
						grid[i,m] = true;
					}
					
					for( int m = 6; m <= 11; m++) {
						GameObject go = Instantiate(DestructubleWall) as GameObject;
						IndestructubleWall wall = go.GetComponent<IndestructubleWall>();
						wall.initialize(getXCoord(i), getYCoord(m));
						
						grid[i,m] = true;
					}
				}
				
				if( i == 3 || i == 15 ) {
					for( int m = 2; m <= 10; m++) {
						GameObject go = Instantiate(DestructubleWall) as GameObject;
						IndestructubleWall wall = go.GetComponent<IndestructubleWall>();
						wall.initialize(getXCoord(i), getYCoord(m));
						
						grid[i,m] = true;
					}
				}
			}
		}
		
		for (int x = 0; x < gridWidth; x++) {
			
			for (int y = 0; y < gridHeight; y++) {
				
				if (isEdge(x, y) || (x % 2 == 0 && y % 2 == 0)) {
					
					GameObject go = Instantiate(mIndestructubleWall) as GameObject;
					IndestructubleWall wall = go.GetComponent<IndestructubleWall>();
					
					wall.initialize(getXCoord(x), getYCoord(y));
					
					grid[x,y] = true;
				}
			}
		}
	}
	
	// converts grid x-coord into x-position
	public float getXCoord(int x) { return xMin + (x * wallSize) + (wallSize / 2); }
	
	// converts grid y-coord into y-position
	public float getYCoord(int y) { return yMin + (y * wallSize) + (wallSize / 2); }
	
	// converts x-position to grid x-coord
	public int getXPos(float x) { return Mathf.RoundToInt( (x - xMin - (wallSize / 2) ) / wallSize); }
	
	// converts y-position to grid y-coord
	public int getYPos(float y) { return Mathf.RoundToInt( (y - yMin - (wallSize / 2) ) / wallSize); }
	
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
