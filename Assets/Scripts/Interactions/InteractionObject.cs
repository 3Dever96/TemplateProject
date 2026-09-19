using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InteractionObject : InteractionBase
{
    public Transform interactPoint;
    public float interactDistance;

    private bool isCurrent;
    public bool canInteract;

    protected virtual void Start()
    {
        if (interactPoint == null)
        {
            interactPoint = transform;
        }
    }

    protected virtual void Update()
    {
        if (isCurrent)
        {
            transform.Rotate(Vector3.up, 90f * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.up, -22.5f * Time.deltaTime);
        }
    }

    public override void OnInteract()
    {
        if (canInteract)
        {
            print("Interacted with " + name);
            isCurrent = false;
            canInteract = false;
        }
    }

    public void SetCurrent(bool value)
    {
        isCurrent = value;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (canInteract)
        {
            InteractionHub hub = other.GetComponent<InteractionHub>();

            if (hub != null)
            {
                hub.AddObject(this);
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        InteractionHub hub = other.GetComponent<InteractionHub>();

        if (hub != null)
        {
            hub.RemoveObject(this);
        }
    }
}
