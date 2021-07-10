using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class menu : MonoBehaviourPunCallbacks
{

    [Header("screens")]
    public GameObject mainScreen;
    public GameObject lobbyScreen;

    [Header("Main Screen")]
    public Button createRoomButton;
    public Button joinRoomButton;

    [Header("Lobby Screen")]
    public TextMeshProUGUI playerListText;
    public Button startGameButton;
    public TextMeshProUGUI roomNameText;

    private void Start()
    {
        createRoomButton.interactable = false;
        joinRoomButton.interactable = false;
    }

    public override void OnConnectedToMaster()
    {
        createRoomButton.interactable = true;
        joinRoomButton.interactable = true;
    }

    void SetScreen(GameObject screen)
    {
        //desactivate all screen
        mainScreen.SetActive(false);
        lobbyScreen.SetActive(false);

        //enabled requested scene
        screen.SetActive(true);
    }

    public void OnCreateRoomButton(TMP_InputField roomNameInput)
    {
        networkManager.instance.CreateRoom(roomNameInput.text);
        roomNameText.text = roomNameInput.text;
    }

    public void OnJoinRoomButton(TMP_InputField roomNameInput)
    {
        networkManager.instance.JoinRoom(roomNameInput.text);
        roomNameText.text = roomNameInput.text;
    }

    public void OnPlayerNameUpdate(TMP_InputField PlayerNameInput)
    {
        PhotonNetwork.NickName = PlayerNameInput.text;
    }

    public override void OnJoinedRoom()
    {
        SetScreen(lobbyScreen);

        photonView.RPC("UpdateLobbyUI", RpcTarget.All);

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateLobbyUI();
    }

    [PunRPC]
    public void UpdateLobbyUI()
    {
        playerListText.text = "";
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            if (player.IsMasterClient)
            {
                playerListText.text += player.NickName + " (Host) \n";
            }
            else
            {
                playerListText.text += player.NickName + "\n";
            }
        }

        if (PhotonNetwork.IsMasterClient)
            startGameButton.interactable = true;
        else
            startGameButton.interactable = false;

    }

    public void OnLeaveLobbyButton()
    {
        PhotonNetwork.LeaveRoom();
        SetScreen(mainScreen);
    }

    public void OnStartGameButton()
    {
        networkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, "Main");

    }

    public void OnChangeScene()
    {
        SceneManager.LoadScene(1);
    }



}
