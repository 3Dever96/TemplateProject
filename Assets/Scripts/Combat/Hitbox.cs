using UnityEngine;

public class Hitbox : InteractionTrigger
{
    public float atk;
    private CharacterStats opponent;

    public override void OnInteract()
    {
        opponent.TakeDamage(atk);
        opponent = null;
    }

    protected override void OnTriggerStay(Collider other)
    {
        opponent = other.GetComponentInParent<CharacterStats>();

        if (opponent != null)
        {
            OnInteract();
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        CharacterStats newStats = other.GetComponentInParent<CharacterStats>();

        if (newStats != null)
        {
            if (newStats == opponent)
            {
                opponent = null;
            }
        }
    }
}
