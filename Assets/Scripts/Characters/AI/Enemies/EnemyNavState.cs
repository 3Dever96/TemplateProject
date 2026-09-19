using UnityEngine;

[System.Serializable]
public abstract class EnemyNavState
{
    public abstract void StartState(EnemyNavController enemy);
    public abstract void UpdateState(EnemyNavController enemy);
    public abstract void ChangeState(EnemyNavController enemy);
    public abstract void ExitState(EnemyNavController enemy);
}
