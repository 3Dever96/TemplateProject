using UnityEngine;

public class Projectile : Hitbox
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    private float currentLifeTime;

    private Rigidbody body;

    private ProjectilePool pool;

    public void Initialize(ProjectilePool newPool)
    {
        pool = newPool;
        body = GetComponent<Rigidbody>();
        Deactivate();
    }

    private void Update()
    {
        currentLifeTime -= Time.deltaTime;

        if (currentLifeTime <= 0f)
        {
            Deactivate();
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        opponent = other.GetComponentInParent<CharacterStats>();

        OnInteract();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        pool.AddToPool(this);
    }

    public void Spawn(Vector3 origin, Vector3 direction)
    {
        transform.position = origin;
        transform.rotation = Quaternion.LookRotation(direction);

        body.linearVelocity = transform.forward * speed;
        currentLifeTime = lifeTime;
    }

    public override void OnInteract()
    {
        base.OnInteract();

        Deactivate();
    }
}
