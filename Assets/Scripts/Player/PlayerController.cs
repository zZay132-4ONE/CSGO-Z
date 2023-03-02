using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     <para>Handles the control of the player, encompassing the player's attributes and actions.</para>
/// </summary>
public class PlayerController : MonoBehaviour
{
    // The RigidBody instance of the player
    [SerializeField] private Rigidbody rigidBody;

    // The camera representing the player's perspective
    [SerializeField] private Camera cam;

    // The limitation of the camera's total rotation value (vertical)
    [SerializeField] private float cameraRotationLimit = 85.0f;
    
    // The player's velocity
    private Vector3 velocity = Vector3.zero;

    // The player's physical rotation (left/right)
    private Vector3 yRotation = Vector3.zero;

    // The player's perspective rotation (up/down)
    private Vector3 xRotation = Vector3.zero;
    
    // The total rotation value of the camera (vertical)
    private float cameraRotationTotal = 0.0f;
    
    // The player's thrust force (y-axis)
    private Vector3 thrustForce = Vector3.zero;

    /// <summary>
    ///     <para>An interface for modifying the player's velocity according to the input.</para>
    /// </summary>
    /// <param name="_velocity">The player's current velocity.</param>
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    /// <summary>
    ///     <para>An interface for modifying the player and its perspective's rotations.</para>
    /// </summary>
    /// <param name="_yRotation">The player's physical rotation.</param>
    /// <param name="_xRotation">The player's perspective rotation.</param>
    public void Rotate(Vector3 _yRotation, Vector3 _xRotation)
    {
        yRotation = _yRotation;
        xRotation = _xRotation;
    }

    /// <summary>
    ///     <para>An interface for modifying the player's thrust force (y-axis force).</para>
    /// </summary>
    /// <param name="_thrustForce"></param>
    public void Thrust(Vector3 _thrustForce)
    {
        thrustForce = _thrustForce;
    }
    
    /// <summary>
    ///     <para>Move the player.</para>
    /// </summary>
    private void PerformMovement()
    {
        // Make the player move
        if (velocity != Vector3.zero)
        {
            rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
        }
        // Make the player fly
        if (thrustForce != Vector3.zero)
        {
            rigidBody.AddForce(thrustForce);
        }
    }

    /// <summary>
    ///     <para>Rotate the player and its camera's perspective.</para>
    /// </summary>
    private void PerformRotation()
    {
        // Rotate the player (left/right)
        if (yRotation != Vector3.zero)
        {
            rigidBody.transform.Rotate(yRotation);
        }

        // Rotate the camera's perspective (up/down)
        if (xRotation != Vector3.zero)
        {
            cameraRotationTotal += xRotation.x;
            cameraRotationTotal = Mathf.Clamp(cameraRotationTotal, -cameraRotationLimit, cameraRotationLimit);
            cam.transform.localEulerAngles = new Vector3(cameraRotationTotal, 0.0f, 0.0f);
        }
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }
}