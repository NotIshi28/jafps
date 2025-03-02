using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using TMPro;
using Photon.Pun.UtilityScripts;

public class Leaderboard : MonoBehaviour
{
    public GameObject playerHolder;

    [Header("Options")]

    public float refreshRate = 1f;

    [Header("UI")]

    public GameObject[] slots;
    public TextMeshProUGUI[] nameText;
    public TextMeshProUGUI[] scoreText;

    void Start()
    {
        InvokeRepeating(nameof(Refresh), 1f, refreshRate);
    }

    public void Refresh()
    {
        foreach(var slot in slots)
        {
            slot.SetActive(false);
        }

        var sortedPlayerList = (from player in PhotonNetwork.PlayerList orderby player.GetScore() descending select player).ToList();

        int i = 0;
        
        foreach (var player in sortedPlayerList)
        {
            slots[i].SetActive(true);
            Debug.Log(player);
            nameText[i].text = player.NickName;
            scoreText[i].text = player.GetScore().ToString();
            i++;
        }
    }

    void Update()
    {
        playerHolder.SetActive(Input.GetKey(KeyCode.Tab));
    }
}
