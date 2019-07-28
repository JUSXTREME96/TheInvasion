using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int health = 100;

	public GameObject deathEffect;
    AudioSource audio;
    private void Start()
    {
        audio = GetComponentInChildren<AudioSource>();
    }
    public void TakeDamage (int damage)
	{
		health -= damage;

		if (health <= 0)
		{
			Die();
		}
	}

	void Die ()
	{
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        //audio.Play();
		Destroy(gameObject);
	}

}
