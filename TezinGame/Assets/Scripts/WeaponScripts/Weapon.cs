using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    public Transform firePoint;
    public GameObject bulletPrefab;

    private SpriteRenderer spriteRender;

    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    [Header("Força do Tiro")]
    public float fireForce = 20f;

    public void Fire()
    {
        Debug.Log("Fogo!!");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
   }

   public void SwitchWeapon(Sprite Weapon)
   {
        spriteRender.sprite = Weapon;
   }
}
