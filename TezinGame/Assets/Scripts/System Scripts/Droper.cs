using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Droper : MonoBehaviour
{   
    private EnemySpawner spawner;
    private GameManager gameManager;

    [Header("Lista dos Pontos de Drop")]
    public List<Transform> DropPoints = new List<Transform>();

    [SerializeField]
    private int RandomDropPointInteger;

    private void Awake()
    {
        spawner = FindObjectOfType<EnemySpawner>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void TryToDropSmth()
    {   
        if(!gameManager.isPlayerAlive())
        {   
            enabled = false;
        }

        Debug.Log("Trying to drop Something");

        if(spawner.Round % 5 == 0)
        {   
            DropSmth();
        }
    }

    private void DropSmth()
    {
        Debug.Log("Droping");

        RandomDropPointInteger = Random.Range(0, DropPoints.Count);

        Debug.Log("Object droped in " + DropPoints[RandomDropPointInteger].position + " Position Sucefully!");
    }
}
