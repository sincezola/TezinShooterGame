using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Jogadores")]
    public Transform playerTransform;
    public GameObject playerGameObject;
    public GameObject enemy;

    [Header("Armas do Jogo")]
    public Sprite M4Weapon;
    public Sprite PistolWeapon;
    public Sprite Shotgun;

    [Header("Características dos Jogadores")]
    public float enemySpeed = 4f;
    public float playerSpeed = 5f;
    public float playerHp = 7.0f;
    public float playerHpLimit = 7.0f;

    [Header("Screen Size")]
    public static int width = 72;
    public static int height = 107;
    
    [Header("Totem")]
    public int totemsHud = 0;
    public int totemCount = 0;
    public int maxTotemsPlaced = 2;

    [Header("Health Bar Settings")]
    public Transform healthBar; // Barra verde
    private Vector3 healthBarScale; // Tamanho da barra
    private float healtPercent; // Percentual de vida para calculo

    private EnemyBrain brain;

    private void Awake()
    {
        brain = FindObjectOfType<EnemyBrain>();
    }

    private void Start()
    {
        totemsHud = 0;
        healthBarScale = healthBar.localScale;
        healtPercent = healthBarScale.x / playerHp;
    }

    private void Update()
    {
        CheckTotems();
    }

    private void CheckTotems()
    {
        if (brain.totem1 == null && brain.totem2 == null) brain.ChangeEnemyObjective(playerTransform);
    }

    public bool isPlayerAlive()
    {
        if (playerTransform)
        {
            return true;
        }

        Application.Quit();
        
        return false;
    }

    public void DecreasePlayerHP(float Damage)
    {   
        playerHp -= Damage;
        AdjustPlayerHealth();
        
        if (playerHp <= 0)
        {
            Destroy(playerGameObject);

            enabled = false;
        }
    }

    public void IncreasePlayerHp(float quantity)
    {
        Debug.Log("Cheguei na função de ganhar vida");
        Debug.Log($"Vida antiga: {playerHp}");

        // Soma a vida que será ganha, respeitando o limite máximo
        playerHp = Mathf.Min(playerHp + quantity, playerHpLimit);

        Debug.Log($"Vida nova: {playerHp}");

        AdjustPlayerHealth();
    }

    public void IncreaseTotemsHud()
    {   
        Debug.Log("Increase!");

        totemsHud++;
    }

    public void DecreaseTotemsHud()
    {
        totemsHud--;
    }

    public void IncreaseTotemCount()
    {
        totemCount++;
    }

    public void DecreaseTotemCount()
    {
        totemCount--;
    }

    public void AdjustPlayerHealth()
    {
        healthBarScale.x = healtPercent * playerHp;
        healthBar.localScale = healthBarScale;
    }
}
