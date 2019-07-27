using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Enemy
{
    public Transform frontFire;

    public float attackTime, attackRange;
    private float nextTime;

    public GameObject projectile;

    private Transform target;

    public LayerMask enemyMask;
    public float speed = 1;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth, myHeight;

    void Start()
    {
        target = GameObject.Find("Player").transform;

        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
    }

    void FixedUpdate()
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(frontFire.position, -frontFire.right, attackRange);

        if (hitInfo)
        {
            if (hitInfo.transform.name == "Player")
                if (nextTime > attackTime)
                {
                    Shoot();
                }
            nextTime += Time.deltaTime;
        }
        else
        {

            Patrol();
        }
    }

    public void Shoot()
    {
        nextTime = 0;
        Debug.Log("Bang");
        GameObject fireBullet = Instantiate(projectile, transform.position + (transform.up * 0.85f), transform.rotation);
    }

    public void Patrol()
    {
        Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;

        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * .05f);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * .05f, enemyMask);

        if (!isGrounded || isBlocked)
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;
        }

        Vector2 myVel = myBody.velocity;
        myVel.x = -myTrans.right.x * speed;
        myBody.velocity = myVel;
    }

    public void FacePlayer()
    {

    }

}