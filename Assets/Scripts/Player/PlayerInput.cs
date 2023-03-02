using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
///     <para>Handles the input for controlling the player.</para>
/// </summary>
public class PlayerInput : MonoBehaviour
{
    // The speed of the player's movement
    [SerializeField] private float moveSpeed = 4.0f;

    // The speed of the camera's movement
    [SerializeField] private float lookSensitivity = 12.0f;

    // The thrust force in the y-axis direction of the player
    [SerializeField] private float thrustForce = 20.0f;
    
    // The instance of the player controller
    [SerializeField] private PlayerController playerController;

    // The "configurable joint" component of the player (for simulating flying)
    private ConfigurableJoint configurableJoint;
    
    // Start is called before the first frame update
    void Start()
    {
        // Lock the cursor
        // Cursor.lockState = CursorLockMode.Locked;
        
        // Assign the "configurable joint" component
        configurableJoint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the player's movement input, calculate the velocity, and move
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");
        Vector3 velocity = (transform.right * xMove + transform.forward * yMove).normalized * moveSpeed;
        playerController.Move(velocity);

        // Get the cursor's movement input, calculate the rotation, and rotate
        float xMouse = Input.GetAxisRaw("Mouse X");
        float yMouse = Input.GetAxisRaw("Mouse Y");
        Vector3 yRotation = new Vector3(0.0f, xMouse, 0.0f) * lookSensitivity;
        Vector3 xRotation = new Vector3(-yMouse, 0.0f, 0.0f) * lookSensitivity;
        playerController.Rotate(yRotation, xRotation);
        
        // Get the player's space input, calculate the velocity, and fly
        Vector3 force = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            force = Vector3.up * thrustForce;
            configurableJoint.yDrive = new JointDrive
            {
                positionSpring = 0.0f,
                positionDamper = 0.0f,
                maximumForce = 0.0f
            };
        }
        else
        {
            configurableJoint.yDrive = new JointDrive
            {
                positionSpring = 20.0f,
                positionDamper = 0.0f,
                maximumForce = 40.0f
            };
        }
        playerController.Thrust(force);
    }
}