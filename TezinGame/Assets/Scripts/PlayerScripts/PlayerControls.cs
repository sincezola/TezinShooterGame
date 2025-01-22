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

    [Header("Cooldown")]
    public bool Cooldown;
    
    // Private Variables
    private Droper droper;
    private float timer;
    private bool isFlashLightOn = true;
    private Rigidbody2D rb;
    private Weapon Weapon;
    private GameManager gameManager;
    private Totem totem;
    private bool tookObject;
    private Collider2D currentDroppedItem;
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

        if (currentDroppedItem != null && Input.GetKeyDown(KeyCode.F))
        {
            tookObject = false;
            switch (currentDroppedItem.gameObject.tag)
            {
                case "DroppedM4":
                    if (!Weapon.isReloading)
                    {   
                        Debug.Log("Took M4");

                        List<float> m4Info = Weapon.getWeaponInfo("M4");

                        Weapon.SwitchWeapon("M4");
                        bulletsTXT.text = m4Info[0] + "/" + m4Info[1];

                        tookObject = true;
                    }
                    break;

                case "DroppedPistol":
                    if (!Weapon.isReloading)
                    {
                        Debug.Log("Took Pistol");

                        List<float> pistolInfo = Weapon.getWeaponInfo("Pistol");

                        Weapon.SwitchWeapon("Pistol");
                        bulletsTXT.text = pistolInfo[0] + "/" + pistolInfo[1];

                        tookObject = true;
                    }
                    break;

                case "DroppedTotem": 
                    if (gameManager.totemsHud < gameManager.maxTotemsPlaced)
                    {   
                        Debug.Log("Took Totem");
                        gameManager.IncreaseTotemsHud();
                        totem.UpdateTotemHud();
                        tookObject = true;
                    }
                    break;

                default:
                    break;
            }

            if (tookObject)
            {
                Destroy(currentDroppedItem.gameObject);
                currentDroppedItem = null;
            }
        }
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
            StartCoroutine(fireCooldown(Weapon.currentWeapon));
        }
    }

    private IEnumerator fireCooldown(string weaponName)
    {
        Cooldown = true;
 
        float cooldown = Weapon.getWeaponInfo(Weapon.currentWeapon, false, true)[0];

        yield return new WaitForSeconds(cooldown);

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DroppedM4") || other.CompareTag("DroppedPistol") || other.CompareTag("DroppedTotem"))
        {
            currentDroppedItem = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == currentDroppedItem)
        {
            currentDroppedItem = null;
        }
    }
}
