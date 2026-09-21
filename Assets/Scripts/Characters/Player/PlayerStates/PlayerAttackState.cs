using UnityEngine;

[System.Serializable]
public class PlayerAttackState : PlayerState
{
    [SerializeField] private HitboxController hitbox;
    [SerializeField] private float attackTime;
    private float currentTime;

    public override void StartState(PlayerController player)
    {
        player.CurrentSpeed = 0f;
        player.VerticalSpeed = 0f;

        hitbox.ActivateHitbox();

        currentTime = attackTime;
    }

    public override void UpdateState(PlayerController player)
    {
        
    }

    public override void ChangeState(PlayerController player)
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            player.SetState(player.GroundState);
        }
    }

    public override void ExitState(PlayerController player)
    {
        hitbox.DeactivateHitbox();
    }
}
