using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{   
    private GameManager gameManager;
    private int enemyHp = 5;

    [Header("Health Bar Settings")]
    public Transform healthBar; // Barra verde
    private Vector3 healthBarScale; // Tamanho da barra
    private float healtPercent; // Percentual de vida para calculo

    private void Awake()
    {
      gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
      healthBarScale = healthBar.localScale; // Guarda o tamanho original da bar
      healtPercent = healthBarScale.x / enemyHp;
    }

    private void Update()
    {
      if (gameManager.isPlayerAlive())
      {
          // Move o inimigo em direção ao jogador.
          transform.position = Vector2.MoveTowards(transform.position, gameManager.playerTransform.position, gameManager.enemySpeed * Time.deltaTime);
      }

      if (enemyHp <= 0)
      {
        Destroy(gameObject);
      }
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
      if(other.gameObject.tag == "Bullet")
      {
        enemyHp -= 1;
        healthBarScale.x = healtPercent * enemyHp;
        healthBar.localScale = healthBarScale;
      }

    }
}
