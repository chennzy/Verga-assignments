using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;  // Drag bullet prefab here
    public Transform firePoint;  // Drag FirePoint object here
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
