using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class NetworkUI : NetworkBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private TextMeshProUGUI playerCountText;

    private NetworkVariable<int> playerCount = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);
    [SerializeField] private InputAction starHost;
    [SerializeField] private InputAction startClient;

    private void Awake()
    {
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });

        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
        starHost.Enable();
        startClient.Enable();
        startClient.started -= startHost => NetworkManager.Singleton.StartHost();
        startClient.started += startClient => NetworkManager.Singleton.StartClient();
    }

    private void OnDisable()
    {
        starHost.started -= startHost => NetworkManager.Singleton.StartHost();
        startClient.started -= startClient => NetworkManager.Singleton.StartClient();
        //starHost.Disable();
        //startClient.Disable();
    }

    private void Update()
    {
        playerCountText.text = playerCount.Value.ToString();
        if (!IsServer)
        {
            return;
        }
        playerCount.Value = NetworkManager.Singleton.ConnectedClients.Count;
    }
}
