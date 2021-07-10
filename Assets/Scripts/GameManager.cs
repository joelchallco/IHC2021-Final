using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;


public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("Stats")]
    public bool gameEnded = false;

    [Header("Players")]
    public string playerPrefabLocation;
    public Transform[] spawnPoints;
    public PlayerController[] players;
    private int playersInGame;
    private List<int> pickedSpawnIndex;

    [Header("Targets")]
    public GameObject selectedTarget;

    //instance
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pickedSpawnIndex = new List<int>();
        players = new PlayerController[PhotonNetwork.PlayerList.Length];
        photonView.RPC("ImInGame", RpcTarget.AllBuffered);

    }

    [PunRPC]
    void ImInGame()
    {
        playersInGame++;

        if (playersInGame == PhotonNetwork.PlayerList.Length)
            SpawnPlayer();
    }

    void SpawnPlayer()
    {
        int rand = Random.Range(0, spawnPoints.Length);

        while (pickedSpawnIndex.Contains(rand))
        {
            rand = Random.Range(0, spawnPoints.Length);
        }

        pickedSpawnIndex.Add(rand);

        GameObject playerObject = PhotonNetwork.Instantiate(playerPrefabLocation, spawnPoints[rand].position, Quaternion.identity);
        PlayerController playerScript = playerObject.GetComponent<PlayerController>();
        playerScript.photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);

    }

    [PunRPC]
    void DestroyTarget(string targetName)
    {
        GameObject target = GameObject.Find(targetName);

        if (target)
        {
            target.SetActive(false);
        }
        else
        {
            print("target not found.\n");
        }
    }

    [PunRPC]
    void RespawnAllTarget()
    {
        if (!CheckTargetExist())
        {
            GameObject targets = GameObject.Find("_TargetObject");
            foreach(Transform target in targets.transform)
            {
                target.gameObject.SetActive(true);
            }
        }
    }

    bool CheckTargetExist()
    {
        GameObject targets = GameObject.Find("_TargetObject");
        foreach(Transform target in targets.transform)
        {
            if (target.gameObject.activeInHierarchy)
            {
                return true;
            }
        }

        return false;
    }

    public PlayerController GetPlayer(int playerID)
    {
        return players.First(x => x.id == playerID);
    }

    public PlayerController GetPlayer(GameObject playerObj)
    {
        return players.First(x => x.gameObject == playerObj);
    }


}

