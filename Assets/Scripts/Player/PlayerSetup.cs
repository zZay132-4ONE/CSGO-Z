using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

/// <summary>
///     <para>Handles the player's setup to disable other online players' components.</para>
/// </summary>
public class PlayerSetup : NetworkBehaviour
{
    // Other players' components that should be disabled
    [SerializeField] private Behaviour[] componentsToDisable;

    // Scene camera
    private Camera sceneCamera;

    // Start is called before the first frame update
    void Start()
    {
        if (!IsLocalPlayer)
        {
            // If the player is not local, then disable all its components
            foreach (Behaviour component in componentsToDisable)
            {
                component.enabled = false;
            }
        }
        else
        {
            // If the player is local, then disable the scene camera
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}