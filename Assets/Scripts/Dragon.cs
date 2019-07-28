using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    public Transform frontFire;

    public float attackTime, attackRange;
    private float nextTime;

    public GameObject projectile;
    [SerializeField] int randomFire = 3;
    private PlayerControl target;

    public LayerMask enemyMask;
    public float speed = 1;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth, myHeight;
    private bool isFacingRight;
    SpriteRenderer mySprite;

    [SerializeField] float maxDist = 5f;
    [SerializeField] float minDist = 10f;
    [SerializeField] GameObject firepoint;
    [SerializeField] float shootFrequency = 5;
    void Start()
    {
        target = FindObjectOfType<PlayerControl>();

        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        mySprite = this.GetComponentInChildren<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(target.transform.position, transform.position) < minDist)
        {
            FacePlayer();

            randomFire = UnityEngine.Random.Range(0, 100);

            if (randomFire < shootFrequency)
                Shoot();
        }
        else
            myBody.velocity = Vector2.zero;

        //RaycastHit2D hitInfo = Physics2D.Raycast(frontFire.position, -frontFire.right, attackRange);

        //if (hitInfo)
        //{
        //    if (hitInfo.transform.name == "Player")
        //        if (nextTime > attackTime)
        //        {
        //            Shoot();
        //        }
        //    nextTime += Time.deltaTime;
        //}
        //else
        //{

        //    Patrol();
        //}
    }

    public void Shoot()
    {
        nextTime = 0;
        Debug.Log("Bang");
        Instantiate(projectile, firepoint.transform.position, firepoint.transform.rotation);
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

        if (transform.position.x > target.transform.position.x)
        {
            if(transform.position.y > target.transform.position.y)
                myBody.velocity = new Vector2(-1 * speed * Time.deltaTime, -Time.deltaTime * (speed / 2));
            else
                myBody.velocity = new Vector2(-1 * speed * Time.deltaTime, Time.deltaTime * (speed / 2));

            if (isFacingRight) // ... flip the player.
                Flip();
        }
        else
        {
            if (transform.position.y > target.transform.position.y)
                myBody.velocity = new Vector2(1 * speed * Time.deltaTime, -Time.deltaTime * (speed / 2));
            else
                myBody.velocity = new Vector2(1 * speed * Time.deltaTime, Time.deltaTime * (speed / 2));

            if (!isFacingRight) // ... flip the player.
                Flip();
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        isFacingRight = !isFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}