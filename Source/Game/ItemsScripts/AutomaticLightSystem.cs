using FlaxEngine;
using System.Collections.Generic;

namespace Game;

public class AutomaticLightSystem : Script
{
    public List<(LightElement, Collider)> lightElements { get; set; } = new();

    public float animationTimer { get; set; } = 50.0f;
    public bool isColorAnimation { get; set; } = true;

    public Color startColor { get; set; } = Color.White;

    public override void OnUpdate()
    {
        if (isColorAnimation)
            ColorCycleAnimation();
    }

    private void ColorCycleAnimation()
    {
        Color c = GetRainbowColor(Time.GameTime, animationTimer);
        foreach (var element in lightElements)
        {
            // Check for light
            if (element.Item1.light)
            {
                // Toggle light
                if (Input.GetAction("ToggleLight"))
                {
                    element.Item1.LightOn = !element.Item1.LightOn;
                }
            }

            element.Item1.light.Color = c;
        }
    }

    public static Color GetRainbowColor(float time, float animationTimer)
    {
        float hue = (time * animationTimer) % 360; // Zmiana koloru w zakresie 0-360 stopni
        return Color.FromHSV(hue, 1.0f, 1.0f); // Pełna saturacja i jasność
    }

    public override void OnStart()
    {
        foreach (var element in lightElements)
        {
            element.Item1.OnStart();
            element.Item1.light.Color = startColor;
        }
    }

    public override void OnEnable()
    {
        foreach (var element in lightElements)
        {
            if (element.Item2 is not null)
            {
                // Register for event
                element.Item2.As<Collider>().TriggerEnter += element.Item1.OnTriggerEnter;
                element.Item2.As<Collider>().TriggerExit += element.Item1.OnTriggerExit;
            }
        }
    }

    public override void OnDisable()
    {
        foreach (var element in lightElements)
        {
            if (element.Item2 is not null)
            {
                // Unregister for event
                element.Item2.As<Collider>().TriggerEnter -= element.Item1.OnTriggerEnter;
                element.Item2.As<Collider>().TriggerExit -= element.Item1.OnTriggerExit;
            }
        }
    }

}

public class LightElement
{
    [Serialize]
    private bool _lightOn;
    public Light light;
    public float brightnessOn { get; set; } = 10.0f;
    public float brightnessOff { get; set; } = 0.0f;

    [NoSerialize]
    public bool LightOn
    {
        get => _lightOn;
        set
        {
            _lightOn = value;
            light.Brightness = (light && value) ? brightnessOn : brightnessOff;
        }
    }

    public void OnStart()
    {
        // Restore state
        LightOn = _lightOn;
    }

    #region TriggerEvents

    public void OnTriggerEnter(PhysicsColliderActor collider)
    {
        // Check for player
        if (collider is CharacterController)
        {
            LightOn = true;
        }
    }

    public void OnTriggerExit(PhysicsColliderActor collider)
    {
        // Check for player
        if (collider is CharacterController)
        {
            LightOn = false;
        }
    }

    #endregion

}