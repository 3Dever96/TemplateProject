using UnityEngine;

public class HitboxController : MonoBehaviour
{
    private GameObject hitbox;

    private void Start()
    {
        hitbox = transform.GetChild(0).gameObject;

        hitbox.SetActive(false);
    }

    public void ActivateHitbox()
    {
        if (hitbox != null)
        {
            hitbox.SetActive(true);
        }
    }

    public void DeactivateHitbox()
    {
        if (hitbox != null)
        {
            hitbox.SetActive(false);
        }
    }
}
