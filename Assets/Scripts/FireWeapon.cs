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

    RaycastHit objectHit;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    //fire weapon with a raycast
    void Shoot()
    {
        Vector3 rayDirection = cameraController.transform.forward;
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.yellow, 1f);

        if(Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance, hitLayers))
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
}
