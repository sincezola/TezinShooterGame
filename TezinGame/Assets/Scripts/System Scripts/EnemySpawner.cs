using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class EnemySpawner : MonoBehaviour
{   
    [Header("Round")]
    public int Round = 1;

    // Public variables
    public List<Transform> enemySpawnPoints = new List<Transform>();
    public int enemysSpawned;

    // Private variables
    private GameManager gameManager;
    private RoundTXTAnim txtAnim;
    private int enemiesDead;
    private int enemyListNumber;
    private Vector3 enemyPos;

    void Awake()
    {  
        gameManager = FindObjectOfType<GameManager>();
        txtAnim = FindObjectOfType<RoundTXTAnim>();

        if(gameManager == null)
        {
            Debug.LogError("GameManager não encontrado.");
        }
    }

    void Start()
    {   
        StartCoroutine(txtAnim.FirstEnemySpawn());

        txtAnim.AnimateRoundTXT(1);

    }

    public void EnemyDied()
    {   
        enemiesDead++;

        if(enemiesDead == enemysSpawned)
        {
            Round++;
            txtAnim.StartCoroutine("changeRoundTXT");
        }
        
        Debug.Log(enemiesDead + " Inimigos mortos");
    }

    public void InstanciarInimigos(int quantidadeInimigos)
    {
        if(!gameManager.isPlayerAlive())
        {
            enabled = false;
        }

        for(int i = 0; i < quantidadeInimigos; i++)
        {   

            enemyListNumber = Random.Range(0, enemySpawnPoints.Count - 1);
            enemyPos = enemySpawnPoints[enemyListNumber].position;

            GameObject enemy = Instantiate(gameManager.enemy, enemyPos, Quaternion.identity);

            enemysSpawned++;

            Debug.Log("Inimigo Spawnado");
        }
    }
}
