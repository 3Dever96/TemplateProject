using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] public Dictionary<string, Stat> stats;

    public float currentHp;

    [Header("I Frame Variables")]
    [SerializeField] private float invincibleTime;
    [SerializeField] private float blinkTime;
    [SerializeField] private float hitStopTime;
    [SerializeField] private GameObject avatar;
    private bool isInvincible;

    public UnityEvent OnSpawn;
    public UnityEvent OnHit;
    public UnityEvent OnDeath;

    private void Start()
    {
        foreach (KeyValuePair<string, Stat> pair in stats)
        {
            Stat stat = pair.Value;
            stat.Initialize();
        }

        currentHp = stats["HP"].Value;

        OnSpawn?.Invoke();
    }

    private void OnEnable()
    {
        OnSpawn?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        if (!isInvincible && currentHp > 0f)
        {
            currentHp = Mathf.Clamp(currentHp - damage, 0, stats["HP"].Value);

            if (currentHp == 0f)
            {
                OnDie();
            }
            else
            {
                OnHit?.Invoke();
                StartCoroutine(SetInvincible());
            }
        }
    }

    protected virtual void OnDie()
    {
        OnDeath?.Invoke();
    }

    private IEnumerator SetInvincible()
    {
        isInvincible = true;
        float iFrame = 0f;
        float blink = 0f;

        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(hitStopTime);
        Time.timeScale = 1f;

        while (iFrame < invincibleTime)
        {
            iFrame += Time.deltaTime;
            blink += Time.deltaTime;

            if (blink >= blinkTime)
            {
                if (avatar != null)
                {
                    avatar.SetActive(!avatar.gameObject.activeInHierarchy);
                }
                blink = 0f;
            }

            yield return null;
        }

        if (avatar != null)
        {
            avatar.SetActive(true);
        }
        isInvincible = false;
    }
}
