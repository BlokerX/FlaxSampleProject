using FlaxEngine;

namespace Game;

/// <summary>
/// TriggerSample Script.
/// </summary>
public class TriggerSample : Script
{
    [Serialize]
    private bool _lightOn;

    public Light LightToControl;

    [NoSerialize]
    public bool LightOn
    {
        get { return _lightOn; }
        set
        {
            _lightOn = value;
            if (LightToControl)
                if (value)
                {
                    LightToControl.Color = Color.DarkBlue;
                    LightToControl.Brightness = 10.0f;
                }
                else
                {
                    LightToControl.Color = Color.Red;
                    LightToControl.Brightness = 1.0f;
                }
        }
    }

    public override void OnStart()
    {
        // Restore state
        LightOn = _lightOn;
    }

    public override void OnUpdate()
    {
        // Check for light
        if (LightToControl)
        {
            // Toggle light
            if (Input.GetAction("ToggleLight"))
            {
                LightOn = !LightOn;
            }
        }

        LightToControl.Color = GetRainbowColor(LightToControl.Color, Time.GameTime);
    }

    public static Color GetRainbowColor(Color color, float time)
    {
        float hue = (time * 50) % 360; // Zmiana koloru w zakresie 0-360 stopni
        return Color.FromHSV(hue, 1.0f, 1.0f); // Pełna saturacja i jasność
    }

    public override void OnEnable()
    {
        // Register for event
        Actor.As<Collider>().TriggerEnter += OnTriggerEnter;
        Actor.As<Collider>().TriggerExit += OnTriggerExit;
    }

    public override void OnDisable()
    {
        // Unregister for event
        Actor.As<Collider>().TriggerEnter -= OnTriggerEnter;
        Actor.As<Collider>().TriggerExit -= OnTriggerExit;
    }

    void OnTriggerEnter(PhysicsColliderActor collider)
    {
        // Check for player
        if (collider is CharacterController)
        {
            LightOn = true;
        }
    }

    void OnTriggerExit(PhysicsColliderActor collider)
    {
        // Check for player
        if (collider is CharacterController)
        {
            LightOn = false;
        }
    }
}
