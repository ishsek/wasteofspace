using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public CharacterController CharacterController;
    public Rigidbody PlayerRigidBody;
    public Transform CameraTransform;
    public AudioSource ThrusterAudioSource;
    public float PitchShiftOnSlowDown = 0.5f;

    [Header("Tuning")]
    public SteamVR_Action_Vector2 Input;
    public bool UseUpAndDown = true;
    public SteamVR_Action_Single MoveUp;
    public SteamVR_Action_Single MoveDown;
    public float Speed;
    public float SlowDownSpeed;
    public float VerticalSpeed;
    public float PlayerMagnitudeLimit;

    [Header("Debug")]
    public float CurrentMagnitude;

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = Input.axis;
        float moveUpInput = MoveUp.GetAxis(SteamVR_Input_Sources.Any);
        float moveDownInput = MoveDown.GetAxis(SteamVR_Input_Sources.Any);

        if (moveInput.magnitude > 0.1f)
        {
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(moveInput.x, 0, moveInput.y));
            PlayerRigidBody.AddForce(Speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, CameraTransform.up), ForceMode.Force);

            //CharacterController.Move(Speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, CameraTransform.up));
        }

        if (UseUpAndDown)
        {
            if (moveUpInput > 0.1f)
            {
                CharacterController.Move(VerticalSpeed * Time.deltaTime * CameraTransform.up);
            }
            if (moveDownInput > 0.1f)
            {
                CharacterController.Move(-VerticalSpeed * Time.deltaTime * CameraTransform.up);
            }
        }

        if (moveUpInput > 0.1f && (PlayerRigidBody.velocity.magnitude > 0.1f))
        {
            PlayerRigidBody.velocity = PlayerRigidBody.velocity * SlowDownSpeed;

            if (PlayerRigidBody.velocity.magnitude <= 0.1f)
            {
                PlayerRigidBody.velocity = Vector3.zero;
            }
        }

        CurrentMagnitude = PlayerRigidBody.velocity.magnitude;
        if (PlayerRigidBody.velocity.magnitude > PlayerMagnitudeLimit)
        {
            PlayerRigidBody.velocity = PlayerRigidBody.velocity * SlowDownSpeed;
        }

        if ((moveInput.magnitude > 0.1f) || (moveUpInput > 0.1f))//|| (moveDownInput > 0.1f))
        {
            if ((moveUpInput > 0.1f) && (PlayerRigidBody.velocity.magnitude > 0.1f))
            {
                ThrusterAudioSource.pitch = PitchShiftOnSlowDown;
            }
            else
            {
                ThrusterAudioSource.pitch = 1f;
            }

            if ((moveUpInput > 0.1f) && (PlayerRigidBody.velocity.magnitude > 0.1f))
            {
                if (ThrusterAudioSource.isPlaying == false) ThrusterAudioSource.Play();
            }
            else if ((moveUpInput <= 0.1f) && (moveInput.magnitude > 0.1f))
            {
                if (ThrusterAudioSource.isPlaying == false) ThrusterAudioSource.Play();
            }
            else ThrusterAudioSource.Stop();
        }
        else
        {
            if (ThrusterAudioSource.isPlaying) ThrusterAudioSource.Stop();
        }
    }
}
