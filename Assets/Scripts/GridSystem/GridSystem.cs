using UnityEngine;
using System.Collections;

public class GridSystem : MonoBehaviour {
	
	private Bounds mWorldBound;  		// this is the world bound
	private Camera mMainCamera;
	
	private float xMin;					// lower left hand corner x-pos
	private float yMin;					// lower left hand corner y-pos
	
	private const float singleGridSize = 14f;	// dimension of wall
	
	private const int gridWidth = 19;	// width of map
	private const int gridHeight = 13;	// height of map
	

	void Start () {
		mMainCamera = Camera.main;
		mWorldBound = new Bounds(Vector3.zero, Vector3.one);
		UpdateWorldWindowBound();
		
		// calculate lower left hand corner of screen
		xMin = mMainCamera.orthographicSize * mMainCamera.aspect * -1;
		yMin = mMainCamera.orthographicSize * -1;	
	}
	
	public int getGridWidth() {
		return gridWidth;
	}
	
	public int getGridHeight() {
		return gridHeight;
	}
	
	public float getSingleGridWidth() {
		return singleGridSize;
	}
	
	public float getSingleGridHeight() {
		return singleGridSize;
	}
	
	// converts grid x-coord into x-position
	public float getXCoord(int x) { return xMin + (x * singleGridSize) + (singleGridSize / 2); }
	
	// converts grid y-coord into y-position
	public float getYCoord(int y) { return yMin + (y * singleGridSize) + (singleGridSize / 2); }
	
	// converts x-position to grid x-coord
	public int getXPos(float x) { return Mathf.RoundToInt( (x - xMin - (singleGridSize / 2) ) / singleGridSize); }
	
	// converts y-position to grid y-coord
	public int getYPos(float y) { return Mathf.RoundToInt( (y - yMin - (singleGridSize / 2) ) / singleGridSize); }
	
	public Vector3 gridToVector(int x, int y) {
		Vector3 toReturn = new Vector3(0, 0, 0);
		
		toReturn.x = getXCoord(x);
		toReturn.z = getYCoord(y);
		
		return toReturn;
	}
	
	// returns true if coordinate is an edge of the map
	public bool isEdge(int x, int y) {
		
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
