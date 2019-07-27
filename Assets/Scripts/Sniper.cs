using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Enemy
{
    public Transform frontFire, upFire, downFire;
    public float forwardAngle;

    public float attackTime, attackRange;
    private float nextTime;

    public GameObject projectile;

    private Transform target;
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (health > 0 && Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            if (nextTime > attackTime)
            {
                Shoot();
                nextTime = 0;
            }
        }
        nextTime += Time.deltaTime;
    }

    public void Shoot()
    { 
        //GameObject fireBullet = Instantiate(projectile, transform.position + (transform.up * 0.85f), transform.rotation);
    }
}
