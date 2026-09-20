using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] public Dictionary<string, Stat> stats;

    public float currentHp;

    private void Start()
    {
        foreach (KeyValuePair<string, Stat> pair in stats)
        {
            Stat stat = pair.Value;
            stat.Initialize();
        }

        currentHp = stats["HP"].Value;
    }

    public void TakeDamage(float damage)
    {

    }
}
