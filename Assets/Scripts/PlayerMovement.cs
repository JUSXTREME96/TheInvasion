using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float currentSpeed, minimumSpeed, maximumSpeed, acceleration;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            if(currentSpeed < maximumSpeed)
            {
                currentSpeed += Time.deltaTime * acceleration;
            }
            else
            {
                currentSpeed = maximumSpeed;
            }
        }
        else
        {
            currentSpeed = minimumSpeed;
        }

        transform.position += movement * Time.deltaTime * currentSpeed;
    }
}
