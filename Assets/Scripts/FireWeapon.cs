using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireWeapon : MonoBehaviour
{
    [SerializeField] Camera cameraController;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float shootDistance = 10f;
    [SerializeField] LayerMask hitLayers;
    [SerializeField] GameObject hitLightPrefab;

    [Header("Pistol")]
    [SerializeField] GameObject pistol;
    [SerializeField] AudioSource _fireSound = null;
    [SerializeField] ParticleSystem _fireParticles = null;
    [SerializeField] float pistolCooldown = 0.1f;
    [SerializeField] int pistolDamage = 20;
    bool pistolDown = false;

    [Header("Shotgun")]
    [SerializeField] GameObject shotgun;
    [SerializeField] AudioSource _fireSoundShotgun = null;
    [SerializeField] ParticleSystem _fireParticlesShotgun = null;
    [SerializeField] Text shotgunAmmoBoard = null;
    [SerializeField] float shotgunCooldown = 1f;
    [SerializeField] int shotgunDamage = 5;
    [SerializeField] int shotgunShots = 10;
    [SerializeField] int shotgunAmmoMax = 10;
    [SerializeField] float shotgunRecoil = 10;
    int shotgunAmmo;
    bool shotgunDown = false;

    [Header("Melee")]
    [SerializeField] Animator pistolWhip;
    [SerializeField] Animator shotgunWhip;
    [SerializeField] int meleeDamage = 100;
    [SerializeField] float punchDistance = 1f;
    bool melee = false;

    float randomRange = 10f;
    RaycastHit objectHit;

    Rigidbody playerRB;
    Vector3 startingPosition;
    Vector3 aimingPosition;

    void Start()
    {
        shotgunAmmo = shotgunAmmoMax;
        startingPosition = pistol.transform.localPosition;
        aimingPosition.Set(0, -0.25f, 0.32f);
        playerRB = this.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(melee == false)
        {
            if (Input.GetMouseButtonDown(0) && pistolDown == false && pistol.active == true)
            {
                pistolShoot();
            }
            if (Input.GetMouseButtonDown(0) && shotgunDown == false && shotgun.active == true)
            {
                shotgunShoot();
            }
            if (Input.GetMouseButton(1))
            {
                pistolWhip.Play("PistolScope");
                shotgunWhip.Play("ShotgunScope");
            }
            if (Input.GetMouseButtonUp(1))
            {
                pistolWhip.Play("IdlePistol");
                shotgunWhip.Play("IdleShotgun");
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(meleeAnim());
            }
        }
    }

    //fire weapon with a raycast
    void pistolShoot()
    {
        Vector3 rayDirection = cameraController.transform.forward;
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.yellow, 1f);
        _fireSound.Play();
        _fireParticles.Play();
        StartCoroutine(pistolWait());

        if (Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance, hitLayers))
        {
            if(objectHit.transform.tag == "Enemy")
            {
                EnemyShooter enemyShooter = objectHit.transform.gameObject.GetComponent<EnemyShooter>();
                if (enemyShooter != null)
                {
                    enemyShooter.TakeDamage(pistolDamage);
                }
            }
            Debug.Log("You hit the " + objectHit.transform.name);
            GameObject light = (GameObject)Instantiate(hitLightPrefab, objectHit.point, Quaternion.identity);
            Destroy(light, 1f);
        } else
        {
            Debug.Log("Miss");
        }
    }

    IEnumerator pistolWait()
    {
        pistolDown = true;
        yield return new WaitForSeconds(pistolCooldown);
        pistolDown = false;
        
    }

    void shotgunShoot()
    {
        _fireSoundShotgun.Play();
        _fireParticlesShotgun.Play();
        StartCoroutine(shotgunWait());
        shotgunAmmo--;
        shotgunAmmoBoard.text = ("Shotgun Ammo: " + shotgunAmmo);
        playerRB.AddForce(cameraController.transform.forward * -shotgunRecoil);

        for(int i = 0; i < shotgunShots; i++)
        {
            Vector3 rayDirection = Quaternion.Euler(Random.Range(-randomRange, randomRange), Random.Range(-randomRange, randomRange), Random.Range(-randomRange, randomRange)) * cameraController.transform.forward;
            Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.yellow, 1f);
            if (Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance, hitLayers))
            {
                if (objectHit.transform.tag == "Enemy")
                {
                    Debug.Log("You hit the " + objectHit.transform.name);
                    EnemyShooter enemyShooter = objectHit.transform.gameObject.GetComponent<EnemyShooter>();
                    if (enemyShooter != null)
                    {
                        enemyShooter.TakeDamage(shotgunDamage);
                    }
                }
                GameObject light = (GameObject)Instantiate(hitLightPrefab, objectHit.point, Quaternion.identity);
                Destroy(light, 1f);
            }
            else
            {
                //Debug.Log("Miss");
            }
        }
        shotgunSwitch();
    }

    void MeleeHit()
    {
        Vector3 rayDirection = cameraController.transform.forward;
        Debug.DrawRay(rayOrigin.position, rayDirection * punchDistance, Color.yellow, 1f);

        if (Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance, hitLayers))
        {
            if (objectHit.transform.tag == "Enemy")
            {
                EnemyShooter enemyShooter = objectHit.transform.gameObject.GetComponent<EnemyShooter>();
                if (enemyShooter != null)
                {
                    enemyShooter.TakeDamage(meleeDamage);
                }
            }
            Debug.Log("You hit the " + objectHit.transform.name);
        }
    }

    IEnumerator shotgunWait()
    {
        shotgunDown = true;
        yield return new WaitForSeconds(pistolCooldown);
        shotgunDown = false;
    }

    IEnumerator meleeAnim()
    {
        melee = true;
        if(pistol.active == true)
        {
            pistolWhip.Play("PistolWhip");
            yield return new WaitForSeconds(0.25f);
            MeleeHit();
            yield return new WaitForSeconds(0.75f);
            pistolWhip.Play("IdlePistol");

        }
        if (shotgun.active == true)
        {
            shotgunWhip.Play("ShotgunWhip");
            yield return new WaitForSeconds(0.25f);
            MeleeHit();
            yield return new WaitForSeconds(0.75f);
            shotgunWhip.Play("IdleShotgun");
        }
        melee = false;
    }

    public void shotgunSwitch()
    {
        if(shotgunAmmo > 0)
        {
            pistol.SetActive(false);
            shotgun.SetActive(true);
            shotgunAmmoBoard.gameObject.SetActive(true);
            shotgunAmmoBoard.text = ("Shotgun Ammo: " + shotgunAmmo);
        }
        if(shotgunAmmo <= 0)
        {
            pistol.SetActive(true);
            shotgun.SetActive(false);
            shotgunAmmoBoard.gameObject.SetActive(false);
        }
    }

    public void shotgunReload()
    {
        shotgunAmmo = shotgunAmmoMax;
    }
}
