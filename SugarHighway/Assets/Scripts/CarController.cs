using UnityEngine;
public class CarController : MonoBehaviour
{
    public float acceleration = 500f;
    public float brakeForce = 300f;
    public float maxSpeed = 50f;
    public float turnSpeed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.centerOfMass = new Vector3(0, -0.5f, 0);  // Lower center of mass for stability
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleSteering();
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxis("Vertical");  // W/S or Up/Down keys
        if (moveInput != 0)
        {
            rb.AddForce(transform.forward * moveInput * acceleration * Time.deltaTime, ForceMode.Acceleration);
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
        }

        if (Input.GetKey(KeyCode.Space))  // Braking
        {
            rb.AddForce(-transform.forward * brakeForce * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    void HandleSteering()
    {
        float turnInput = Input.GetAxis("Horizontal");  // A/D or Left/Right keys
        if (turnInput != 0)
        {
            Vector3 turn = new Vector3(0f, turnInput * turnSpeed * Time.deltaTime * Mathf.Sign(rb.linearVelocity.z), 0f);
            transform.Rotate(turn);
        }
    }
}
