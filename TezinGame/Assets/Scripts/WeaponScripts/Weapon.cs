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
    private PlayerControls controls;
    private string bulletString;

    [Header("Weapon")]
    public string currentWeapon = "Pistol";

    [Header("Bullet")]
    public TextMeshProUGUI bulletMark;

    [Header("Cadência de Tiro (balas por segundo)")]
    public float fireRate = 10f;

    [Header("Reloading Info")]
    public GameObject reloadingText;
    public bool isReloading = false;
    
    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        stats = FindObjectOfType<WeaponsStats>();
        firepoint = GameObject.FindWithTag("FirePoint");
        controls = FindObjectOfType<PlayerControls>();

        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        reloadingText.SetActive(false);
        
        var weaponInfo = getWeaponInfo(currentWeapon);
        bulletMark.text = weaponInfo[0] + "/" + weaponInfo[1];

        refreshBulletValues();
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

    private IEnumerator showReloadingText(float time)
    {
        if (reloadingText != null)
        {
            if (!reloadingText.activeSelf) reloadingText.SetActive(true);

            yield return new WaitForSeconds(time);

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

    public List<float> getWeaponInfo(string weapon, bool wannaReloadTime = false, bool wannaFireRate = false, bool wannaFireSpeed = false)
    {
        List<float> infos = new List<float>();

        if (stats.allStats.ContainsKey(weapon))
        {   
            if (wannaFireRate)
            {
                infos.Add(stats.fireRate[weapon]);
            }

            else if (wannaReloadTime)
            {
                infos.Add(stats.reloadTime[weapon]);
            }
            
            else if (wannaFireSpeed)
            {
                infos.Add(stats.fireSpeed[weapon]);
            }

            else
            {
                infos.Add(stats.bulletStats[weapon]["Comb"]);
                infos.Add(stats.bulletStats[weapon]["Reserve"]);
            };
        }

        else
        {
            Debug.LogError("Gun " + weapon + " cannot be find");
        };

        return infos;
    }

    private IEnumerator reloadWeapon(string weaponName)
    {   
        int slashPos = bulletString.IndexOf('/');

        if ( slashPos == -1 )
        {
            throw new System.Exception("Falta de '/' em bullet mark!");
        };

        int comb = int.Parse(bulletString.Substring(0, slashPos));
        int reserve = int.Parse(bulletString.Substring(slashPos + 1));

        float weaponComb = getWeaponInfo(weaponName)[0];

        if (comb != weaponComb && comb < weaponComb && reserve >= 1)
        {   
            Debug.Log("Reloading " + weaponName);
            isReloading = true;

            StartCoroutine(showReloadingText(getWeaponInfo(weaponName, true)[0]));
            yield return new WaitForSeconds(getWeaponInfo(weaponName, true)[0]);

            float missingBullets = weaponComb - comb;
            while (missingBullets >= 1)
            {   
                if (reserve == 0 ) break;

                reserve--;

                comb++;
                missingBullets--;

                bulletMark.text = comb + "/" + reserve;
            };
        };

        isReloading = false;
        refreshBulletValues();
    }    

    private bool canShoot()
    {   
        int slashPos = bulletString.IndexOf('/');

        if ( slashPos == -1 )
        {
            throw new System.Exception("Falta de '/' em bullet mark!");
        }

        int comb = int.Parse(bulletString.Substring(0, slashPos));

        if (comb > 0 && !isReloading && !controls.Cooldown) return true;

        return false;
    }

    public void Fire()
    {
        Debug.Log("Trying to shoot");

        if (canShoot())
        {   

            float fireForce = getWeaponInfo(currentWeapon, false, false, true)[0];
            
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);

            decreaseBullet(); // Diminui as balas
            refreshBulletValues();
        }
    }

    public void SwitchWeapon(string Weapon)
    {
        if (stats.allStats.ContainsKey(Weapon) && !isReloading)
        {
            transform.localScale = stats.allStats[Weapon]["Scale"];

            if (firepoint.transform.position == new Vector3(1.16f, 0.54f, 0f))
            {
                firepoint.transform.position = new Vector3(firepoint.transform.position.x + 0.27f, firepoint.transform.position.y, 0f);
            }

            if (Weapon == "M4")
            {
                spriteRender.sprite = _gameManager.M4Weapon;

                List<float> m4Info = getWeaponInfo("M4");
                bulletMark.text = m4Info[0] + "/" + m4Info[1];

                canSpamShoots = true;
                currentWeapon = "M4";
            }
            else if (Weapon == "Pistol")
            {
                spriteRender.sprite = _gameManager.PistolWeapon;
                
                List<float> pistolInfo = getWeaponInfo("Pistol");
                bulletMark.text = pistolInfo[0] + "/" + pistolInfo[1];

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
        } 

        else if (currentWeapon == "Pistol"){
            return 1;
        }

        else
        {   
            Debug.Log("Arma desconhecida, alterando dano para 1");
            return 1;
        };
    }   
}
