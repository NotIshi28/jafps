using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{

    public static RoomManager instance;
    public GameObject player;
    public Transform spawnPoint;

    public GameObject roomCam;

    public GameObject nameUI;
    public GameObject connectingUI;
    
    private string playerName="WigglySquid123";

    
    public void ChangeName(string _name)
    {
        playerName = _name;
    }

    public void JoinButtonPressed()
    {
        Debug.Log("Connecting to server...");
        PhotonNetwork.ConnectUsingSettings();

        nameUI.SetActive(false);
        connectingUI.SetActive(true);
    }

    void Awake()
    {
        instance = this;   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnected();

        Debug.Log("Connected to server");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("Room", null, null);

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("Joined room");

        roomCam.SetActive(false);

        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().isLocalPlayer();
        _player.GetComponent<Health>().isLocalPlayer = true;

    }

    public void RespawnPlayer()
    {
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().isLocalPlayer();
        _player.GetComponent<Health>().isLocalPlayer = true;

        _player.GetComponent<PhotonView>().RPC("SetName", RpcTarget.All, playerName);
    }
}
