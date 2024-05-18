using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Collections;
using UnityEngine;

public class Droper : MonoBehaviour
{   
    private EnemySpawner spawner;
    private GameManager gameManager;
    public GameObject armaParaDropar;

    [Header("Lista dos Pontos de Drop")]
    public List<Transform> DropPoints = new List<Transform>();

    [SerializeField]
    private int RandomDropPointInteger;

    private void Awake()
    {
        spawner = FindObjectOfType<EnemySpawner>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void DropSmth()
    {   
        if(!gameManager.isPlayerAlive())
        {   
            enabled = false;
        }

        Debug.Log("Droping");

        RandomDropPointInteger = Random.Range(0, DropPoints.Count);
        UnityEngine.Quaternion weaponRotation = UnityEngine.Quaternion.Euler(0f, 0f, 14.024f);

        Instantiate(armaParaDropar, DropPoints[RandomDropPointInteger].position, weaponRotation);

        Debug.Log("Object droped in " + DropPoints[RandomDropPointInteger].position + " Position Sucefully!");
    }

    public UnityEngine.Vector3 WeaponPos()
    {
        return DropPoints[RandomDropPointInteger].position;
    }
}
