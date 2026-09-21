using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    private Queue<Projectile> pool = new Queue<Projectile>();

    private void Awake()
    {
        Projectile[] children = GetComponentsInChildren<Projectile>();

        for (var i = 0; i < children.Length; i++)
        {
            children[i].Initialize(this);
        }
    }

    public void AddToPool(Projectile projectile)
    {
        if (!pool.Contains(projectile))
        {
            pool.Enqueue(projectile);
        }
    }

    public Projectile GetProjectile()
    {
        if (pool.Count > 0)
        {
            Projectile bullet = pool.Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }

        return null;
    }
}
