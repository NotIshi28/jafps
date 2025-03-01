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

    public void isLocalPlayer()
    {
        movement.enabled = true;
        camera.SetActive(true);
    }

    [PunRPC]
    public void SetName(string _name)
    {
        playerName = _name;
        nameText.text=_name;
    }

}
