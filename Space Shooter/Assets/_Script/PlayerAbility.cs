using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Ability_Heal
{
    public float cost, CD, Heals;
    [HideInInspector] public float NextCD;
    [HideInInspector] public bool finishCD = false;
}

[System.Serializable]
public class Ability_EnhanceAmmo
{
    public float cost, CD, dmgBuffs, duration;
    [HideInInspector] public float NextCD;
    [HideInInspector] public bool finishCD = false;
}
public class PlayerAbility : MonoBehaviour
{
    public Ability_Heal ab1;
    public Ability_EnhanceAmmo ab2;

    PlayerHealth HP;
    PlayerEnergy MP;
    PlayerController stats;

    public GameObject shot;
    public GameObject advancedShot;
    public Transform shotSpawn;

    float fireRate;
    public float fireRate_normal;
    public float fireRate_special;
    private float nextFire;

    public Text ability1Text;
    public Text ability2Text;

    float ab1_countDown;
    float ab2_countDown;

    public AudioClip boltSoundSpecial;
    public AudioClip boltSoundNormal;
    public AudioSource MusicClip;
    public float volume_special;
    public float volume_normal;
    float timer = 0f;
    bool abilityOn = false;

    void Awake()
    {
        MusicClip = GetComponent<AudioSource>();
    }


    void Start()
    {
        GameObject playerHealthObject = GameObject.FindWithTag("Player");
        GameObject playerEnergyObject = GameObject.FindWithTag("Player");
        GameObject playerControllerObject = GameObject.FindWithTag("Player");

        stats = playerControllerObject.GetComponent<PlayerController>();
        HP = playerHealthObject.GetComponent<PlayerHealth>();
        MP = playerEnergyObject.GetComponent<PlayerEnergy>();

        ab1_countDown = ab1.CD;
        ab2_countDown = ab2.CD;
    }
    
    void Update()
    {
        // key binding of fire
        if ((Input.GetButton("Fire1") && Time.time > nextFire))
        {
            if (abilityOn)
            {
                fireRate = fireRate_special;
                nextFire = Time.time + fireRate; // frequency of fire
                Instantiate(advancedShot, shotSpawn.position, shotSpawn.rotation);
                MusicClip.PlayOneShot(boltSoundSpecial, volume_special);

                shotSpawn.transform.eulerAngles = new Vector3(0, Random.Range(-8f, 8f), 0);
            } else {
                fireRate = fireRate_normal;
                stats.ATK = 20;
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                MusicClip.PlayOneShot(boltSoundNormal, volume_normal);

                shotSpawn.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        // Ability 1: Heal
        if ((Input.GetKeyDown(KeyCode.Q)) && Time.time > ab1.NextCD && MP.playerCurrentEnergy >= ab1.cost && HP.playerCurrentHP < HP.playerInitialHP)
        {
            ab1.NextCD = Time.time + ab1.CD; // CoolDown
            HP.playerCurrentHP += ab1.Heals; // Heals
            // Player's HP won't exceed 100 in any case
            if (HP.playerCurrentHP > HP.playerInitialHP) // (> 100HP)
            {
                HP.playerCurrentHP = HP.playerInitialHP;
            }
            // update UI
            MP.UseEnergy(ab1.cost);
            HP.UpdateHPText();

            ab1.finishCD = true;
        }

        // Ability 2: Enhanced Ammo
        if ((Input.GetKeyDown(KeyCode.E)) && Time.time > ab2.NextCD && MP.playerCurrentEnergy >= ab2.cost)
        {
            abilityOn = true;
            ab2.NextCD = Time.time + ab2.CD; // CoolDown
            stats.ATK += ab2.dmgBuffs;
            Debug.Log("Ability 2 activated. Damage increased to " + stats.ATK);
            MP.UseEnergy(ab2.cost);
        }
        if (abilityOn)
        {
            timer += Time.deltaTime;
            if (timer > ab2.duration) // ability ends
            {
                abilityOn = false;
                timer = 0f;
                Debug.Log("Ability 2 ends. Player damage: " + stats.ATK);
                ab2.finishCD = true;
            }
        }

        if (ab1.finishCD)
        {
            ab1_countDown -= Time.deltaTime;
            ability1Text.gameObject.GetComponent<Text>().text = "Cooldown: " + (int)ab1_countDown;
            if (ab1_countDown <= 0f)
            {
                ab1_countDown = ab1.CD;
                ab1.finishCD = false;
                ability1Text.text = "Healing: Q";
            }
        }
        if (ab2.finishCD)
        {
            ab2_countDown -= Time.deltaTime;
            ability2Text.gameObject.GetComponent<Text>().text = "Cooldown: " + (int)ab2_countDown;
            if (ab2_countDown <= 0f)
            {
                ab2_countDown = ab2.CD;
                ab2.finishCD = false;
                ability2Text.text = "Enhanced Ammo: E";
            }
        }
    }
}
