using FlaxEngine;

namespace Game;

/// <summary>
/// SpectatorModeOnG Script.
/// </summary>
public class SpectatorModeOnG : Script
{
    public Actor Player { get; set; }
    public Camera PlayerCamera { get; set; }
    public PlayerController PlayerControllerScript { get; private set; }

    public Camera FreeCameraActor { get; private set; }
    public FreeCamera FreeCameraScript { get; private set; }

    public override void OnStart()
    {
        PlayerControllerScript = Player.GetScript<PlayerController>();
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyboardKeys.G))
        {
            if (PlayerControllerScript.Enabled)
            {
                FreeCameraActor = new Camera();
                FreeCameraActor.AddScript<FreeCamera>();
                FreeCameraActor.Transform = Player.Transform;
                FreeCameraActor.Parent = Actor.Scene;
                PlayerControllerScript.Enabled = false;
                PlayerCamera.IsActive = false;
            }
            else
            {
                PlayerCamera.IsActive = true;
                PlayerControllerScript.Enabled = true;
                Destroy(FreeCameraActor);
            }
        }
    }
}
