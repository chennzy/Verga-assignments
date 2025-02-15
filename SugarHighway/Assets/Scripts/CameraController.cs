using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    //public 
    public Transform target; // The ball (player) to follow
    //public Vector3 offset = new Vector3(0, 5, -10); // Default camera offset
    public float smoothSpeed = 5f; // Speed of smoothing

    void LateUpdate()
    {
        if (target == null) return; // Ensure there's a target before following

        // Compute desired position based on target position + offset
        Vector3 desiredPosition = target.position;// + offset;

        // Smoothly interpolate between current and desired position
       // t.Translate = (desiredPosition.x, desiredPosition.y, desiredPosition.z);
    }
}