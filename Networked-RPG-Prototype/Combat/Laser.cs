using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	public Rigidbody laser;
	public float deleteBullet = 3.5f;
    private ExpManager health;
    public float speed = 500;

	void Start()
	{
		//pp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerProperties>();
		laser = gameObject.GetComponent<Rigidbody>();
        health = GameObject.FindGameObjectWithTag("Inventory").GetComponent<ExpManager>();
	}
	void FixedUpdate()
	{
		laser.AddForce(transform.forward * speed);
	}

	// Update is called once per frame
	void Update () {
		//transform.position += (velocity * transform.position) * Time.deltaTime;


		deleteBullet -= Time.deltaTime;

		if(deleteBullet < 0)
		{
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter (Collision col) {
		//if (col.gameObject.tag == "Player" || col.gameObject.tag == "Bot")
			//col.health -= 10;

		if(col.gameObject.tag == "Enemy")
		{
			//Destroy(col.gameObject);
		}
        else if(col.gameObject.tag == "Player")
        {
            health.health -= 50;
        }
			// Self destruct
	}
}
