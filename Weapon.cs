using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public int damage;
    public Camera camera;
    public float range;
    public float fireRate;
    
    [Header("vfx")]
    public ParticleSystem hitFx;
    
    private float nextFire;

    [Header("ammo")]
    public int mag=5;
    public int ammo=45;
    public int magAmmo=45;
    public int maxAmmo=90;

    [Header("UI")]
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI magText;


    [Header("Animation")]
    public Animation anim;
    public AnimationClip reload;

    void Start()
    {
        magText.text = "Mag: " + mag.ToString();
        ammoText.text = "Ammo: " +  ammo.ToString() + '/' + magAmmo.ToString();
    }

    void Update()
    {

        if(nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }

        if(Input.GetButton("Fire1") && nextFire <= 0 && anim.isPlaying == false)
        {
            if(ammo > 0)
            {

                nextFire = 1 / fireRate;
                Shoot();
                
                ammo--;

                magText.text = "Mag: " + mag.ToString();
                ammoText.text = "Ammo: " +  ammo.ToString() + '/' + magAmmo.ToString();

            }

            else
            {
                Reload();
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
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

    public void Reload()
    {
        if(mag > 0 && ammo < magAmmo)
        {
            anim.Play(reload.name);
            mag--;
            ammo = magAmmo;
        }

        magText.text = "Mag: " + mag.ToString();
        ammoText.text = "Ammo: " +  ammo.ToString() + '/' + magAmmo.ToString();
    }
}
