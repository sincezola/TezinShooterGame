using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
  private int contador;
  private EnemyBrain enemyScript;

  private void Awake()
  {
      enemyScript = FindObjectOfType<EnemyBrain>();
  }

  private void Start()
  {
    InvokeRepeating("Contador", 1.0f, 1.0f);
  }
  private void Update()
  {
    if (contador >= 1) // Destrói a bala depois de 1 segundo de ela ser atirada
    {
      Destroy(gameObject);
      contador = 0;
    }
  }
  private void OnTriggerEnter2D(Collider2D other)
  {   
    Debug.Log("Atingi " + other.name);

    if(other.gameObject.tag == "Enemy")
    {
      Destroy(gameObject);
    }
  }

  private void Contador()
  {
    contador++;
  }
}
