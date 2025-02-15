using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;  // Speed of the bullet

    void Start()
    {
        // Destroy the bullet after a certain amount of time
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // Move the bullet forward based on its speed
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // Add collision detection if you want the bullet to interact with objects
    void OnCollisionEnter(Collision collision)
    {
        // Destroy bullet on collision
        Destroy(gameObject);
    }
}
