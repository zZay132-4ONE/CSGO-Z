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
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
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

    private void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}