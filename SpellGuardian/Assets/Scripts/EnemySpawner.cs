using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public int enemyDamage;
    private int baseEnemyDamage;
    public int enemyMaxHP;
    private int baseEnemyMaxHP;
    public double enemySpeed;
    private double baseEnemySpeed;
    public int prize;
    private int basePrize;

    public float enemyAttackDelay;

    public float spawnDelay;
    private float defaultSpawnDelay;
    private float baseDefaultSpawnDelay;
    public float timeToHardUp;
    private float defaultTimeToHardUp;
    public int difficultLevel;

    void Start()
    {
        defaultTimeToHardUp = timeToHardUp;
        difficultLevel = 1;

        defaultSpawnDelay = spawnDelay;
        spawnDelay = 0;

        baseEnemyDamage = enemyDamage;
        baseEnemyMaxHP = enemyMaxHP;
        baseEnemySpeed = enemySpeed;
        baseDefaultSpawnDelay = defaultSpawnDelay;
        basePrize = prize;
    }
    void Update()
    {
        if (timeToHardUp > 0)
        {
            timeToHardUp -= Time.deltaTime;
        }
        else
        {
            hardUp();
            timeToHardUp = defaultTimeToHardUp;
        }

        spawnEnemyEvent();
        
    }
    private void spawnEnemyEvent()
    {
        if (spawnDelay > 0)
        {
            spawnDelay -= Time.deltaTime;
        }
        else
        {
            enemy.GetComponent<EnemyManager>().maxHP = enemyMaxHP;
            enemy.GetComponent<EnemyManager>().currentHP = enemyMaxHP;
            enemy.GetComponent<EnemyManager>().movementSpeed = enemySpeed;
            enemy.GetComponent<EnemyManager>().damage = enemyDamage;
            enemy.GetComponent<EnemyManager>().attackDelay = enemyAttackDelay ;
            enemy.GetComponent<EnemyManager>().points = prize;
            Instantiate(enemy, transform.position, Quaternion.identity);
            spawnDelay = defaultSpawnDelay;
        }
    }

    private void hardUp()
    {
        difficultLevel++;  
        enemyDamage = (2*difficultLevel) + baseEnemyDamage;
        enemyMaxHP = (15 * difficultLevel) + baseEnemyMaxHP;
        if (difficultLevel > 20)
        {
            enemyMaxHP += difficultLevel*30;
        }
        enemySpeed = (0.07 * difficultLevel) + baseEnemySpeed;
        prize = (2 * difficultLevel) + basePrize;
        
        if (defaultSpawnDelay > 0.5)
        {
            defaultSpawnDelay = baseDefaultSpawnDelay - (0.5f * difficultLevel);
        }
        else
        {
            defaultSpawnDelay = 0.5f;
        }

    }
}
