using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Public variables
    public float minDistanciaEntreJogadorEInimigo = 5f;  //Distância mínima entre jogador e inimigo.

    // Private variables
    private GameManager gameManager;
    private int timer;
    private int howManyEnemiesToSpawn = 2;

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
        if (gameManager.isPlayerAlive())
        {
            for (int i = 0; i < quantidadeInimigos; i++)
            {
                Vector3 posicaoJogador = gameManager.playerTransform.position;

                float posX, posY;
                do
                {
                    posX = Random.Range(-GameManager.width / 2f, GameManager.width / 2f);
                    posY = Random.Range(-GameManager.height / 2f, GameManager.height / 2f);

                } while (Vector3.Distance(posicaoJogador, new Vector3(posX, posY, 0f)) < minDistanciaEntreJogadorEInimigo);

                //Instancia o objeto inimigo na posição calculada
                GameObject inimigo = Instantiate(gameManager.enemy, new Vector3(posX, posY, 0f), Quaternion.identity);

                Debug.Log("Nasci");
            }
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
