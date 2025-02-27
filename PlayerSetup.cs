using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public Movement movement;

    public GameObject camera;

    public void isLocalPlayer()
    {
        movement.enabled = true;
        camera.SetActive(true);
    }


}
