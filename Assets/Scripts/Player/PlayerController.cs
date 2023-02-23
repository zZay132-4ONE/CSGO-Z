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

    // The player's velocity
    private Vector3 velocity = Vector3.zero;

    // The player's physical rotation (left/right)
    private Vector3 yRotation = Vector3.zero;

    // The player's perspective rotation (up/down)
    private Vector3 xRotation = Vector3.zero;

    /// <summary>
    ///     <para>An interface for changing the player's velocity according to the input.</para>
    /// </summary>
    /// <param name="_velocity">The player's current velocity.</param>
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    /// <summary>
    ///     <para>An interface for changing the the player's and its perspective's rotations.</para>
    /// </summary>
    /// <param name="_yRotation">The player's physical rotation.</param>
    /// <param name="_xRotation">The player's perspective rotation.</param>
    public void Rotate(Vector3 _yRotation, Vector3 _xRotation)
    {
        yRotation = _yRotation;
        xRotation = _xRotation;
    }

    /// <summary>
    ///     <para>Move the player.</para>
    /// </summary>
    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
        }
    }

    /// <summary>
    ///     <para>Rotation the player and its camera's perspective.</para>
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
            cam.transform.Rotate(xRotation);
        }
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }
}