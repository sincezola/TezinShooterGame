using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Collections;
using UnityEngine;

public class Droper : MonoBehaviour
{   
    private EnemySpawner spawner;
    private GameManager gameManager;
    public GameObject gunToDrop;

    [Header("Lista dos Pontos de Drop")]
    public List<Transform> DropPoints = new List<Transform>();

    [Header("Lista dos Objetos para Serem Dropados")]
    public List<GameObject> ItemsArray = new List<GameObject>();

    [Header("Texto para Dropar Item")]
    public GameObject dropingTXT;

    [SerializeField]
    private int RandomDropPointInteger;

    private void Awake()
    {
        spawner = FindObjectOfType<EnemySpawner>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        dropingTXT.SetActive(false);
    }

    public IEnumerator DropSmth()
    {   
        if(!gameManager.isPlayerAlive())
        {   
            enabled = false;
        }

        dropingTXT.SetActive(true);

        int randomIndex = Random.Range(0, ItemsArray.Count);

        gunToDrop = ItemsArray[randomIndex];

        yield return new WaitForSeconds(2.0f);

        Debug.Log("Droping");

        RandomDropPointInteger = Random.Range(0, DropPoints.Count);
        UnityEngine.Quaternion gunRotation = UnityEngine.Quaternion.Euler(0f, 0f, 14.024f);

        Instantiate(gunToDrop, DropPoints[RandomDropPointInteger].position, gunRotation);

        Debug.Log("Object dropped in " + DropPoints[RandomDropPointInteger].position + " Position Sucefully!");

        dropingTXT.SetActive(false);
    }

    public UnityEngine.Vector3 WeaponPos()
    {
        return DropPoints[RandomDropPointInteger].position;
    }
}
