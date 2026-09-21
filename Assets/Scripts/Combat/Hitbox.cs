using UnityEngine;

public class Hitbox : InteractionTrigger
{
    public float atk;
    public CharacterStats opponent;
    private CharacterStats myStats;

    private void Start()
    {
        myStats = GetComponentInParent<CharacterStats>();
    }

    private void Update()
    {
        if (myStats != null)
        {
            if (atk != myStats.stats["ATK"].Value)
            {
                atk = myStats.stats["ATK"].Value;
            }
        }
    }

    public override void OnInteract()
    {
        if (opponent != null)
        {
            opponent.TakeDamage(atk);
            opponent = null;
        }
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
