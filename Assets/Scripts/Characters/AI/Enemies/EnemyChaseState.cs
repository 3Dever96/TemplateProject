using TMPro;
using UnityEngine;

[System.Serializable]
public class EnemyChaseState : EnemyNavState
{
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float chaseDistance;

    private Vector3 targetPosition;

    public override void StartState(EnemyNavController enemy)
    {
        enemy.Agent.speed = chaseSpeed;
        enemy.Agent.isStopped = false;
    }

    public override void UpdateState(EnemyNavController enemy)
    {
        if (UnityEngine.AI.NavMesh.SamplePosition(enemy.Player.position, out UnityEngine.AI.NavMeshHit hit, 2f, UnityEngine.AI.NavMesh.AllAreas))
        {
            targetPosition = hit.position;
        }
        else
        {
            targetPosition = enemy.transform.position;
        }

        if (targetPosition != enemy.transform.position)
        {
            enemy.Agent.SetDestination(targetPosition);
        }
    }

    public override void ChangeState(EnemyNavController enemy)
    {
        if (targetPosition == enemy.transform.position || enemy.GetDistanceToPlayer() > chaseDistance)
        {
            enemy.SetState(enemy.RoamState);
        }
    }

    public override void ExitState(EnemyNavController enemy)
    {
        
    }
}
