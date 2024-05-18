using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Jogadores")]
    public Transform playerTransform;
    public GameObject playerGameObject;
    public GameObject enemy;

    [Header("Objetos do Jogo")]
    public Sprite M4Weapon;
    public Sprite PistolWeapon;

    [Header("Características dos Jogadores")]
    public float enemySpeed = 4f;
    public float playerSpeed = 5f;
    public int playerHp = 7;

    [Header("Screen Size")]
    public static int width = 72;
    public static int height = 107;

    [Header("Health Bar Settings")]
    public List <GameObject> HealthBarPieces = new List<GameObject>();

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
            Destroy(HealthBarPieces[0]);

            enabled = false;
        }

        else
        {
            HealthBarPieces[playerHp].SetActive(false);
        }
    }
}
