using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    [SerializeField] Camera cameraController;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float shootDistance = 10f;

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

        if(Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance))
        {
            Debug.Log("You hit the " + objectHit.transform.name);
        } else
        {
            Debug.Log("Miss");
        }
    }
}
