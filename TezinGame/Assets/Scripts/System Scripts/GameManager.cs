using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Jogadores")]
    public Transform playerTransform;
    public GameObject playerGameObject;
    public GameObject enemy;

    [Header("Características dos Jogadores")]
    public float enemySpeed = 4f;
    public float playerSpeed = 5f;
    public int playerHp = 7;

    [Header("Screen Size")]
    public static int width = 72;
    public static int height = 107;

    [Header("Health Bar Settings")]
    public Transform healthBar; // Barra de Vida
    public GameObject healthBarGameObject; // GameObject da Barra de Vida
    private Vector3 healthBarScale; // Tamanho da barra
    private float healtPercent; // Percentual de vida para cálculo

    private void Start()
    {
        // Inicialize a variável healthBarScale corretamente
        healthBarScale = healthBar.localScale;
        healtPercent = healthBarScale.x / playerHp;
    }

    public bool isPlayerAlive()
    {
        if (playerTransform)
        {
            return true;
        }
        else
        {
            Application.Quit();
            return false;
        }
    }

    public void DecreasePlayerHP(int Damage)
    {   

        playerHp -= Damage;
        
        if (playerHp <= 0)
        {
            Destroy(playerGameObject);
            Destroy(healthBarGameObject);

            enabled = false;
        }

        else
        {
            // Atualize a escala da barra de vida
            healthBarScale.x = healtPercent * playerHp;
            healthBar.localScale = healthBarScale;
        }
    }
}
