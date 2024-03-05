using UnityEngine;

public class EnemySpawner : MonoBehaviour
{   
    private GameManager gameManager;
    public float minDistanciaEntreJogadorEInimigo = 5f;  //Distância mínima entre jogador e inimigo.

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
}
