using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private ProjectilePool pool;
    [SerializeField] private InputHub input;

    [SerializeField] private int magazineLength;
    [SerializeField] private int clip;
    [SerializeField] private float reloadTime;
    [SerializeField] private float coolDownTime;

    public int currentRound;
    public float currentCoolDownTime;
    public float currentReloadTime;

    public bool isReloading;
    public bool isCooling;

    private void Start()
    {
        input = GetComponent<InputHub>();

        currentRound = magazineLength;

        currentCoolDownTime = coolDownTime;
        currentReloadTime = reloadTime;
    }

    private void Update()
    {
        if (pool != null)
        {
            if (isCooling)
            {
                currentCoolDownTime -= Time.deltaTime;

                if (currentCoolDownTime <= 0f)
                {
                    isCooling = false;
                    currentCoolDownTime = coolDownTime;
                }
            }

            if (isReloading)
            {
                currentReloadTime -= Time.deltaTime;

                if (currentReloadTime <= 0f)
                {
                    clip--;
                    currentRound = magazineLength;
                    isReloading = false;
                    isCooling = false;

                    currentCoolDownTime = coolDownTime;
                    currentReloadTime = reloadTime;
                }
            }

            if (input.Shoot && !isCooling && !isReloading && clip > 0)
            {
                Projectile bullet = pool.GetProjectile();
                if (bullet != null)
                {
                    bullet.Spawn(spawnPoint.position, spawnPoint.forward);
                    currentRound--;

                    if (currentRound <= 0)
                    {
                        isReloading = true;
                    }
                    else
                    {
                        isCooling = true;
                    }
                }
            }
        }
    }
}
