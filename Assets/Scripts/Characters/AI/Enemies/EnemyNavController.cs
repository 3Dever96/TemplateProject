using UnityEngine;

public class EnemyNavController : NavController
{
    public EnemyNavState CurrentState { get; private set; }
    [field: SerializeField] public EnemyRoamState RoamState { get; private set; } = new EnemyRoamState();
    [field: SerializeField] public EnemyChaseState ChaseState { get; private set; } = new EnemyChaseState();

    public Transform Player { get; private set; }

    public EnemySpawnPoint mySpawner;

    protected override void Start()
    {
        base.Start();

        Player = GameObject.FindWithTag("Player").transform;

        SetState(RoamState);
    }

    private void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.UpdateState(this);
            CurrentState.ChangeState(this);
        }
    }

    public void SetState(EnemyNavState newState)
    {
        if (CurrentState != null)
        {
            CurrentState.ExitState(this);
        }

        CurrentState = newState;

        if (CurrentState != null)
        {
            CurrentState.StartState(this);
        }
    }

    public float GetDistanceToPlayer()
    {
        Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.z);
        Vector2 playerPosition = new Vector2(Player.position.x, Player.position.z);

        return Vector2.Distance(enemyPosition, playerPosition);
    }
}
