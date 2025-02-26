using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    [HideInInspector] PlayerBaseState currentState;
    [HideInInspector] public PlayerIdleState idleState = new PlayerIdleState();
    [HideInInspector] public PlayerWalkState walkState = new PlayerWalkState();
    [HideInInspector] public PlayerSneakState sneakState  =new PlayerSneakState();
    [HideInInspector] public Vector2 movement;

    public float default_speed = 20;
    public bool isSneaking = false;

    CharacterController controller;

    // Camera Movement
    [SerializeField] GameObject cam;
    [SerializeField] float mouseSensitivity = 100f;
    private float cameraUpRotation = 0f;
    private Vector2 mouseMovement;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cam == null)
        {
            cam = Camera.main?.gameObject; // Automatically assigns the Main Camera if not set
            if (cam == null)
            {
          
            }
        }

        SwitchState(idleState);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        currentState.UpdateState(this);
        RotateCamera(); // Apply camera rotation
    }

    // Handle Input
    void OnMove(InputValue moveVal)
    {
        movement = moveVal.Get<Vector2>();
    }

    void OnSprint()
    {
        isSneaking = !isSneaking;

    }

    void OnLook(InputValue lookVal)
    {
        mouseMovement = lookVal.Get<Vector2>();
    }

    // Helper Function for Player Movement
    public void MovePlayer(float speed)
    {
        float moveX = movement.x;
        float moveZ = movement.y;

        Vector3 actual_movement = transform.right * moveX + transform.forward * moveZ;
        controller.Move(actual_movement * Time.deltaTime * speed);
    }

    public void SwitchState(PlayerBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    // Rotate Camera Based on Mouse Input
    void RotateCamera()
    {
        float lookX = mouseMovement.x * Time.deltaTime * mouseSensitivity;
        float lookY = mouseMovement.y * Time.deltaTime * mouseSensitivity;

        cameraUpRotation -= lookY;
        cameraUpRotation = Mathf.Clamp(cameraUpRotation, -90f, 90f); // Prevent flipping

        cam.transform.localRotation = Quaternion.Euler(cameraUpRotation, 0f, 0f); // Rotate camera up/down
        transform.Rotate(Vector3.up * lookX); // Rotate player left/right
    }
}


/*void OnSprint(InputValue sprintVal)
{
    isSneaking = sprintVal.isPressed;
}*/