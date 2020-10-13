using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRifle : Weapon
{
    public int bursts = 3;

    public override void Fire()
    {
        if (audioSource)
        {
            audioSource.Play();
        }

        StartCoroutine(Burst());
    }

    public override void PlayFireEffects(RaycastHit hit)
    {
        /*GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactGO, 1f);*/
    }

    private IEnumerator Burst()
    {
        for (int i = 0; i < bursts; i++)
        {
            RaycastHit hit;

            Vector3 startPont = weaponCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            Vector3 fireDirection = weaponCamera.transform.forward;
            Quaternion fireRotation = Quaternion.LookRotation(fireDirection);
            Quaternion randomRotation = Random.rotation;
            fireRotation = Quaternion.RotateTowards(fireRotation, randomRotation, Random.Range(0.0f, spread));

            if (Physics.Raycast(startPont, fireRotation * Vector3.forward, out hit, range))
            {
                Debug.DrawRay(startPont, fireRotation * Vector3.forward * hit.distance, Color.green, 2, false);

                /*Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }*/
                TakeAmmo(1);
                PlayFireEffects(hit);
            }
            Debug.Log("Burst done");
            yield return new WaitForSeconds(0.1f); 
    }   }
}
