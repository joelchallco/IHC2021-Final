using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [HideInInspector]
    public int id;

    [Header("Info")]
    public Animator animator;

    [Header("Component")]
    public Rigidbody rig;
    public Player photonPlayer;
    public Text playerNickName;

    [PunRPC]

    public void Initialize(Player player)
    {
        photonPlayer = player;
        id = player.ActorNumber;

        GameManager.instance.players[id - 1] = this;

        if (!photonView.IsMine)
        {
            rig.isKinematic = true;
        }

    }

    private void Start()
    {
        playerNickName.text = photonPlayer.NickName;
    }

    public void shoot()
    {
        GameManager.instance.photonView.RPC("DestroyTarget", RpcTarget.All,GameManager.instance.selectedTarget.transform.name);
    }

    public void initiateRespawnTarget()
    {
        GameManager.instance.photonView.RPC("RespawnAllTarget", RpcTarget.All);
    }


}
