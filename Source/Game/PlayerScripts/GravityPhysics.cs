using FlaxEngine;

namespace Game;

public class GravityPhysics : Script
{
    public CharacterController characterController;

    public float Mass = 2f;

    private float yVelocity;
    private bool isGroundedLastFrame;

    public bool IsGrounded => characterController.IsGrounded;

    public void ApplyVerticalForce(float force)
    {
        yVelocity = force;
    }

    public override void OnFixedUpdate()
    {
        ApplyGravity();
        HandleGroundDetection();
        ApplyVerticalMovement();
    }

    private void ApplyGravity()
    {
        // Apply gravity only when not grounded
        if (!characterController.IsGrounded)
        {
            yVelocity += Mass * Physics.Gravity.Y * Time.DeltaTime;
        }
    }

    private void ApplyVerticalMovement()
    {
        if (characterController)
        {
            characterController.Move(new Vector3(0, yVelocity * Time.DeltaTime, 0));
        }
    }

    private void HandleGroundDetection()
    {
        // Reset velocity when landing
        if (characterController.IsGrounded && !isGroundedLastFrame)
        {
            yVelocity = 0;
        }

        // Update ground state
        isGroundedLastFrame = characterController.IsGrounded;
    }

    public void ResetGravity()
    {
        yVelocity = 0;
        isGroundedLastFrame = false;
    }
}