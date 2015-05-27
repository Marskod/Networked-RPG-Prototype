using UnityEngine;
using System.Collections;

public class EnemyDefault : MonoBehaviour {

    public GUISkin skin;

    private PlayerCombat pc;
    public GameObject enemy;
    public Transform ent;
    public float distance = 0;
    public float health = 50;
    public float h;
    public bool display = false; 

    void Start()
    {
        //pc = GameObject.FindGameObjectWithTag("Inventory").GetComponent<PlayerCombat>();
    }


    void Update()
    {
        enemy = GameObject.FindGameObjectWithTag("Player");


        if(enemy != null)
        {
            distance = (Vector3.Distance(enemy.transform.position, transform.position));

            if (distance < 5 && distance!= 0)
            {
                display = true;
            }

        }
        if (health <= 0)
            Destroy(gameObject);
    }

    void OnMouseDown()
    {
        print("yo");
        display = !display;
    }

    void OnGUI()
    {
        GUI.skin = skin;

        Rect hDisplay = new Rect(Screen.width/4, Screen.height/15, health*4, 20);
        
        if(display == true)
            GUI.Box(hDisplay, "",skin.GetStyle("ExpBar"));
    }

    void OnCollisionStay(Collision col)
    {
        GameObject[] enemies;

        enemies = GameObject.FindGameObjectsWithTag("Weapon");

        print("sup");

        if ((col.gameObject.tag == "Weapon") /*&& (pc.isAttacking == true) && pc.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")*/) 
        {
            health -= 1;
        }
        if ((col.gameObject.tag == "Bullet") /*&& pc.anim.GetCurrentAnimatorStateInfo(0).IsName("Pistol_Attack")*/)
        {
            health -= 3;
            display = true;
            Destroy(col.gameObject);
        }
    }
}
