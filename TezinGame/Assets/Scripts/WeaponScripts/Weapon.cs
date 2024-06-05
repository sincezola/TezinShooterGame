using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public bool canSpamShoots = false;

    private GameManager _gameManager;
    private WeaponsStats stats;
    private SpriteRenderer spriteRender;
    private GameObject firepoint;
    private Coroutine fireCoroutine;
    private string currentWeapon = "Pistol";

    [Header("Força do Tiro")]
    public float fireForce = 20f;
    [Header("Cadência de Tiro (balas por segundo)")]
    public float fireRate = 10f;


    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        stats = FindObjectOfType<WeaponsStats>();
        firepoint = GameObject.FindWithTag("FirePoint");

        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {   
        if (canSpamShoots)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (fireCoroutine == null)
                {
                    fireCoroutine = StartCoroutine(FireContinuously());
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (fireCoroutine != null)
                {
                    StopCoroutine(fireCoroutine);
                    fireCoroutine = null;
                }
            }
        }

    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(1f / fireRate);
        }
    }

    public void Fire()
    {
        Debug.Log("Fogo!!");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }

    public void SwitchWeapon(string Weapon)
    {
        if (stats.allStats.ContainsKey(Weapon))
        {
            transform.localScale = stats.allStats[Weapon]["Scale"];

            if (firepoint.transform.position == new Vector3(1.16f, 0.54f, 0f))
            {
                firepoint.transform.position = new Vector3(firepoint.transform.position.x + 0.27f, firepoint.transform.position.y, 0f);
            }

            if (Weapon == "M4")
            {
                spriteRender.sprite = _gameManager.M4Weapon;

                canSpamShoots = true;
                currentWeapon = "M4";
            }
            else if (Weapon == "Pistol")
            {
                spriteRender.sprite = _gameManager.PistolWeapon;

                canSpamShoots = false;
                currentWeapon = "Pistol";
            }
        } else
        {
            Debug.LogError("Weapon not found: " + Weapon);
        }

    }

    public int CurrentWeaponForce()
    {
        if (currentWeapon == "M4")
        {
            return 2;
        } else if (currentWeapon == "Pistol"){
            return 1;
        }

        else
        {   
            Debug.Log("Arma desconhecida, alterando dano para 1");
            return 1;
        }
    }   
}
