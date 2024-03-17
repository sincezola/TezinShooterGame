using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{   
    [Header("Round")]
    public int Round = 1;

    // Public variables
    public List<Transform> enemySpawnPoints = new List<Transform>();
    public int enemysSpawned;

    // Private variables
    private GameManager gameManager;
    private Animator anim;
    private int enemiesDead;
    private EnemyBrain brain;
    private int enemyListNumber;
    private Vector3 enemyPos;

    void Awake()
    {  
        gameManager = FindObjectOfType<GameManager>();

        if(gameManager == null)
        {
            Debug.LogError("GameManager não encontrado.");
        }
    }

    void Start()
    {
        InstanciarInimigos(1);
        //RoundAnim(); // Chamada original, pode ser removida se preferir usar AnimateTextRound()
        brain = FindObjectOfType<EnemyBrain>();
        anim = GetComponent<Animator>();
    }

    public void EnemyDied()
    {   
        enemiesDead++;

        if(enemiesDead == enemysSpawned)
        {
            Round++;
            //RoundAnim(); // Chamada original, pode ser removida se preferir usar AnimateTextRound()
            InstanciarInimigos(Round);
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

    // RoundTXT
    public void AnimateTextRound()
    {
        anim.SetTrigger("RoundAnimation");

        Debug.Log("Trigou a Animação");
    }

    private void RoundAnim()
    {
        anim.SetTrigger("RoundAnimation");

        Debug.Log("Trigou a Animação");
    }
}
