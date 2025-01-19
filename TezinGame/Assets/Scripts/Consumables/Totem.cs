using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{

    [Header("Totem Info")]
    public bool isTotemAvailable = false;
    public GameObject totemPrefab;
    public int health = 5;

    [Header("Health Bar Settings")]
    public Transform healthBar; // Barra verde

    private Vector3 healthBarScale; // Tamanho da barra
    private float healthPercent; // Percentual de vida para calculo
    private bool isCollidingWithEnemy = false;

    private GameManager manager;
    private float timer;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        healthBarScale = healthBar.localScale;
        healthPercent = healthBarScale.x / health;
    }

    private void Update()
    {
        CatchCall();

        if (isCollidingWithEnemy) // For collisions
        {
            timer += Time.deltaTime;

            if (timer >= 1.0f)
            {
                Debug.Log("Tomei dano!");

                health--;
                healthBarScale.x = healthPercent * health;
                healthBar.localScale = healthBarScale;

                CheckTotemLife();
                timer = 0;
            }
        }
    }

    private void CatchCall()
    {
        if (Input.GetKeyUp(KeyCode.G) && isTotemAvailable)
        {
            ActiveTotem();
        }
    }

    private void ActiveTotem()
    {
        Debug.Log("Activing Totem");

        Vector3 playerPos = manager.playerTransform.position;
        Instantiate(totemPrefab, playerPos, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Tomei dano!");

            health--;
            healthBarScale.x = healthPercent * health;
            healthBar.localScale = healthBarScale;

            CheckTotemLife();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isCollidingWithEnemy = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isCollidingWithEnemy = false;
            timer = 0; // Reseta o timer se sair da colisão
        }
    }

    private void CheckTotemLife()
    {
        if (health <= 0) Destroy(totemPrefab);
    }
}
