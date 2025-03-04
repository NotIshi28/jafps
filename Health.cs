using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Health : MonoBehaviour
{
    public float health;

    public bool isLocalPlayer;
    public TextMeshProUGUI healthText;

    public RectTransform healthbar;
    private float originalHealthbarSize;


    void Start()
    {
        originalHealthbarSize = healthbar.sizeDelta.x;
        
    }

    [PunRPC]
    public void TakeDamage(float _damage)
    {
        health -= _damage;
        
        healthText.text = "Health: " + Mathf.Round(health).ToString();

        healthbar.sizeDelta = new Vector2(originalHealthbarSize * health/100f, healthbar.sizeDelta.y);

        if (health <= 0)
        {
            if (isLocalPlayer)
            {
                RoomManager.instance.RespawnPlayer();
                RoomManager.instance.deaths++;
                RoomManager.instance.SetHashes();
            }
            
            
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (transform.position.y < -2)
        {
            if (isLocalPlayer)
                RoomManager.instance.RespawnPlayer();

            Destroy(gameObject);
        }
    }

}
