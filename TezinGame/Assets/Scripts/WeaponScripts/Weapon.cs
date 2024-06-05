using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{   
    public Transform firePoint;
    public GameObject bulletPrefab;
    
    private GameManager _gameManager;
    private WeaponsStats stats;
    private SpriteRenderer spriteRender;
    private GameObject firepoint;

    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        stats = FindObjectOfType<WeaponsStats>();
        firepoint = GameObject.FindWithTag("FirePoint");

        _gameManager = FindObjectOfType<GameManager>();
    }

    [Header("Força do Tiro")]
    public float fireForce = 20f;

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

            if (firepoint.transform.position == new UnityEngine.Vector3(1.16f, 0.54f, 0f))
            {
                firepoint.transform.position = new UnityEngine.Vector3(firepoint.transform.position.x + 0.27f, firepoint.transform.position.y, 0f);
            }

            if (Weapon == "M4")
            {
                spriteRender.sprite = _gameManager.M4Weapon;
            } else if (Weapon == "Pistol"){

                spriteRender.sprite = _gameManager.PistolWeapon;
            }
          
        }
        else
        {
            Debug.LogError("Weapon not found: " + Weapon);
        }
   }
}
