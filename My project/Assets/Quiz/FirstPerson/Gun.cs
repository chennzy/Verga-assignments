using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign in Inspector
    public Transform firePoint; // Create an empty GameObject at gun barrel
    public float bulletForce = 20f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Left mouse button
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
    }
}
