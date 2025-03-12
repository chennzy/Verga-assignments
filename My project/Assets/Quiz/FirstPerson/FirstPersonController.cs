using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] float speed = 2.0f;
    [SerializeField] float mouseSensitivity = 100;
    [SerializeField] GameObject cam;
    [SerializeField] private GameObject NextStage; 

    Vector2 movement;
    Vector2 mouseMovement;
    float cameraUpRotation = 0;
    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

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

        if (!controller.isGrounded)
        {
            actual_moveset.y -= 9.81f * Time.deltaTime;
        }
        else
        {
            actual_moveset.y = -0.1f;
        }

        controller.Move(actual_moveset * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "NextStage") 
        {
            Debug.Log("Player touched the 'Next stage' block! Switching scene...");
            SceneManager.LoadScene("EndScene2");
        }
    }

    void OnMove(InputValue moveVal)
    {
        movement = moveVal.Get<Vector2>();
    }

    void OnLook(InputValue lookVal)
    {
        mouseMovement = lookVal.Get<Vector2>();
    }
}
