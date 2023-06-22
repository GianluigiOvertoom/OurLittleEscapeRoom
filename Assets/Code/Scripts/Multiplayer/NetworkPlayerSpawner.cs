using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;
    private Vector3 spawnPosition;
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnPosition = new Vector3((PhotonNetwork.CountOfPlayers <= 1) ? -15f : 15f, 10f, -14f);
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("NetworkPlayer", spawnPosition, transform.rotation);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
