using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private bool spawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn)
        {
            SpawnEnemy();
            spawn = false;
        }
        foreach (Enemy e in enemies)
        {
            e.Move();
        }
    }

    public void SpawnEnemy()
    {
        Vector3 playerPos = player.transform.position;
        MinMax RandomMinMax()
        {
            int min = 5;
            int max = 10;
            if (Random.Range(0,2) == 0)
            {
                min = -10;
                max = -5;
            }
            return new MinMax (min,max);
        }
        MinMax minMax1 = RandomMinMax();
        MinMax minMax2 = RandomMinMax();
        Vector3 spawnPosition = new Vector3(playerPos.x + Random.Range(minMax1.min, minMax1.max), 0, playerPos.z + Random.Range(minMax2.min, minMax2.max));

        Enemy newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity).GetComponent<Enemy>();
        newEnemy.gameObject.transform.parent = transform;
        newEnemy.SetPlayer(player);
        newEnemy.gameObject.transform.position = new Vector3(newEnemy.gameObject.transform.position.x, newEnemy.GroundDistance, newEnemy.gameObject.transform.position.z);
        enemies.Add(newEnemy);
    }

    public class MinMax
    {
        public MinMax(int minimum, int maximum)
        {
            min = minimum;
            max = maximum;
        }

        public int min;
        public int max;
    }
}

