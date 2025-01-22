using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{

    [Header("Totem Info")]
    public GameObject totemHud;
    public GameObject totemHud2;
    public GameObject totemPrefab;
    public int health = 4;
    public List<Sprite> spriteList;

    [Header("Health Bar Settings")]
    public Transform healthBar; // Barra verde
    private Vector3 healthBarScale; // Tamanho da barra
    private float healthPercent; // Percentual de vida para calculo
    
    private bool isCollidingWithEnemy = false;
    private SpriteRenderer spriteRenderer;
    private GameManager manager;
    private float timer;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        healthBarScale = healthBar.localScale;
        healthPercent = healthBarScale.x / health;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteList[0];

        UpdateTotemHud();
    }

    private void Update()
    {
        CatchCall();
        TotemDamageSystem();
    }

    private void TotemDamageSystem()
    {
        if (isCollidingWithEnemy)
        {
            timer += Time.deltaTime;

            if (isCollidingWithEnemy && timer >= 1.0f)
            {
                health--;

                if (health != 0) spriteRenderer.sprite = spriteList[health];

                healthBarScale.x = healthPercent * health;
                healthBar.localScale = healthBarScale;

                CheckTotemLife();
                timer = 0;
            }
        };
    }

    private void CatchCall()
    {
        if (Input.GetKeyUp(KeyCode.G) && manager.totemsHud > 0 && manager.totemCount < manager.maxTotemsPlaced)
        {   
            ActiveTotem();
        };
    }

    private void ActiveTotem()
    {
        Debug.Log("Activing Totem");

        Vector3 playerPos = manager.playerTransform.position;
        Instantiate(totemPrefab, playerPos, Quaternion.identity);

        
        manager.DecreaseTotemsHud();
        manager.IncreaseTotemCount();

        UpdateTotemHud();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            health--;

            if (health == 3) spriteRenderer.sprite = spriteList[3];

            healthBarScale.x = healthPercent * health;
            healthBar.localScale = healthBarScale;

            CheckTotemLife();
        };
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isCollidingWithEnemy = true;
        };
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isCollidingWithEnemy = false;
            timer = 0;
        };
    }

    private void CheckTotemLife()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            manager.DecreaseTotemCount();
        };
    }

    public void UpdateTotemHud()   
    {
        if (manager.totemsHud == 2)
        {
            totemHud.SetActive(true);
            totemHud2.SetActive(true);
        }
        else if (manager.totemsHud == 1)
        {
            totemHud.SetActive(true);
            totemHud2.SetActive(false);
        }
        else
        {
            totemHud.SetActive(false);
            totemHud2.SetActive(false);
        };
    }
}
