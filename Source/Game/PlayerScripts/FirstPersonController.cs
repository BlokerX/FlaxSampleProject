using FlaxEngine;

namespace Game;

/// <summary>
/// FirstPersonController Script.
/// </summary>
public class FirstPersonController : Script
{
    public CharacterController CharacterControllerScript { get; set; }
    public Camera CameraScript { get; set; }

    public RigidBody RigidBodyScript { get; set; }

    [Limit(0, 100), Tooltip("Character movement speed factor")]
    public float MoveSpeed { get; set; } = 4;

    [Tooltip("Camera rotation smoothing factor")]
    public float CameraSmoothing { get; set; } = 20.0f;

    private float pitch;
    private float yaw;

    /// <inheritdoc/>
    public override void OnStart()
    {
        RigidBodyScript.LocalPosition = Vector3.Zero;

        CameraScript.LocalPosition = new Vector3(0, 55, 0);
        CameraScript.LocalOrientation = Quaternion.Euler(0, 0, 0);
        CameraScript.Parent = Actor;

        var initialEulerAngles = Actor.Orientation.EulerAngles;
        pitch = initialEulerAngles.X;
        yaw = initialEulerAngles.Y;
    }

    /// <inheritdoc/>
    public override void OnEnable()
    {
        // Here you can add code that needs to be called when script is enabled (eg. register for events)
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        Screen.CursorVisible = false;
        Screen.CursorLock = CursorLockMode.Locked;

        var mouseDelta = new Float2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        pitch = Mathf.Clamp(pitch + mouseDelta.Y, -88, 88);
        yaw += mouseDelta.X;
    }

    public override void OnFixedUpdate()
    {
        var trans = Actor.Transform;
        var camFactor = Mathf.Saturate(CameraSmoothing * Time.DeltaTime);

        trans.Orientation = Quaternion.Lerp(trans.Orientation, Quaternion.Euler(0, yaw, 0), camFactor);
        CameraScript.Orientation = Quaternion.Lerp(CameraScript.Orientation, Quaternion.Euler(pitch, yaw, 0), camFactor);

        var inputH = Input.GetAxis("Horizontal");
        var inputV = Input.GetAxis("Vertical");
        var inputJump = Input.GetKey(KeyboardKeys.Spacebar);
        var inputSneak = Input.GetKey(KeyboardKeys.Shift);
        var move = new Vector3(inputH, 0, inputV);
        if (inputJump)
            Actor.AddMovement(new Vector3(0, 1, 0));
        if (inputSneak)
            Actor.AddMovement(new Vector3(0, -1, 0));
        move.Normalize();
        move = trans.TransformDirection(move);



        trans.Translation += move * MoveSpeed;

        Actor.Transform = trans;
    }
}
