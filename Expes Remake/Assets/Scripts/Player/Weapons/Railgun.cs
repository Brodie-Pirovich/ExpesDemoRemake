using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railgun : Weapon
{
    public Transform gunEnd;

    public override void Fire()
    {
        laserLine = GetComponent<LineRenderer>();

        RaycastHit hit;
        Vector3 startPont = weaponCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Vector3 fireDirection = weaponCamera.transform.forward;
        Quaternion fireRotation = Quaternion.LookRotation(fireDirection);
        Quaternion randomRotation = Random.rotation;
        fireRotation = Quaternion.RotateTowards(fireRotation, randomRotation, Random.Range(0.0f, spread));

        // Set the start position for our visual effect for our laser to the position of gunEnd
        laserLine.SetPosition(0, gunEnd.position);

        if (Physics.Raycast(startPont, fireRotation * Vector3.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Debug.DrawRay(startPont, fireRotation * Vector3.forward * hit.distance, Color.green, 2, false);

            Entity target = hit.transform.GetComponent<Entity>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            TakeAmmo(1);
            PlayFireEffects(hit);
        }
        else
        {
            // If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
            laserLine.SetPosition(1, startPont + (weaponCamera.transform.forward * range));
        }
    }

    public override void PlayFireEffects(RaycastHit hit)
    {
        // Set the end position for our laser line 
        laserLine.SetPosition(1, hit.point);

        /*GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactGO, 1f);*/
    }
}
