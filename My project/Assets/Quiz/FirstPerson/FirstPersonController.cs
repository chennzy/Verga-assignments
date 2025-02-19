using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField]
    float speed = 2.0f;
    [SerializeField]
    float mouseSensitivity = 100;
    [SerializeField]
    GameObject cam;
    [SerializeField]
    GameObject Bullet;
    [SerializeField]
    GameObject BulletSpawner;

    Vector2 movement;
    Vector2 mouseMovement;
    float cameraUpRotation = 0;
    CharacterController controller;

    public InputActionAsset playerControls;
    private InputAction shootAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the CharacterController component
        controller = GetComponent<CharacterController>();

        // Ensure playerControls is assigned
        if (playerControls == null)
        {
            Debug.LogError("PlayerControls is not assigned in the Inspector!");
            return;  // Exit early if there's an issue
        }

        // Try to find the "Shoot" action
        shootAction = playerControls.FindAction("Shoot");

        // Ensure shootAction is assigned
        if (shootAction == null)
        {
            //Debug.LogError("Shoot action is not found in the InputActionAsset! Make sure the action is named 'Shoot' and exists.");
            return;  // Exit early if the action is not found
        }

        // Subscribe to the performed event so OnAttack() is called only once per click.
        shootAction.performed += ctx => OnAttack();
        shootAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        float lookX = mouseMovement.x * Time.deltaTime * mouseSensitivity;
        float lookY = mouseMovement.y * Time.deltaTime * mouseSensitivity;

        cameraUpRotation -= lookY;
        cameraUpRotation = Mathf.Clamp(cameraUpRotation, -90, 90);

        cam.transform.localRotation = Quaternion.Euler(cameraUpRotation, 0, 0);
        transform.Rotate(Vector3.up * lookX);

        float moveX = movement.x;
        float moveY = movement.y;

        Vector3 forwardMovement = new Vector3(transform.forward.x, 0, transform.forward.z).normalized * moveY;
        Vector3 rightMovement = new Vector3(transform.right.x, 0, transform.right.z).normalized * moveX;

        Vector3 actual_moveset = forwardMovement + rightMovement;

        // APPLY GRAVITY **ONLY IF NOT GROUNDED**
        if (!controller.isGrounded)
        {
            actual_moveset.y -= 9.81f * Time.deltaTime;
        }
        else
        {
            actual_moveset.y = -0.1f; // Small downward force to keep the player grounded
        }

        /*Debug.Log("Player Y Position: " + transform.position.y);*/
        /*Debug.Log("Is Grounded: " + controller.isGrounded);*/

        controller.Move(actual_moveset * Time.deltaTime * speed);
    }


    void OnMove(InputValue moveVal)
    {
        movement = moveVal.Get<Vector2>();
    }
    void OnLook(InputValue lookVal)
    {
        mouseMovement = lookVal.Get<Vector2>();
    }

    void OnAttack()
    {
        Instantiate(Bullet, BulletSpawner.transform.position, BulletSpawner.transform.rotation);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Collided with: " + hit.gameObject.name);
    }
}
