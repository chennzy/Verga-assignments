using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RollABallPlayer : MonoBehaviour
{
    public float jumpForce = 500;
    bool isGrounded;
    bool isNearGround;
    Vector2 m;
    Rigidbody rb;
    public Transform cameraPos; // This helps the player's input be relative to where the camera is facing.
    public float speed = 10f; 
    public float maxSpeed = 50f;
    public float groundCheckDistance = 0.1f;
    public float nearGroundDistance = 0.75f;

    public float detectionRadius = 5f; // How far to check for enemies
    public LayerMask enemyLayer; // To find the "enemyies" when I explode.


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m = new Vector2(0, 0);
    }
    
    void FixedUpdate()
    {
        MoveBallRelativeToCamera();

        /*
        Vector3 movement = new Vector3(m.x, 0, m.y) * speed;

        // Apply force with VelocityChange mode for more responsive movement
        rb.AddForce(movement, ForceMode.VelocityChange);

        // Limit maximum velocity
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame) // Change "e" to whatever key you want
        {
            CheckForEnemies();
        }
        CheckIfGrounded();

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
        }
        /*float x_dir = m.x; 
        float z_dir = m.y;
        Vector3 actualMovement = new Vector3(x_dir, 0, z_dir);
        print(actualMovement);
        rb.AddForce(actualMovement);*/
    }
    void MoveBallRelativeToCamera()
    {
        if (cameraPos == null) return; // Ensure the camera is assigned

        // Get camera's forward and right directions (ignore Y so movement is flat)
        Vector3 cameraForward = cameraPos.forward;
        Vector3 cameraRight = cameraPos.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Convert input into world direction
        Vector3 moveDirection = (cameraForward * m.y + cameraRight * m.x).normalized;

        // Apply force in that direction
        rb.AddForce(moveDirection * speed, ForceMode.VelocityChange);

        // Limit max speed for better control
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    void OnMove(InputValue movement)
    {
        m = movement.Get<Vector2>(); 
    }
    // Uses a OverlapSphere to detect if enemies are within range of ball/player.
    void CheckForEnemies()
    {
        float randomExplosivePower = UnityEngine.Random.Range(50f, 500f);
        // This creates a list of confirmed enemies found within the set sphere.
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        if (hitColliders.Length > 0)
        {
            Debug.Log("Enemies detected: " + hitColliders.Length);
            foreach (Collider enemy in hitColliders)
            {
                Debug.Log("Enemy in range: " + enemy.name);
                enemy.GetComponent<Rigidbody>().AddExplosionForce(randomExplosivePower, transform.position,10f,2.5f,ForceMode.Impulse);
            }
        }
        else
        {
            Debug.Log("No enemies in range.");
        }
    }

    void CheckIfGrounded()
    {
        // Replace this with your actual ground check logic
        //isGrounded = true;
        // Ground check
        Vector3 spherePosition = transform.position + Vector3.down * groundCheckDistance;
        isGrounded = Physics.SphereCast(spherePosition, 0.1f, Vector3.down, out RaycastHit hitInfo, groundCheckDistance);

        // Near ground check
        spherePosition = transform.position + Vector3.down * nearGroundDistance;
        isNearGround = Physics.SphereCast(spherePosition, 0.1f, Vector3.down, out hitInfo, nearGroundDistance);
    }
}
