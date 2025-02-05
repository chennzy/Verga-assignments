using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RollABall : MonoBehaviour
{
    Rigidbody rb;
    Vector2 m;

    void Start()
    {
        m = new Vector2(0, 0);
        rb = GetComponent<Rigidbody>();

    }
    void Update()
    {
        float x_dir = m.x;
        float z_dir = m.y;
        Vector3 actual_movement = new Vector3(x_dir, 0, z_dir);
        rb.AddForce(actual_movement);
    }
    void OnMove(InputValue movement)
    {
        m = movement.Get<Vector2>();
    }
}
/*public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/