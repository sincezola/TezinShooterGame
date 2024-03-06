using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{   
    public Light lanterna;

    private bool Cooldown;
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

        lanterna.transform.position = new Vector3(gameManager.playerTransform.position.x, gameManager.playerTransform.position.y, -3);
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
            StartCoroutine(fireCooldown());
        }
    }

    private IEnumerator fireCooldown()
    {
        Cooldown = true;

        yield return new WaitForSeconds(0.1f);

        Cooldown = false;
    }
    
    private void CatchAxis()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            gameManager.DecreasePlayerHP(2);
        }
    }
}
