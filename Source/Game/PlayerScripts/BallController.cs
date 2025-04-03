using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// BallController Script.
/// </summary>
public class BallController : Script
{
    public RigidBody ball;
    private Vector3 _velocity;

    public float speed = 2f;
    public float jumpForce = 2f;

    public override void OnUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var yVelocity = 0f;

        if (Input.GetAction("Jump"))
        {
            yVelocity = 25 * jumpForce;
        }

        _velocity = new Vector3(horizontal, 0, vertical);
        _velocity.Normalize();
        _velocity *= speed;
        _velocity.Y = yVelocity;
    }

    public override void OnFixedUpdate()
    {
        ball.AddForce(_velocity * 50000 * Time.DeltaTime, ForceMode.Acceleration);
    }
}
