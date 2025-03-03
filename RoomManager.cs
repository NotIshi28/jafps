using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomManager : MonoBehaviourPunCallbacks
{

    public static RoomManager instance;
    public GameObject player;
    public Transform[] spawnPoints;

    public GameObject roomCam;

    public GameObject nameUI;
    public GameObject connectingUI;

    [HideInInspector]
    public int kills = 0;
    public int deaths = 0;
    
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

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().isLocalPlayer();
        _player.GetComponent<Health>().isLocalPlayer = true;
        _player.GetComponent<PhotonView>().RPC("SetName", RpcTarget.AllBuffered, playerName);

        PhotonNetwork.LocalPlayer.NickName = playerName;
    }

    public void RespawnPlayer()
    {

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().isLocalPlayer();
        _player.GetComponent<Health>().isLocalPlayer = true;
        _player.GetComponent<PhotonView>().RPC("SetName", RpcTarget.AllBuffered, playerName);

        PhotonNetwork.LocalPlayer.NickName = playerName;
    }

    
    public void SetHashes()
    {
        try
        {
            Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;

            hash["kills"] = kills;
            hash["deaths"] = deaths;

            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }

        catch
        {

        }
    }

}
