using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(SphereCollider))]
public class InteractionHub : MonoBehaviour
{
    public List<InteractionObject> interactions = new List<InteractionObject>();
    public InteractionObject currentObject;
    public InteractionObject lastObject;

    private InputHub input;
    private bool canInteract;

    private void Start()
    {
        input = GetComponentInParent<InputHub>();
    }

    private void Update()
    {
        interactions.Sort(SortObjects);

        currentObject = interactions.Count > 0 ? interactions[0] : null;

        if (currentObject != lastObject)
        {
            if (currentObject != null)
            {
                currentObject.SetCurrent(true);
            }

            if (lastObject != null)
            {
                lastObject.SetCurrent(false);
            }

            lastObject = currentObject;
        }

        if (input.Interact && canInteract)
        {
            if (currentObject != null)
            {
                Vector2 position = transform.position;
                Vector2 objectPos = currentObject.interactPoint.position;

                if (Vector2.Distance(position, objectPos) < currentObject.interactDistance)
                {
                    currentObject.OnInteract();
                    RemoveObject(currentObject);
                }
            }
        }

        canInteract = !input.Interact;
    }

    public bool AddObject(InteractionObject newObject)
    {
        if (!interactions.Contains(newObject))
        {
            interactions.Add(newObject);
            return true;
        }

        return false;
    }

    public bool RemoveObject(InteractionObject newObject)
    {
        if (interactions.Contains(newObject))
        {
            interactions.Remove(newObject);
            return true;
        }

        return false;
    }

    private int SortObjects(InteractionObject a, InteractionObject b)
    {
        Vector2 player = new Vector2(transform.position.x, transform.position.z);
        Vector2 positionA = new Vector2(a.interactPoint.position.x, a.interactPoint.position.z);
        Vector2 positionB = new Vector2(b.interactPoint.position.x, b.interactPoint.position.z);

        float distA = Vector2.Distance(player, positionA);
        float distB = Vector2.Distance(player, positionB);

        if (distA > distB)
        {
            return 1;
        }

        if (distA < distB)
        {
            return -1;
        }

        return 0;
    }
}
