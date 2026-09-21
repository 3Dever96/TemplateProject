using UnityEngine;

[System.Serializable]
public class EnemyAttackState : EnemyNavState
{
    [SerializeField] private float windUpTime;
    [SerializeField] private float attackTime;
    [SerializeField] private float coolDownTime;

    private float currentWindUpTime;
    private float currentAttackTime;
    private float currentCoolDownTime;

    private bool isAttacking;

    [SerializeField] private HitboxController hitbox;

    public override void StartState(EnemyNavController enemy)
    {
        isAttacking = false;

        currentWindUpTime = windUpTime;
        currentAttackTime = attackTime;
        currentCoolDownTime = coolDownTime;

        enemy.Agent.isStopped = true;
    }

    public override void UpdateState(EnemyNavController enemy)
    {
        if (!isAttacking)
        {
            currentWindUpTime -= Time.deltaTime;

            if (currentWindUpTime <= 0f)
            {
                isAttacking = true;
                hitbox.ActivateHitbox();
            }
        }
        else
        {
            currentAttackTime -= Time.deltaTime;
            currentCoolDownTime -= Time.deltaTime;

            if (currentAttackTime <= 0f)
            {
                hitbox.DeactivateHitbox();
            }
        }
    }

    public override void ChangeState(EnemyNavController enemy)
    {
        if (currentCoolDownTime <= 0f)
        {
            enemy.SetState(enemy.ChaseState);
        }
    }

    public override void ExitState(EnemyNavController enemy)
    {
        
    }
}
