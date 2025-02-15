using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
public class FirstPersonController : MonoBehaviour
{
    Vector2 movement;
    Vector2 mouseMovement;
    Vector2 look;
    CharacterController controller;
    float cameraUpRotaion = 0;
    [SerializeField]
    float speed = 2.0f;
    float mouseSensitivity = 10f;
    public GameObject cam;
    [SerializeField]
    GameObject bullet;
    GameObject BulletSpawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float lookX = mouseMovement.x * Time.deltaTime * mouseSensitivity;
        float lookY = mouseMovement.y * Time.deltaTime * mouseSensitivity;
        
        cameraUpRotaion -= lookY;
        cam.transform.localRotation = Quaternion.Euler(cameraUpRotaion,0,0);
        
        float moveX = movement.x * Time.deltaTime * mouseSensitivity;
        float moveZ = movement.y * Time.deltaTime * mouseSensitivity;

        //Vector3 actual_movement = new Vector3(moveX, 0, moveZ);
        
        transform.Rotate(Vector3.up * lookX);

        Vector3 actual_movement = (transform.forward * moveZ + transform.right * moveX);
        controller.Move(actual_movement * Time.deltaTime * speed);
    }
    void OnMove(InputValue moveVal)
    {
        movement = moveVal.Get<Vector2>();
    }
    void OnLook(InputValue lookVal)
    {
        mouseMovement = lookVal.Get<Vector2>();
    }
    void onAttack()
    {
        //Instantiate(bullet, BulletSpawner.transform.rotation,
    }
}
