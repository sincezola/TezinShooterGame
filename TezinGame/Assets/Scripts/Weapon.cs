using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    [Header("Objetos")]
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("Força do Tiro")]
    public float fireForce = 20f;

    public void Fire()
    {
        Debug.Log("Fogo!!");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
   }
}
