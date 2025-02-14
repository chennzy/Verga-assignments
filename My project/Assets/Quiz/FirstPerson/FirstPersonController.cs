using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{   [SerializeField]
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

        cameraUpRotation =- lookY;


        cameraUpRotation = Mathf.Clamp(cameraUpRotation, -90, 90);
        
        
        cam.transform.localRotation = Quaternion.Euler(cameraUpRotation * Time.deltaTime, 0, 0); 

        transform.Rotate(Vector3.up * lookX);
        


        float moveX = movement.x;
        float moveY = movement.y;

        Vector3 actual_moveset = (transform.forward * moveY) + (transform.right * moveX);

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




}


