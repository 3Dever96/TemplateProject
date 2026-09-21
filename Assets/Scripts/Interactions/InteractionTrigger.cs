using Unity.AppUI.UI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(BoxCollider))]
public class InteractionTrigger : InteractionBase
{
    public enum InteractType
    {
        Enter,
        Stay,
        Exit
    }

    [SerializeField] private InteractType interactType;

    public override void OnInteract()
    {
        print("Player position is " + transform.position);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (interactType == InteractType.Enter)
        {
            OnInteract();
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (interactType == InteractType.Stay)
        {
            OnInteract();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (interactType == InteractType.Exit)
        {
            OnInteract();
        }
    }
}
