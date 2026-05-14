using UnityEngine;


public class Camera2D : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    public Vector3 offset = new Vector3(0f, 0f, 0f);

    Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        //Debug.Log(target); 
        if (target == null) return;

        Vector3 targetposition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetposition, ref velocity, smoothTime);
    }
}
