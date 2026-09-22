using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : MonoBehaviour
{
    [SerializeField] private Slider healthbar;
    private CharacterStats player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();

        player.OnHit.AddListener(UpdateHealthbar);
        player.OnDeath.AddListener(UpdateHealthbar);
    }

    public void UpdateHealthbar()
    {
        healthbar.value = Mathf.FloorToInt((player.currentHp / player.stats["HP"].Value) * 100f);
    }

    private void OnEnable()
    {
        if (player != null)
        {
            player.OnHit.AddListener(UpdateHealthbar);
            player.OnDeath.AddListener(UpdateHealthbar);
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();

            player.OnHit.AddListener(UpdateHealthbar);
            player.OnDeath.AddListener(UpdateHealthbar);
        }
    }

    private void OnDisable()
    {
        if (player != null)
        {
            player.OnHit.RemoveListener(UpdateHealthbar);
            player.OnDeath.RemoveListener(UpdateHealthbar);
        }
    }
}
