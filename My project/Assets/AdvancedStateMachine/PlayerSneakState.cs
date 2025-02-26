using UnityEngine;

public class PlayerSneakState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("im sneaking");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //what am i doing in this sate
        player.MovePlayer(player.default_speed / 2);




        //what condition moves to next state

        if (player.movement.magnitude < .01)
        {
            player.SwitchState(player.idleState);

        } else if (player.isSneaking == false){

            player.SwitchState(player.walkState);
        }


    }






}