using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{   
    [Header("Flashlight Options")]
    public Light lanterna;
    public GameObject lanternaObject;

    [Header("Bullet Text")]
    public TextMeshProUGUI bulletsTXT;
    
    // Private Variables
    private bool Cooldown;
    private Droper droper;
    private float timer;
    private bool isFlashLightOn = true;
    private Rigidbody2D rb;
    private Weapon Weapon;
    private GameManager gameManager;
    private Totem totem;

    Vector2 moveDirection;

    private void Awake()
    {
        Weapon = FindObjectOfType<Weapon>();
        droper = FindObjectOfType<Droper>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        totem = FindObjectOfType<Totem>();
    }

    private void Update()
    {
        CapturarBotaoDoMouse();
        CatchAxis();
        CapturarTeclas();

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

    private void CapturarTeclas()
    {
        if(Input.GetKeyDown(KeyCode.E))
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

    private void OnCollisionStay2D(Collision2D other)
    {   
        if (other.gameObject.CompareTag("Enemy"))
        {    
            timer += Time.deltaTime;

            if (timer >= 1.0f)
            {
                gameManager.DecreasePlayerHP(1);
                timer = 0;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            timer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {   
            Debug.Log("Colidi");
            gameManager.DecreasePlayerHP(Weapon.CurrentWeaponForce());
        }
    }

    private void OnTriggerStay2D(Collider2D other) // Dropped Objects
    {
        // Took a dropped weapon
        
        if(Input.GetKeyDown(KeyCode.F)) // Interacting to a dropped item
        {
            switch (other.gameObject.tag)
            {
                case "DroppedM4":

                    Debug.Log("Took M4");

                    List<int> m4Info = Weapon.getWeaponInfo("M4");

                    Weapon.SwitchWeapon("M4");
                    bulletsTXT.text = m4Info[0] + "/" + m4Info[1];
                    break;

                case "DroppedPistol":
                    
                    Debug.Log("Took Pistol");

                    List<int> pistolInfo = Weapon.getWeaponInfo("Pistol");

                    Weapon.SwitchWeapon("Pistol");
                    bulletsTXT.text = pistolInfo[0] + "/" + pistolInfo[1];

                    break;

                case "DroppedTotem": 

                    totem.isTotemAvailable = true;

                    break;

                default:
                    break;
            };

            Destroy(other.gameObject);
        }
    }
}