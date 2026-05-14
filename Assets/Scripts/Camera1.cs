using UnityEngine;

public class Camera1 : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    public Vector3 offset = new Vector3(0, 0, 0);
    private Vector3 velocity = Vector3.zero;

    
    void LatedUpdate()
    {
        if (target == null) return;

        Vector3 targetposition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetposition, ref velocity, smoothTime);
    }
}
