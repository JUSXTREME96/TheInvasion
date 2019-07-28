using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 20f;
	public int damage = 40;
	public Rigidbody2D rb;
	public GameObject impactEffect;
    public bool startLorR = true;
	// Use this for initialization
	void Start () {
        if(startLorR)
		    rb.velocity = transform.right * speed;
        else
            rb.velocity = -transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (tag == "Player Projectile" && hitInfo.tag == "Enemy")
         {
            Enemy enemy = hitInfo.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            //Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }

        if (tag == "Enemy Projectile" && hitInfo.tag == "Player")
        {
            PlayerControl PC = hitInfo.GetComponent<PlayerControl>();

            Debug.Log("Player Hit!");

            if (PC != null)
            {
                //PC.TakeDamage(damage);
            }

            //Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
	
}
