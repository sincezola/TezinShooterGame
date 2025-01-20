using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{   
    private GameManager gameManager;
    private EnemySpawner spawner;
    private int enemyHp = 5;
    private bool isCollidingWithPlayer = false;
    private bool isCollidingWithTotem = false;

    [Header("Health Bar Settings")]
    public Transform healthBar; // Barra verde
    private Vector3 healthBarScale; // Tamanho da barra
    private float healtPercent; // Percentual de vida para calculo

    private void Awake()
    {
      gameManager = FindObjectOfType<GameManager>();
      spawner = FindObjectOfType<EnemySpawner>();
    }

    private void Start()
    {
      healthBarScale = healthBar.localScale;
      healtPercent = healthBarScale.x / enemyHp;
    }

    private void Update()
    {
      if (gameManager.isPlayerAlive() && !isCollidingWithPlayer && !isCollidingWithTotem)
      {
        transform.position = Vector2.MoveTowards(transform.position, gameManager.playerTransform.position, gameManager.enemySpeed * Time.deltaTime);
      }
    }

  private void OnTriggerEnter2D(Collider2D other)
  { 
    if(other.gameObject.tag == "Bullet")
    {
      enemyHp--;
      healthBarScale.x = healtPercent * enemyHp;
      healthBar.localScale = healthBarScale;

      if (enemyHp <= 0)
      {
        Destroy(gameObject);

        spawner.EnemyDied();
      }
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          isCollidingWithPlayer = true;
        }

        else if (collision.gameObject.CompareTag("Totem"))
        {
          isCollidingWithTotem = true;
        }
    }

  private IEnumerator OnCollisionExit2D(Collision2D collision)
  {
      if (collision.gameObject.CompareTag("Player"))
      { 
        yield return new WaitForSeconds(0.6f);
        isCollidingWithPlayer = false;
      }

      else if (collision.gameObject.CompareTag("Totem"))
      {
        yield return new WaitForSeconds(0.6f);
        isCollidingWithTotem = false;
      }
  }
}
