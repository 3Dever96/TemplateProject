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

    private void OnTriggerEnter(Collider other)
    {
        if (interactType == InteractType.Enter)
        {
            OnInteract();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (interactType == InteractType.Stay)
        {
            OnInteract();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (interactType == InteractType.Exit)
        {
            OnInteract();
        }
    }
}
