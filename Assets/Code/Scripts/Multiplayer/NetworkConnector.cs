using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;

public class NetworkConnector : MonoBehaviour
{
    private NetworkManager _networkManager;
    private void Awake()
    {
        _networkManager = GetComponent<NetworkManager>();
    }
    private void Start()
    {   
        CheckforHost();
    }

    public async void CheckforHost()
    {
        _networkManager.StartClient();
        await Task.Delay(1700);
        if (!_networkManager.IsConnectedClient)
        {
            _networkManager.Shutdown();
            CreateNewHost();
        }
    }

    public async void CreateNewHost()
    {
        await Task.Delay(1500);
        _networkManager.StartHost();
    }
}
