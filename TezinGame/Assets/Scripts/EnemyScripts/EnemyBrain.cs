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
    private Coroutine damageCoroutine;

    [Header("Totems")]
    public Transform totem1;
    public Transform totem2;

    [Header("Damage")]
    public float damage = 1.0f;

    [Header("Health Bar Settings")]
    public Transform healthBar;
    
    private Vector3 healthBarScale;
    private float healtPercent;
    private Transform enemyObjective;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        spawner = FindObjectOfType<EnemySpawner>();
    }
    private void Start()
    {
        healthBarScale = healthBar.localScale;
        healtPercent = healthBarScale.x / enemyHp;

        enemyObjective = gameManager.playerTransform;
    }

    private void Update()
    {
        MoveEnemy();
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.gameObject.tag == "Bullet")
        {
            enemyHp--;
            AdjustEnemyHealthBar();

            if (enemyHp <= 0)
            {
                Destroy(gameObject);
                spawner.EnemyDied();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (damageCoroutine == null)
            {
                isCollidingWithPlayer = true;
                damageCoroutine = StartCoroutine(DamagePlayerOverTime());
            }
        }
        else if (collision.gameObject.CompareTag("Totem"))
        {
            isCollidingWithTotem = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            isCollidingWithPlayer = false;

            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
        else if (collision.gameObject.CompareTag("Totem"))
        {
            isCollidingWithTotem = false;
        }
    }

    private IEnumerator DamagePlayerOverTime()
    {   
        while (isCollidingWithPlayer)
        {   
            gameManager.DecreasePlayerHP(damage);
            yield return new WaitForSeconds(1f);
        }
    }

    public void AdjustEnemyHealthBar()
    {
        healthBarScale.x = healtPercent * enemyHp;
        healthBar.localScale = healthBarScale; 
    }

    private void MoveEnemy()
    {   
        if (!gameManager.playerTransform) enabled = false;

        if (!isCollidingWithTotem && !isCollidingWithPlayer)
        {
            if (totem1 == null && totem2 == null)
            {
                transform.position = Vector2.MoveTowards(transform.position, enemyObjective.position, gameManager.enemySpeed * Time.deltaTime);
            }

            else if ( totem1 != null )
            {
                transform.position = Vector2.MoveTowards(transform.position, totem1.position, gameManager.enemySpeed * Time.deltaTime);
            }

            else
            {
                transform.position = Vector2.MoveTowards(transform.position, totem2.position, gameManager.enemySpeed * Time.deltaTime);
            };
        };
    }

    public void ChangeEnemyObjective(Transform where)
    {
        enemyObjective = where;
    }
}