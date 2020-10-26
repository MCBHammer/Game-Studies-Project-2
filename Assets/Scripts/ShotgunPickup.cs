using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FireWeapon weaponSwitch = other.GetComponent<FireWeapon>();
            weaponSwitch.shotgunReload();
            weaponSwitch.shotgunSwitch();
            Destroy(this.gameObject);
        }
    }
}
