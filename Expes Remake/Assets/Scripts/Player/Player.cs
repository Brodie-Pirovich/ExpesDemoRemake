using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    public float health = 100;
    public float maxHealth = 200;
    public float armor = 0;
    public float maxArmor = 200;

    [Header("Ammo")]
    public float bullets = 50;
    public float shells = 25;
    public float rockets = 0;
    public float cells = 0;
    public float slugs = 0;
    public enum ammotype { bullets, shells, rockets, cells, slugs};
    [SerializeField]
    public ammotype currentWeaponAmmo;

    [Header("Keys")]
    public bool b_hasBlue = false;
    public bool b_hasRed = false;
    public bool b_hasYellow = false;

    [Header("UI")]
    public GameObject blueKeyUI;
    public GameObject redKeyUI;
    public GameObject yellowKeyUI;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI ammoText;
    public GameObject HUD;

    // variables to check if a weapon has already been picked up //
    private bool b_hasShotgun;
    private bool b_hasSuperShotgun;
    private bool b_hasBattleRifle;
    private bool b_hasChaingun;
    private bool b_hasRocketLauncher;
    private bool b_hasFlamegun;
    private bool b_hasRailgun;

    // Start is called before the first frame update
    void Start()
    {
        healthText.SetText(health.ToString());
        armorText.SetText(armor.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHealth(float amount)
    {
        float Temp = health + amount;

        if (Temp >= maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health = Temp;
        }

        UpdateHealth();
    }

    public void AddArmor(float amount)
    {
        float Temp = armor + amount;

        if (Temp >= maxArmor)
        {
            armor = maxArmor;
        }
        else
        {
            armor = Temp;
        }

        UpdateArmor();
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetArmor()
    {
        return armor;
    }

    public void UpdateHealth()
    {
        healthText.SetText(health.ToString());
    }

    public void UpdateArmor()
    {
        armorText.SetText(armor.ToString());
    }

    public void TakeDamage(float damageAmount)
    {
        if (damageAmount > 0.0f)
        {
            float HealthAbsorbingFraction = 1.0f / 3.0f;
            float ArmorAbsorbingFraction = 1.0f - HealthAbsorbingFraction;
            float HealthDamage = HealthAbsorbingFraction * damageAmount;
            float ArmorDamage = ArmorAbsorbingFraction * damageAmount;

            // calculate armor
            float RemainingAmor = armor - ArmorDamage;
            if (RemainingAmor < 0.0f)
            {
                HealthDamage -= RemainingAmor;
                armor = 0.0f;
            }
            else
            {
                armor = RemainingAmor;
            }

            // calculate health
            float RemainingHealth = health - HealthDamage;
            if (RemainingHealth < 0.0f)
            {
                health = 0.0f;
            }
            else
            {
                health = RemainingHealth;
            }
        }

        UpdateHealth();
        UpdateArmor();

        if (health <= 0.0f)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        Destroy(this);
    }

    public void ShowHUDUI()
    {
        HUD.SetActive(true);
    }

    public void HideHUD()
    {
        HUD.SetActive(false);
    }

    public void ShowKeyUI()
    {

    }

    public void UpdateAmmoCounter()
    {
        switch(currentWeaponAmmo)
        {
            case ammotype.bullets:
                ammoText.text = bullets.ToString();
                break;
            case ammotype.shells:
                ammoText.text = shells.ToString();
                break;
            case ammotype.rockets:
                ammoText.text = rockets.ToString();
                break;
            case ammotype.cells:
                ammoText.text = cells.ToString();
                break;
            case ammotype.slugs:
                ammoText.text = slugs.ToString();
                break;
        }
    }
}
