// sprite sheet animation script
// original script source: 
//		http://wiki.unity3d.com/index.php?title=Animating_Tiled_texture
// modified by:
//		385 Studios


using UnityEngine;
using System.Collections;

public class SpriteSheetNG : MonoBehaviour
{	
    private float iX=0;
    private float iY=1;
    public int _columns = 1;
    public int _rows = 1;
    public int _fps = 10;
	public bool _destroyAfterAnimation = true;
    private Vector2 _size;
    private Renderer _myRenderer;
    private int _lastIndex = -1;
 
    void Start ()
    {
        _size = new Vector2 (1.0f / _columns ,
                             1.0f / _rows);
 
        _myRenderer = renderer;
 
        if(_myRenderer == null) enabled = false;
 
        _myRenderer.material.SetTextureScale ("_MainTex", _size);
    }
 
 
 
    void Update()
    {	
        int index = (int)(Time.timeSinceLevelLoad * _fps) % (_columns * _rows);
 
        if(index != _lastIndex)
        {
            Vector2 offset = new Vector2(iX*_size.x, 1-(_size.y*iY));
			
			// move to next column
            iX++;
			
			// if we reach the end of animation destory animation obj
			if (iX / _columns == 1 && iY / _rows == 1) {
				if (_destroyAfterAnimation) {
					Destroy(gameObject);
				}
			}
			
			// move to next row if we reach end of column
            if(iX / _columns == 1)
            {
                if(_rows!=1) {
					iY++;
				}
				
                iX=0;
				
                if(iY / _rows == 1)
                {
                    iY=1;
                }
            }
 
            _myRenderer.material.SetTextureOffset ("_MainTex", offset);
 
            _lastIndex = index;
        }
    }
}