using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

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
    private string bulletString = "8/16";

    [Header("Bullet")]
    public TextMeshProUGUI bulletMark;
    [Header("Força do Tiro")]
    public float fireForce = 20f;
    [Header("Cadência de Tiro (balas por segundo)")]
    public float fireRate = 10f;
    [Header("Reloading Text")]
    public GameObject reloadingText;

    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        stats = FindObjectOfType<WeaponsStats>();
        firepoint = GameObject.FindWithTag("FirePoint");

        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        reloadingText.SetActive(false);
    }

    private void Update()
    {   
        if (canSpamShoots) // Spam shoots logic
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

        if (Input.GetKeyDown(KeyCode.R)) StartCoroutine(reloadWeapon(currentWeapon)); // Reload the weapon when player press 'R'
    }

    private IEnumerator showReloadingText()
    {
        if (reloadingText != null)
        {
            if (!reloadingText.activeSelf) reloadingText.SetActive(true);

            yield return new WaitForSeconds(1.5f);

            reloadingText.SetActive(false);
        };
    }

    private void refreshBulletValues()
    {
        bulletString = bulletMark.text;
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(1f / fireRate);
        }
    }
    
    private void decreaseBullet() 
    {
        Debug.Log("Decreasing bullet of " + currentWeapon);
        refreshBulletValues();

        int slashPos = bulletString.IndexOf('/');

        int comb = int.Parse(bulletString.Substring(0, slashPos)); // All numbers before slash (/)
        int reserve = int.Parse(bulletString.Substring(slashPos + 1)); // All numbers before slash (/)

        if ( slashPos == -1 )
        {
            throw new System.Exception("Falta de '/' em bullet mark!");
        }

        else if (comb < 1 && reserve < 1) bulletMark.text = "0/0"; // Acabou a bala do pente e da reserva

        else comb--;

        if (comb == 0) 
        {
            bulletMark.text = "0/" + reserve;
            refreshBulletValues();
            StartCoroutine(reloadWeapon(currentWeapon));
        };

        bulletMark.text = comb.ToString() + "/" + reserve.ToString();
    }

    private List<int> getWeaponInfo(string weapon)
    {
        List<int> infos = new List<int>();

        if (stats.allStats.ContainsKey(weapon))
        {
            infos.Add(stats.bulletStats[weapon]["Comb"]);
            infos.Add(stats.bulletStats[weapon]["Reserve"]);
        };

        return infos;
    }

    private IEnumerator reloadWeapon(string weaponName)
    {   
        Debug.Log("Reloading " + weaponName);

        int slashPos = bulletString.IndexOf('/');

        if ( slashPos == -1 )
        {
            throw new System.Exception("Falta de '/' em bullet mark!");
        };

        int comb = int.Parse(bulletString.Substring(0, slashPos));
        int reserve = int.Parse(bulletString.Substring(slashPos + 1));

        int weaponComb = getWeaponInfo(weaponName)[0];
    
        if (comb < weaponComb && reserve >= 1) 
        {   
            StartCoroutine(showReloadingText());
            yield return new WaitForSeconds(1.5f);

            int missingBullets = weaponComb - comb;
            while (missingBullets >= 1)
            {   
                if (reserve == 0 ) break;

                reserve--;

                comb++;
                missingBullets--;

                bulletMark.text = comb + "/" + reserve;
            };
        };

        refreshBulletValues();
    } 

    private bool canShoot()
    {   
        int slashPos = bulletString.IndexOf('/');

        if ( slashPos == -1 )
        {
            throw new System.Exception("Falta de '/' em bullet mark!");
        };

        int comb = int.Parse(bulletString.Substring(0, slashPos));

        if (comb > 0) return true;

        return false;
    }

    public void Fire()
    {
        Debug.Log("Trying to shoot");

        if (canShoot())
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);

            decreaseBullet(); // Diminui as balas
            refreshBulletValues();
        }
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

        refreshBulletValues();
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
