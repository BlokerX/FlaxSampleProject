using FlaxEngine;

namespace Game;

/// <summary>
/// PlayerController Script.
/// </summary>
public class PlayerController : Script
{
    public CharacterController characterController { get; set; }

    #region Camera

    public Camera camera { get; set; }
    public Actor cameraHolder { get; set; }
    public CameraView cameraView { get; set; } = CameraView.FirstPerson;

    public float mouseSensitivity = 1;
    public float mouseScrollSensitivity = 10;

    private float mouseX;

    private float mouseY;

    //third person camera properties:
    public float cameraDistance { get; set; } = 500;
    public float minimumCameraDistance { get; set; } = 160;
    public float maximumCameraDistance { get; set; } = 800;

    #endregion

    #region Movement

    public float Speed { get; set; } = 1000;
    public float AccelerationMultiplier { get; set; } = 1.75f;
    public float SlowdownMultiplier { get; set; } = 0.5f;

    public float Mass { get; set; } = 2f;
    private float yVelocity { get; set; }
    public float JumpForce { get; set; } = 1000;
    private int jumpCount { get; set; } = 0;


    #endregion

    public override void OnStart()
    {
        Screen.CursorLock = CursorLockMode.Locked;
        Screen.CursorVisible = false;

        switch (cameraView)
        {
            case CameraView.FirstPerson:
                SetFirstPersonCameraView();
                break;
            case CameraView.ThirdPerson:
                SetThirdPersonCameraView();
                break;
        }
    }

    public override void OnUpdate()
    {
        #region Camera View Switching

        if (Input.GetKeyDown(KeyboardKeys.V))
        {
            if (cameraView == CameraView.FirstPerson)
            {
                cameraView = CameraView.ThirdPerson;
                SetThirdPersonCameraView();
            }
            else
            {
                cameraView = CameraView.FirstPerson;
                SetFirstPersonCameraView();
            }
        }

        #endregion

        #region Camera Movement

        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY += Input.GetAxis("Mouse Y") * mouseSensitivity;

        switch (cameraView)
        {
            case CameraView.FirstPerson:
                FirstPersonCameraMove();
                break;
            case CameraView.ThirdPerson:
                var cameraDistanceChanger = Input.GetAxis("CameraApproximation") * mouseScrollSensitivity;
                if (cameraDistanceChanger != 0)
                {
                    cameraDistance += cameraDistanceChanger;
                    cameraDistance = Mathf.Clamp(cameraDistance, minimumCameraDistance, maximumCameraDistance);
                    camera.LocalPosition = Vector3.Lerp(camera.LocalPosition, new Vector3(0, 0, -cameraDistance), 10 * Time.DeltaTime);
                }
                ThirdPersonCameraMove();
                break;
        }

        #endregion

        #region Jump

        if (Input.GetAction("Jump"))
        {
            if (jumpCount == 0)
            {
                yVelocity = JumpForce;
                jumpCount++;
            }
        }

        #endregion

    }

    private void ThirdPersonCameraMove()
    {
        mouseY = Mathf.Clamp(mouseY, -90, 90);

        cameraHolder.LocalEulerAngles = new Float3(mouseY, 0, 0);
        Actor.EulerAngles = new Float3(0, mouseX, 0);
    }

    private void FirstPersonCameraMove()
    {
        mouseY = Mathf.Clamp(mouseY, -90, 90);

        camera.LocalEulerAngles = new Vector3(mouseY, 0, 0);
        Actor.EulerAngles = new Vector3(0, mouseX, 0);
    }

    public override void OnFixedUpdate()
    {
        #region Movement

        Vector3 moveInput = new Vector3();

        moveInput.X = Input.GetAxis("Horizontal");
        moveInput.Z = Input.GetAxis("Vertical");

        moveInput.Normalize();


        Vector3 lookRotation = new Vector3(0, Actor.LocalEulerAngles.Y, 0);
        Vector3 direction = Vector3.Transform(moveInput, Quaternion.Euler(lookRotation));

        float slowdown = Input.GetAction("Sneak") ? SlowdownMultiplier : 1;
        float acceleration = Input.GetAction("Sprint") ? AccelerationMultiplier : 1;

        Vector3 velocity = direction * Speed * (acceleration * slowdown) * Time.DeltaTime;
        characterController.Move(velocity);

        #endregion

        #region Jump and Gravity

        yVelocity += Mass * Physics.Gravity.Y * Time.DeltaTime; // Gravity

        characterController.Move(new Vector3(0, yVelocity * Time.DeltaTime, 0));

        if (characterController.IsGrounded)
        {
            jumpCount = 0;
            yVelocity = 0;
        }


        //if (yVelocity < 0)
        //    Debug.Log("Current yVelocity < 0");
        //else if (yVelocity > 0)
        //    Debug.Log("Current yVelocity > 0");
        //else if (yVelocity == 0)
        //    Debug.Log("Current yVelocity = " + yVelocity);

        #endregion
    }

    private void SetFirstPersonCameraView()
    {
        cameraHolder.LocalPosition = new Vector3(0, 0, 0);
        cameraHolder.LocalEulerAngles = new Float3(0, 0, 0);

        camera.LocalEulerAngles = new Float3(0, 0, 0);
        camera.LocalPosition = new Vector3(0, 75, 0);
    }

    private void SetThirdPersonCameraView()
    {
        cameraHolder.LocalPosition = new Vector3(0, 0, 0);
        cameraHolder.LocalEulerAngles = new Float3(0, 0, 0);

        camera.LocalPosition = new Vector3(0, 0, -cameraDistance);
        camera.LocalEulerAngles = new Float3(0, 0, 0);
    }

    public enum CameraView
    {
        FirstPerson,
        ThirdPerson
    }

    #region Dodatkowe

    public void Remove()
    {
        Destroy(Actor);
    }

    public void OnPlayerDeath()
    {
        // Here you can add code that needs to be called when player dies
    }

    public void OnPlayerRespawn()
    {
        // Here you can add code that needs to be called when player respawns
    }

    #endregion
}
