using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public int playerHp = 3;
    
    [Header("Screen Size")]
    public static int width = 17;
    public static int height = 10;

    public bool isPlayerAlive()
    {
        if(playerTransform)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public void DecreasePlayerHP(int Damage)
    {
        playerHp -= Damage;

        if (playerHp <= 0)
        {
          Destroy(playerGameObject);
        }
    }
}
