using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerStateManager : MonoBehaviour
{

    PlayerBaseState currentState;

    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerWalkState walkState = new PlayerWalkState();
    [HideInInspector]
    public PlayerSneakState sneakState
    CharacterController controller;

    [HideInInspector]
    public Vector2 movement;
    CharacterController controller;
    public float default_speed = 1;

    public bool isSneaking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        SwitchState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    //Handle Input

    void OnMove(InputValue moveVal)
    {
        Debug.Log("Moving");
        movement = moveVal.Get<Vector2>();
    }

    void OnSprint()
    {
        if (isSneaking == false)
        {
            isSneaing = true;
        }else
        {
            isSneaking = false;
        }
    }
    public void MovePlayer(float speed)
    {
        float moveX = movement.x;
        float moveY = movement.y;

        Vector3 actual_movement = new Vector3(moveX, 0, moveY);
        controller.Move(actual_movement * Time.deltaTime * speed);


    }

    public void SwitchState(PlayerBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);

    }
}
