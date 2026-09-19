using UnityEngine;

[System.Serializable]
public class EnemyRoamState : EnemyNavState
{
    [SerializeField] private float roamDistance;
    [SerializeField] private float waitTime;

    [SerializeField] private float roamSpeed;
    [SerializeField] private float chaseDistance;

    private Vector3 roamPoint;
    private float currentTime;
    private bool getPosition;

    private Vector3 targetPosition;
    private bool isRoaming;

    public override void StartState(EnemyNavController enemy)
    {
        enemy.Agent.speed = roamSpeed;

        if (!getPosition)
        {
            roamPoint = enemy.transform.position;
            getPosition = true;

            currentTime = Random.Range(0f, waitTime);
        }
    }

    public override void UpdateState(EnemyNavController enemy)
    {
        if (isRoaming)
        {
            enemy.Agent.SetDestination(targetPosition);
            enemy.Agent.isStopped = false;

            // Using 2D vector logic for flat distance checking
            Vector2 ePosition = new Vector2(enemy.transform.position.x, enemy.transform.position.z);
            Vector2 tPosition = new Vector2(targetPosition.x, targetPosition.z);

            // Check if the agent reached the target, or if the path is entirely broken
            if (Vector2.Distance(ePosition, tPosition) < 1.5f ||
                enemy.Agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
            {
                ResetWaitTime();
            }
            // Pro-tip: If the agent gets physically stuck, check if remainingDistance stops changing
            else if (!enemy.Agent.pathPending && enemy.Agent.remainingDistance <= enemy.Agent.stoppingDistance)
            {
                ResetWaitTime();
            }
        }
        else
        {
            enemy.Agent.isStopped = true;
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                float x = Random.Range(-roamDistance, roamDistance);
                float z = Random.Range(-roamDistance, roamDistance);
                Vector3 randomPoint = roamPoint + new Vector3(x, 0f, z);

                // 1. Validate and snap the random point to the closest NavMesh position
                // We use a max range of 2f to look for a nearby valid mesh edge
                if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out UnityEngine.AI.NavMeshHit hit, 2f, UnityEngine.AI.NavMesh.AllAreas))
                {
                    targetPosition = hit.position;
                    isRoaming = true;
                }
                else
                {
                    // 2. Fallback: If the point is completely off-mesh, reset timer to try a new point next frame
                    ResetWaitTime();
                }
            }
        }
    }

    // Helper method to keep your random wait generation clean and consistent
    private void ResetWaitTime()
    {
        currentTime = Random.Range(0f, waitTime);
        isRoaming = false;
    }


    public override void ChangeState(EnemyNavController enemy)
    {
        if (enemy.GetDistanceToPlayer() <= chaseDistance)
        {
            enemy.SetState(enemy.ChaseState);
        }
    }

    public override void ExitState(EnemyNavController enemy)
    {
        
    }
}
