using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] ParticleSystem chargeParticles;
    [SerializeField] Transform targetPlayer;
    [SerializeField] GameObject enemyProjectile;

    float chargeTime;
    [SerializeField] float shotCooldown = 1f;
    [SerializeField] float projectileLifetime = 2f;
    [SerializeField] float projectileSpeed = 10f;
    bool firing = false;

    Rigidbody projectileRB;
    Vector3 startingPosition;

    void Start()
    {
        chargeTime = chargeParticles.duration;
        startingPosition = enemyProjectile.transform.position;
        projectileRB = enemyProjectile.GetComponent<Rigidbody>();
        ReadyFire();
    }

    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {

    }

    //Start Fire Sequence
    void ReadyFire()
    {
        if(firing == false) 
        {
            StartCoroutine(chargeWait());
        }
    }

    IEnumerator chargeWait()
    {
        yield return new WaitForSeconds(3f);
        firing = true;
        chargeParticles.Play();
        yield return new WaitForSeconds(chargeTime);
        Shoot();
        yield return new WaitForSeconds(shotCooldown);
        firing = false;
    }

    void Shoot()
    {
        transform.LookAt(targetPlayer);
        StartCoroutine(projectileSpawn());
    }

    IEnumerator projectileSpawn()
    {
        enemyProjectile.SetActive(true);
        enemyProjectile.transform.position = startingPosition;
        projectileRB.AddForce(transform.forward * projectileSpeed);
        yield return new WaitForSeconds(projectileLifetime);
        enemyProjectile.SetActive(false);
    }
    //End Fire Sequence
}
