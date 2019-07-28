using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabWeapon : MonoBehaviour {

	public Transform firePoint;
	public GameObject bulletPrefab;
    public GameObject specialPrefab;
    public int maxShot, currentShot;
    public Text ammo;

    AudioSource audio;
    // Update is called once per frame
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    void Update() {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        ammo.text = currentShot.ToString(); ;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "SpecialPickUp")
        {
            currentShot = maxShot;
            Destroy(other.gameObject);
        }

    }

    void Shoot ()
	{
        audio.Play();
        if(currentShot > 0)
        {
            Instantiate(specialPrefab, firePoint.position, firePoint.rotation);
            currentShot = currentShot - 1;
        }
        else
        {
            //Creates the ammo that gets a velocity to shoot
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
	}
}
