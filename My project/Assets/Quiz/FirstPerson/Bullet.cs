using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float speed = 10f;  // Speed of the bullet

    [SerializeField]
    float lifetime = 1f;  // Time before the bullet is destroyed

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Get Rigidbody

        // Set velocity for constant bullet movement
        rb.linearVelocity = transform.forward * speed;

        // Destroy bullet after a certain time
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);  // Destroy bullet on impact
    }
}
