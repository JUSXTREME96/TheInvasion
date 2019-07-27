using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;

    void Start()
    {
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction != 0)
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rigidBody.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (direction == 1)
                {
                    rigidBody.velocity = Vector2.right * dashSpeed;
                }
                else if (direction == 2)
                {
                    rigidBody.velocity = Vector2.left * dashSpeed;
                }
            }
        }
    }

    public void Dash(float directionPassed)
    {
        if (directionPassed > 0)
        {
            direction = 1;
        }
        else
        {
            direction = 2;
        }
    }
}
