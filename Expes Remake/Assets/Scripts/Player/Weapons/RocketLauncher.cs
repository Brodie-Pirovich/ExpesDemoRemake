using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    public Transform rocket;
    public Transform muzzle;

    public override void Fire()
    {
        Vector3 startPont = weaponCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Vector3 fireDirection = weaponCamera.transform.forward;
        Quaternion fireRotation = Quaternion.LookRotation(fireDirection);

        Transform tempRocket = Instantiate(rocket, muzzle.position, fireRotation);
        tempRocket.GetComponent<Rocket>().Setup(fireDirection);
        //Destroy(tempRocket, 3);
    }

    public override void PlayFireEffects(RaycastHit hit)
    {
        // Play the shooting sound effect
        if (audioSource)
        {
            audioSource.Play();
        }
    }

}
