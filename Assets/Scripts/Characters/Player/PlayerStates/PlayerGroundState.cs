using UnityEngine;

[System.Serializable]
public class PlayerGroundState : PlayerState
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float accel;
    [SerializeField] private float decel;
    [SerializeField] private float fric;
    [SerializeField] private float turnAngle;

    private float moveSpeed;

    private bool canJump;
    private bool canMelee;

    public override void StartState(PlayerController player)
    {
        player.VerticalSpeed = player.StickForce;
        canJump = false;
        canMelee = false;
    }

    public override void UpdateState(PlayerController player)
    {
        // Set direction based on input
        Vector3 direction = player.Cam.right * player.Input.Move.x + player.Cam.forward * player.Input.Move.y;
        direction.y = 0f;
        direction = direction.normalized;

        // Set the desired movement speed
        moveSpeed = maxSpeed * player.Input.Move.magnitude;

        // Calculate movement speed using a momentum system
        if (player.Input.Move != Vector2.zero)
        {
            if (Vector3.Angle(direction, player.Direction) > turnAngle)
            {
                player.CurrentSpeed -= decel * Time.deltaTime;

                if (player.CurrentSpeed <= 0f)
                {
                    player.CurrentSpeed = 0f;
                    player.Direction = direction;
                }
            }
            else
            {
                if (player.CurrentSpeed < moveSpeed)
                {
                    player.CurrentSpeed += accel * Time.deltaTime;
                }
                else if (player.CurrentSpeed > moveSpeed + 0.1f)
                {
                    player.CurrentSpeed -= accel * Time.deltaTime;
                }
                else
                {
                    player.CurrentSpeed = moveSpeed;
                }

                player.Direction = direction;
            }
        }
        else
        {
            player.CurrentSpeed -= Mathf.Min(player.CurrentSpeed, fric * Time.deltaTime);
        }

        player.FaceDirection(player.Direction, player.TurnSpeed);

        if (player.Input.Jump && canJump)
        {
            player.VerticalSpeed = player.JumpSpeed;
        }

        canJump = !player.Input.Jump;
    }

    public override void ChangeState(PlayerController player)
    {
        if (player.VerticalSpeed > 0f || !player.CheckCollision(-Vector3.up * 0.1f))
        {
            player.SetState(player.AirState);
        }

        if (player.Input.Melee && canMelee)
        {
            player.SetState(player.AttackState);
        }

        canMelee = !player.Input.Melee;
    }

    public override void ExitState(PlayerController player)
    {
        
    }
}
