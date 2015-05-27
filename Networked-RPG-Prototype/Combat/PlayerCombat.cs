using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{

    public GameObject weap;
    public GameObject combatcam;
    public GameObject mainCam;
    public GameObject bullet; 

    private Transform target; 
 
    public Transform spawn;
    public Transform PistolSpawnBullet;

    public AudioSource gun;

    private float counter;

    public float attacktime = .3f;

    public bool display = false;
    public bool weaponEquip = false;
    public bool combat = false;
    public bool isAttacking = false;

    public Item weapon;

    public CombatCamera cam;
    public CharacterMenu cm;
    public Animator anim;

    private GameObject[] playertags;

    void Start()
    {
        //delete anything with the player tag before server initialization 
        playertags = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playertags.Length; i++)
        {
            //Destroy(playertags[i]);
        }
        combatcam = GameObject.FindGameObjectWithTag("CombatCamera");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");

        anim = GetComponent<Animator>();
        cm = GetComponent<CharacterMenu>();
        cam = combatcam.GetComponent<CombatCamera>();
        spawn = transform;

        cam.player = transform;
    }

    void Update()
    {
        if (Input.GetKeyDown("z") )
        {
            combat = !combat;
        }

        if (combat)
        {
            combat_mode();
        }
        else
        {
            if(combatcam!= null)
                combatcam.SetActive(false);

            mainCam.SetActive(true);

            //Set all animation states back to false when the player camera is not in combat mode 
            if(anim != null)
            {
                anim.SetBool("IsAttacking", false);
                anim.SetBool("RangeReady", false);
                anim.SetBool("RangeAttack", false);
            }
        }
    }

    void combat_mode()
    {
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        combatcam.SetActive(true);
        mainCam.SetActive(false);

        //Default states of all the attack related animations 
        anim.SetBool("IsAttacking", false);
        anim.SetBool("RangeReady", false);
        anim.SetBool("RangeAttack", false);

        //Start countdown timer to make sure player does not spam attacks 
        if (isAttacking)
        {
            counter -= Time.deltaTime;

            if (counter < 0)
            {
                isAttacking = false;
                counter = attacktime;
            }
        }

        //Melee Attack
        if (Input.GetButtonDown("Attack") && isAttacking == false && cm.weapon.melee == 1)
        {
            isAttacking = true;
            anim.SetBool("IsAttacking", true);
        }

        //Pistol 
        if (isAttacking == false && cm.weapon.melee == 2)
        {
            //If the player is not attacking, simply have the player aim their weapon
            anim.SetBool("RangeReady", true);
            //If the player chooses to attack, do the following
            if (Input.GetButtonDown("Attack"))
            {
                anim.SetBool("RangeAttack", true);
                Instantiate(bullet, PistolSpawnBullet.transform.position, PistolSpawnBullet.transform.rotation);
                gun.GetComponent<AudioSource>().Play();
                isAttacking = true;
            }
            isAttacking = false;
        }


    }
    
    //spawn weapon at the provided weapon location and rotation 
    public void SpawnWeapon()
    {
        weaponEquip = true;
        weap = Instantiate(weapon.obj, spawn.position, spawn.rotation) as GameObject;
        weap.transform.parent = spawn.transform;
    }
}
