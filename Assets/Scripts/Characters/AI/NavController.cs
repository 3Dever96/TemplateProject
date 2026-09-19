using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(CapsuleCollider))]
public class NavController : MonoBehaviour
{
    public NavMeshAgent Agent { get; private set; }

    protected virtual void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }
}
