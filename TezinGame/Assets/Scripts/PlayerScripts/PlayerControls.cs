using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{   
    public Light lanterna;
    public GameObject lanternaObject;

    // Private Variables
    private bool Cooldown;
    private bool isFlashLightOn = true;
    private Rigidbody2D rb;
    private Weapon Weapon;
    private GameManager gameManager;

    Vector2 moveDirection;

    private void Awake()
    {
        Weapon = FindObjectOfType<Weapon>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        CapturarBotaoDoMouse();
        CatchAxis();
        CapturarF();

        if(!gameManager.isPlayerAlive())
        {
            Destroy(lanterna);
        }

        lanterna.transform.position = new Vector3(gameManager.playerTransform.position.x, gameManager.playerTransform.position.y, -1.39f);
    }
    
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * gameManager.playerSpeed, moveDirection.y * gameManager.playerSpeed);
    }
 
    private void CapturarBotaoDoMouse()
    {
        if(Input.GetMouseButtonDown(0) && !Cooldown)
        {
            Weapon.Fire();
            StartCoroutine(fireCooldown(0.1f));
        }
    }

    private IEnumerator fireCooldown(float HowMuchCooldown)
    {
        Cooldown = true;

        yield return new WaitForSeconds(HowMuchCooldown);

        Cooldown = false;
    }
    
    private void CatchAxis()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    private void CapturarF()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            ChangeFlashLightState();
        }
    }

    private void ChangeFlashLightState()
    {
        if(!isFlashLightOn)
        {
            isFlashLightOn = true;
        }

        else
        {
            isFlashLightOn = false;
        }

        lanternaObject.SetActive(isFlashLightOn);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            gameManager.DecreasePlayerHP(1);
        }
    }
}
