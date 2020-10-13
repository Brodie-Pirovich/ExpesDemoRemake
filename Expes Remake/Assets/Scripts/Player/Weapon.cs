using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    public Camera weaponCamera;

    [SerializeField]
    public AudioSource audioSource;

    private float shootElapsed;

    [Header("Weapon Stats")]
    private float shootDelayed;
    public float shotsPerMinute;
    public int damage;
    public float spread;
    public float range;
    public int currentAmmo;
    public string ammoTag;
    public string weaponName;

    [SerializeField]
    public GameObject impactEffect = null;


    [SerializeField]
    public Player m_playerStats;

    // Start is called before the first frame update
    void Awake()
    {
        shootDelayed = shotsPerMinute / 60.0f;
    }

    // Update is called once per frame
    void Update()
    {
        shootElapsed += Time.deltaTime;
        //Debug.Log(shootElapsed);
        GetAmmoType();

        if (Input.GetButton("Fire1") && shootElapsed >= shootDelayed)
        {
            if (currentAmmo > 0)
            {
                shootElapsed = 0.0f;
                Fire();
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            weaponCamera.fieldOfView = 40;

        }
        if(Input.GetButtonUp("Fire2"))
        {
            weaponCamera.fieldOfView = 60;
        }
    }

    public virtual void Fire()
    {

    }

    public virtual void PlayFireEffects(RaycastHit hit)
    {

    }

    public void GetAmmoType()
    {
       /* switch (ammoTag)
        {
            case "bullet":
                m_CurrentAmmo = m_playerStats.bullets;
                break;
            case "shells":
                m_CurrentAmmo = m_playerStats.shells;
                break;
            case "rocket":
                m_CurrentAmmo = m_playerStats.rockets;
                break;
            case "cells":
                m_CurrentAmmo = m_playerStats.cells;
                break;
            case "slugs":
                m_CurrentAmmo = m_playerStats.slugs;
                break;
        }*/
    }

    public void TakeAmmo(int amount)
    {
        switch (ammoTag)
        {
            case "bullet":
                m_playerStats.bullets -= amount;
                break;
            case "shells":
                m_playerStats.shells -= amount;
                break;
            case "rocket":
                m_playerStats.rockets -= amount;
                break;
            case "cells":
                m_playerStats.cells -= amount;
                break;
            case "slugs":
                m_playerStats.slugs -= amount;
                break;
        }
    }
}
