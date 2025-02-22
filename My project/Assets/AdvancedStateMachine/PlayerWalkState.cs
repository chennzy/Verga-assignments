using UnityEngine;

public class PlayerWalkState :PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("im walkiing");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //what am i doing in this sate
        player.MovePlayer(player.default_speed);




        //what condition moves to next state

        if (player.movement.magnitude < .01)
        {
            player.SwitchState(player.idleState);

        } else if (player.isSneaking)
        {
            player.SwitchState(player.sneakState);
        }


    }




}
