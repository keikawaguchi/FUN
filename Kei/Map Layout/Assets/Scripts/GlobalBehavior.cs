using UnityEngine;
using System.Collections;

public class GlobalBehavior : MonoBehaviour {
	
	private Bounds mWorldBound;  // this is the world bound
	private Camera mMainCamera;
	
		// spwaning enemy ...
	public GameObject mIndestructubleWall = null;
	
	private const int width = 15;
	private const int height = 13;
	private const float wallHeight = 12.5f;
	private const float wallWidth = 15.0f;

	// Use this for initialization
	void Start () {
		mMainCamera = Camera.main;
		mWorldBound = new Bounds(Vector3.zero, Vector3.one);
		UpdateWorldWindowBound();
		
		// initialize map layout
		if (null == mIndestructubleWall) 
			mIndestructubleWall = Resources.Load("Prefabs/Indestructuble Wall") as GameObject;
		
		initializeMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void initializeMap() {
		
		/*
		 *  Walls within the map
		 */
		float minZ = mMainCamera.orthographicSize * -1 + 6.25f;
		float minX = mMainCamera.orthographicSize * mMainCamera.aspect * -1 + 7.5f;
		
		float tempZ = minZ + wallHeight / 2;
		float tempX = minX + wallWidth / 2;
		
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				
				// Create indestructuble wall
				if (x % 2 == 1 && y % 2 == 1) {
					GameObject go = Instantiate(mIndestructubleWall) as GameObject;
					IndestructubleWall wall = go.GetComponent<IndestructubleWall>();
					
					wall.initialize(tempX, tempZ);
				}
				tempX += wallWidth;
			}
			tempX = minX + wallWidth / 2;
			tempZ += wallHeight;
		}
		
		
		/*
		 * Walls outlining the map
		 */
		float maxZ = mMainCamera.orthographicSize;
		float maxX = mMainCamera.orthographicSize * mMainCamera.aspect;
		float temp = maxZ * -1;
		
		// initialize left and right sides
		for (int i = 0; i < height + 2; i++) {
			GameObject goLeft = Instantiate(mIndestructubleWall) as GameObject;
			IndestructubleWall leftWall = goLeft.GetComponent<IndestructubleWall>();
			
			GameObject goRight = Instantiate(mIndestructubleWall) as GameObject;
			IndestructubleWall rightWall = goRight.GetComponent<IndestructubleWall>();
			
			leftWall.initialize(maxX * -1, temp);
			rightWall.initialize(wallWidth * width / 2 - 5.0f, temp);
			
			temp += wallHeight;
		}
		
		// initialize top and bottom sides
		temp = maxX * -1 + 7.5f;
		
		for (int i = 0; i <= width ; i++) {
			GameObject goTop = Instantiate(mIndestructubleWall) as GameObject;
			IndestructubleWall topWall = goTop.GetComponent<IndestructubleWall>();
			
			GameObject goBottom = Instantiate(mIndestructubleWall) as GameObject;
			IndestructubleWall bottomWall = goBottom.GetComponent<IndestructubleWall>();
			
			topWall.initialize(temp, wallHeight * height / 2 - 6.25f);
			bottomWall.initialize(temp, maxZ * -1);
			
			temp += wallWidth;
		}
		
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
			
			Debug.Log("Aspect Ratio: " + mMainCamera.aspect);
			Debug.Log("X: " + sizeX);
			Debug.Log("Y: " + sizeY);
			Debug.Log("Z: " + sizeZ);
		}
	}
	
	public Bounds WorldBound { get { return mWorldBound; } }
}
