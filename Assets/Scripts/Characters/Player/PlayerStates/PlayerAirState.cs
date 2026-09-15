using UnityEngine;

public class PlayerAirState : PlayerState
{
    public override void StartState(PlayerController player)
    {
        
    }

    public override void UpdateState(PlayerController player)
    {
        if (!player.Input.Jump || player.CheckCollision(Vector3.up * 1.1f))
        {
            player.VerticalSpeed = Mathf.Min(0f, player.VerticalSpeed);
        }

        if (player.VerticalSpeed > player.FallSpeed)
        {
            player.VerticalSpeed += player.Gravity * Time.deltaTime;
        }
    }

    public override void ChangeState(PlayerController player)
    {
        if (player.VerticalSpeed < 0f && player.CheckCollision(-Vector3.up * 0.1f))
        {
            player.SetState(player.GroundState);
        }
    }

    public override void ExitState(PlayerController player)
    {

    }
}
