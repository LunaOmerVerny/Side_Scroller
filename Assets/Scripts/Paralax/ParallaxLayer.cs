using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
   public float speedX = 0.5f;
   public float speedY = 0.2f;

    private Transform _transform;
    private Vector3 _targetposition;

    private SpriteRenderer _sprite;
    private float _spriteWidth;
    private bool _infiniteX;

    public ParallaxLayer(Transform t)
    {
        //Debug.Log("ParallaxLayer created for " + t.name);
        _transform = t;
        _targetposition = t.position;

        var settings = t.GetComponent<ParallaxLayerSettings>();
        
        if (settings != null)
        {
            //Debug.Log(settings.speedX);
            speedX = settings.speedX;
            speedY = settings.speedY;
        }
    }

    public void Move (Vector3 delta, bool vertical, float smoothing)
    {
        float moveX = delta.x * (1f - speedX);
        float moveY = vertical ? delta.y * (1f - speedY) : 0f;

        _targetposition += new Vector3(moveX, moveY, 0f);
        _transform.position = smoothing > 0f ? Vector3.Lerp(_transform.position, _targetposition, smoothing): _targetposition;
         
        if (_infiniteX)
        {
            WrapHorizontal();  
        }
    }

    private void WrapHorizontal()
    {
        float camX = Camera.main.transform.position.x;
        float diffX = camX - _transform.position.x;

        if (Mathf.Abs(diffX) >= _spriteWidth)
        {
            float offsetX = diffX > 0 ? _spriteWidth : -_spriteWidth;
            _transform.position += new Vector3(offsetX, 0f, 0f);
        }
        
    }
}
