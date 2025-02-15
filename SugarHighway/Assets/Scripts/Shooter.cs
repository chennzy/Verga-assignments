using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;  // Reference to the bullet prefab
    public Transform gunBarrel;      // Position where the bullet is spawned
    public float fireRate = 0.5f;    // How fast the bullet can be fired
    private float nextFire = 0f;     // Time to wait before firing again

    void Update()
    {
        // Check if the time has passed since the last shot and if the fire button is pressed
        if (Input.GetButton("Fire") && Time.time >= nextFire)  // "Fire1" is the default input for the left mouse button or Ctrl key
        {
            ShootBullet();
            nextFire = Time.time + fireRate; // Set the time for the next fire
        }
    }

    void ShootBullet()
    {
        // Instantiate the bullet at the gun's position and rotation
        Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
    }
}
