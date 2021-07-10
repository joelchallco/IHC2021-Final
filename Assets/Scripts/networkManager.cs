using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class networkManager : MonoBehaviourPunCallbacks
{
    public static networkManager instance;

    public void Awake()
    {
        if( instance != null && instance != this)
        {
            gameObject.SetActive(false);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

    }

    public void CreateRoom( string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("create room: " + PhotonNetwork.CurrentRoom.Name);
    }

    public void JoinRoom(string roomName)
    {
        if(PhotonNetwork.PlayerList.Length <= 2)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    [PunRPC]
    public void ChangeScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }


}
