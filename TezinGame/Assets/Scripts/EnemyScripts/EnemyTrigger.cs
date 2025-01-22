using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private EnemyBrain brain;
    private GameManager manager;

    private void Awake()
    {
        brain = FindObjectOfType<EnemyBrain>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Totem")) 
        {   
            Debug.Log("Colidi com totem!");

            if (brain.totem1 == null) brain.totem1 = other.transform;

            else if (brain.totem2 == null) brain.totem2 = other.transform;
        }
    }
}
