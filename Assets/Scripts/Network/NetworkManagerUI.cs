using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     <para>Handles the control of network-manager-related UI.</para>
/// </summary>
public class NetworkManagerUI : MonoBehaviour
{
    // A button for starting a host
    [SerializeField] private Button hostBtn;

    // A button for starting a server
    [SerializeField] private Button serverBtn;

    // A button for starting a client
    [SerializeField] private Button clientBtn;

    // Start is called before the first frame update
    void Start()
    {
        // Add listeners to buttons
        hostBtn.onClick.AddListener(() => { NetworkManager.Singleton.StartHost(); });
        serverBtn.onClick.AddListener(() => { NetworkManager.Singleton.StartServer(); });
        clientBtn.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
    }

    // Update is called once per frame
    void Update()
    {
    }
}