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
    float dist;

    void Start()
    {
        chargeTime = chargeParticles.duration;
        projectileRB = enemyProjectile.GetComponent<Rigidbody>();
        Physics.IgnoreCollision(enemyProjectile.GetComponent<BoxCollider>(), this.gameObject.GetComponent<SphereCollider>());
        //ReadyFire();
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
        dist = Vector3.Distance(targetPlayer.position, this.transform.position);
        if(dist <= 10)
        {
            ReadyFire();
        }
    }

    //Start Fire Sequence
    void ReadyFire()
    {
        if(firing == false && enemyProjectile.activeSelf == false) 
        {
            StartCoroutine(chargeWait());
        }
    }

    IEnumerator chargeWait()
    {
        firing = true;
        chargeParticles.Play();
        yield return new WaitForSeconds(chargeTime);
        Shoot();
        yield return new WaitForSeconds(shotCooldown);
    }

    void Shoot()
    {
        transform.LookAt(targetPlayer);
        StartCoroutine(projectileSpawn());
    }

    IEnumerator projectileSpawn()
    {
        enemyProjectile.SetActive(true);
        enemyProjectile.transform.localPosition = new Vector3(0, 0, 0);
        projectileRB.velocity = Vector3.zero;
        projectileRB.angularVelocity = Vector3.zero;
        projectileRB.AddForce(transform.forward * projectileSpeed);
        yield return new WaitForSeconds(projectileLifetime);
        enemyProjectile.SetActive(false);
        firing = false;
    }
    //End Fire Sequence
}
