using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    [SerializeField] Camera cameraController;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float shootDistance = 10f;
    [SerializeField] GameObject visualFeedback;
    [SerializeField] int pistolDamage = 20;
    [SerializeField] LayerMask hitLayers;
    [SerializeField] float pistolCooldown = 0.1f;

    [Header("Gun")]
    [SerializeField] AudioSource _fireSound = null;
    [SerializeField] ParticleSystem _fireParticles = null;

    bool pistolDown = false;

    RaycastHit objectHit;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && pistolDown == false)
        {
            PistolShoot();
        }
    }

    //fire weapon with a raycast
    void PistolShoot()
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
            visualFeedback.transform.position = objectHit.point;
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
}
