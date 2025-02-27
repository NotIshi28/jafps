using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    public Camera camera;
    public float range;
    public float fireRate;
    
    [Header("vfx")]
    public ParticleSystem hitFx;
    
    private float nextFire;

    void Update()
    {

        if(nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }

        if(Input.GetButton("Fire1") && nextFire <= 0 )
        {
            nextFire = 1 / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        RaycastHit hit;

        if(Physics.Raycast(ray.origin, ray.direction, out hit, range))
        {
            
            PhotonNetwork.Instantiate(hitFx.name, hit.point, Quaternion.identity);

            if(hit.transform.gameObject.GetComponent<Health>())
            {
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
                Debug.Log("Hit: " + hit.transform.name);
            }
            
        }
    }


}
