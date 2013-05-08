using UnityEngine;
using System.Collections;

public class LSBehaviour : MonoBehaviour {

	private Camera MainCamera;
	Bounds WorldBound;
	public static LSBehaviour loveBehavior;
	GameObject Spawn;
	void Start () {
		MainCamera = Camera.main;
		WorldBound = new Bounds(Vector3.zero, Vector3.one);
		updateWorldBound();
		loveBehavior = this;
		
		if(Spawn == null)
		{
			Spawn = Resources.Load("Prefabs/Enemy") as GameObject;
		}
		LSEnemy.theHero = GameObject.Find("Hero");
		Object.Instantiate(Spawn);
	}
	
	// Update is called once per frame
	void Update () {
		}
	
	public void updateWorldBound()
	{
		if(MainCamera != null)
		{
			float size = MainCamera.orthographicSize;
			float dimension = MainCamera.orthographicSize * MainCamera.aspect;
			float x = 2f * dimension;
			float y = 2f * size;
			float z = Mathf.Abs((float)(MainCamera.farClipPlane - MainCamera.nearClipPlane));
			Vector3 position = MainCamera.transform.position;
			position.y -= 0.5f * y;
			WorldBound.center = position;
			WorldBound.size = new Vector3(x,y,z);
		}
	}
	
	public enum WorldBoundStatus //taken from class Example
    {
        CollideTop,
        CollideLeft,
        CollideRight,
        CollideBottom,
        Outside,
        Inside
    }
	
	public Bounds GetWorldBound
	{
		get
		{
			return WorldBound;
		}
	}
	
	public WorldBoundStatus ObjectCollideWorldBound(Bounds objBound)
    {
        WorldBoundStatus inside = WorldBoundStatus.Inside;
        if (this.WorldBound.Intersects(objBound))
        {
			if (objBound.min.x < this.WorldBound.min.x)
            {
                return WorldBoundStatus.CollideLeft;
            }
            if (objBound.max.x > this.WorldBound.max.x)
            {
                return WorldBoundStatus.CollideRight;
            }

            if (objBound.max.z > this.WorldBound.max.z)
            {
                return WorldBoundStatus.CollideTop;
            }
            if (objBound.min.z < this.WorldBound.min.z)
            {
                return WorldBoundStatus.CollideBottom;
            }
			
            if ((objBound.min.y >= this.WorldBound.min.y) && (objBound.max.y <= this.WorldBound.max.y))
            {
                return inside;
            }
            return WorldBoundStatus.Outside;
        }
        return WorldBoundStatus.Outside;
    }
	
	public WorldBoundStatus ClampToWorld(Transform param)
	{
		Vector3 pos = param.position;
		WorldBoundStatus retVal = WorldBoundStatus.Inside;
		
		if(pos.y < WorldBound.min.y || pos.y > WorldBound.max.y)
		{
			retVal = WorldBoundStatus.Outside;
		}
		
		if(pos.x < WorldBound.min.x)
		{
			pos.x = WorldBound.min.x;
			retVal = WorldBoundStatus.CollideLeft;
		}
		else if(pos.x > WorldBound.max.x)
		{
			pos.x = WorldBound.max.x;
			retVal = WorldBoundStatus.CollideRight;
		}
		
		if(pos.z < WorldBound.min.z)
		{
			pos.z = WorldBound.min.z;
			retVal = WorldBoundStatus.CollideBottom;
		}
		else if(pos.z > WorldBound.max.z)
		{
			pos.z = WorldBound.max.z;
			retVal = WorldBoundStatus.CollideTop;
		}
		
		param.position = pos;
		return retVal;
	}

}
