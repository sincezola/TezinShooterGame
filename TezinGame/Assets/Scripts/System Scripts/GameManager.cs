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
    public int playerHp = 7;

    [Header("Screen Size")]
    public static int width = 72;
    public static int height = 107;
    
    [Header("Totem")]
    public int totemsHud = 0;
    public int totemCount = 0;
    public int maxTotemsPlaced = 2;

    [Header("Health Bar Settings")]
    public List <GameObject> HealthBarPieces = new List<GameObject>();

    private void Start()
    {
        totemsHud = 0;
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
}
