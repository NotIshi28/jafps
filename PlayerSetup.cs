using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviour
{
    public Movement movement;

    public GameObject camera;

    public TextMeshPro nameText;

    public string playerName;

    public Transform TPWeaponHolder;

    public void isLocalPlayer()
    {
        TPWeaponHolder.gameObject.SetActive(false);
        movement.enabled = true;
        camera.SetActive(true);
    }

    [PunRPC]
    public void setTPWeapon(int _weaponIndex)
    {
        foreach(Transform weapon in TPWeaponHolder)
        {
            weapon.gameObject.SetActive(false);
        }

        TPWeaponHolder.GetChild(_weaponIndex).gameObject.SetActive(true);
    }

    [PunRPC]
    public void SetName(string _name)
    {
        playerName = _name;
        nameText.text=_name;
    }

}
