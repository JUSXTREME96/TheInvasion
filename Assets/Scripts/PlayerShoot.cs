using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject standardProjectile;
    public Transform firingPoint, forwardFire, upFire, downFire;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void Shoot(float direction)
    {
        if(direction == 0)
        {
            firingPoint = forwardFire;
        }
        else if(direction == 1)
        {
            firingPoint = upFire;
        }
        else if(direction == -1)
        {
            firingPoint = downFire;
        }

        GameObject fireShot = Instantiate(standardProjectile, firingPoint.position, firingPoint.rotation);
    }
}
