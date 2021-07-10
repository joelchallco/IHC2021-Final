using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunscript : MonoBehaviour
{    
    public int gunDamage;
    public Camera cam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    float nestTimeToShot;
    public float firerate;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //nestTimeToShot = Time.time + 1f / firerate;
            Shoot();
        }
    }
    void Shoot()
    {
        //muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            Debug.Log("we hit" + hit.transform.name);
            Health health = hit.transform.GetComponent<Health>();
            if (health != null)
            {
                health.Damage(gunDamage);
            }

        }
    }
}