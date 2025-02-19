using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    float speed = 10f;  // Speed of the bullet
    [SerializeField]
    float lifetime = 1f;  // Time in seconds before the bullet is destroyed

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Get the Rigidbody component

        // Set the velocity to move the bullet in the direction it's facing
        rb.linearVelocity = transform.forward * speed;  // Directly set the velocity for constant speed

        // Destroy the bullet after the specified lifetime
        Destroy(gameObject, lifetime);
    }

    

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);  
    }
}
