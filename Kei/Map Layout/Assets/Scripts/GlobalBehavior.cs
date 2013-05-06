using UnityEngine;
using System.Collections;

public class GlobalBehavior : MonoBehaviour {
	
	private Bounds mWorldBound;  // this is the world bound
	private Camera mMainCamera;
	
		// spwaning enemy ...
	public GameObject mIndestructubleWall = null;
	
	private const float wallSize = 12.5f;

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
		float maxZ = mMainCamera.orthographicSize;
		float maxX = mMainCamera.orthographicSize * mMainCamera.aspect;
		
		float temp = maxZ * -1;
		
		// initialize left and right sides
		while(temp < maxZ) {
			GameObject goLeft = Instantiate(mIndestructubleWall) as GameObject;
			IndestructubleWall leftWall = goLeft.GetComponent<IndestructubleWall>();
			
			GameObject goRight = Instantiate(mIndestructubleWall) as GameObject;
			IndestructubleWall rightWall = goRight.GetComponent<IndestructubleWall>();
			
			leftWall.initialize(maxX * -1, temp);
			rightWall.initialize(maxX, temp);
			
			temp += wallSize;
		}
		
		// initialize top and bottom sides
		temp = maxX * -1 + 6.25f;
		
		while (temp < maxX) {
			GameObject goTop = Instantiate(mIndestructubleWall) as GameObject;
			IndestructubleWall topWall = goTop.GetComponent<IndestructubleWall>();
			
			GameObject goBottom = Instantiate(mIndestructubleWall) as GameObject;
			IndestructubleWall bottomWall = goBottom.GetComponent<IndestructubleWall>();
			
			topWall.initialize(temp, maxZ);
			bottomWall.initialize(temp, maxZ * -1);
			
			temp += wallSize;
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
