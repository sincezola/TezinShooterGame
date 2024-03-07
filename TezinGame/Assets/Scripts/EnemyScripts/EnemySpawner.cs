using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Public variables
    public List <Transform> enemySpawnPoints = new List<Transform>();

    // Private variables
    private GameManager gameManager;
    private int timer;
    private int howManyEnemiesToSpawn = 2;
    private int enemyListNumber;
    private Vector3 enemyPos;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager não encontrado.");
        }
    }

    void Start()
    {
        InstanciarInimigos(1);
        InvokeRepeating("RoundSystem", 1.0f, 1.0f);
    }
    void InstanciarInimigos(int quantidadeInimigos)
    {   
        for(int i = 0; i < quantidadeInimigos; i++)
        {   

            enemyListNumber = Random.Range(0, enemySpawnPoints.Count - 1);
            enemyPos = enemySpawnPoints[enemyListNumber].position;

            GameObject enemy = Instantiate(gameManager.enemy, enemyPos, Quaternion.identity);
        }

    }

    private void RoundSystem()
    {
        timer ++;

        if(timer >= 5)
        {
            InstanciarInimigos(howManyEnemiesToSpawn);
            
            howManyEnemiesToSpawn ++;
            timer = 0;
        }
    }
}
