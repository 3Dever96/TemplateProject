using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private List<EnemyNavController> enemies = new List<EnemyNavController>();
    private Queue<EnemyNavController> enemyQueue = new Queue<EnemyNavController>();
    [SerializeField] private float spawnTime;
    [SerializeField] private int enemyLimit;
    [SerializeField] private List<EnemyNavController> activeEnemies = new List<EnemyNavController>();

    private float currentTime;

    private void Awake()
    {
        for (var i = 0; i < enemies.Count; i++)
        {
            enemies[i].mySpawner = this;
            QueueEnemy(enemies[i]);
        }
    }

    private void Update()
    {
        if (activeEnemies.Count < enemyLimit)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                activeEnemies.Add(SpawnEnemy());
                currentTime = spawnTime;
            }
        }
    }

    private void OnEnable()
    {
        currentTime = spawnTime;
    }

    public void QueueEnemy(EnemyNavController newEnemy)
    {
        if (enemyQueue.Contains(newEnemy)) { return; }

        enemyQueue.Enqueue(newEnemy);
        newEnemy.gameObject.SetActive(false);
    }

    public EnemyNavController SpawnEnemy()
    {
        if (enemyQueue.Count == 0) {  return null; }

        EnemyNavController newEnemy = enemyQueue.Dequeue();

        newEnemy.gameObject.SetActive(true);

        if (newEnemy.Agent == null)
        {
            newEnemy.SetAgent();
        }

        newEnemy.Agent.enabled = false;
        newEnemy.transform.position = transform.position;
        newEnemy.transform.rotation = transform.rotation;
        newEnemy.Agent.enabled = true;

        return newEnemy;
    }
}
