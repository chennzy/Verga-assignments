using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("im idling");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //what am i doing in this sate
        //Nothing

        
        if (player.movement.magnitude > 0.1)
        {
            if (player.isSneaking)
            {
                player.SwitchState(player.sneakState);
            }else
                player.SwitchState(player.walkState);
        }
    }

}

