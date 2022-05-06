using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MagnetController : MonoBehaviour
{
    public class AttachedObjectData
    {
        public GameObject AttachedObject;
        public Rigidbody AttachedObjectRigidBody;
        public Transform AttachedObjectPullPoint;
        public float AttachDistance;
    }

    [Header("References")]
    public string DebrisTag;
    public int DebrisLayer;
    public int GunLayer;
    public int PlayerLayer;
    public MagnetAttachPointController AttachPointController;
    public Transform LooseAttachPoint;
    public Transform OriginalAttachPoint;
    public AudioSource MagnetAudioSource;
    public NarrativeScriptingController NarrativeController;

    [Header("Audio")]
    public AudioClip MagnetPullAudio;
    public AudioClip MagnetAttachAudio;
    public AudioClip MagnetEjectAudio;

    [Header("Tuning")]
    public SteamVR_Action_Boolean AttractButton;
    public SteamVR_Action_Boolean EjectButton;
    public bool AllowPull = true;
    public bool PullButtonHeld = false;
    public bool EjectButtonHeld = false;
    public float PullForce;
    public float AttachedPower;
    public float EjectForce;

    public bool Pulling { get; private set; } = false;
    public AttachedObjectData AttachedObjData { get; private set; } = default;

    private void Update()
    {
        Pulling = AllowPull ? AttractButton.state : false;

        if (EjectButtonHeld)
        {
            Pulling = false;

            if (AttractButton.state == false)
            {
                EjectButtonHeld = false;
            }
        }

        if (Pulling)
        {
            if ((MagnetAudioSource.clip != MagnetPullAudio) || (MagnetAudioSource.isPlaying == false))
            {
                MagnetAudioSource.clip = MagnetPullAudio;
                MagnetAudioSource.loop = true;
                MagnetAudioSource.Play();
            }
        }
        else if ((MagnetAudioSource.clip == MagnetPullAudio) && (MagnetAudioSource.isPlaying))
        {
            MagnetAudioSource.Stop();
        }

        if (AttachedObjData != null)
        {
            AttachedObjData.AttachedObject.transform.position = Vector3.MoveTowards(AttachedObjData.AttachedObject.transform.position, LooseAttachPoint.transform.position, AttachedPower * Time.deltaTime);

            if ((AttractButton.state == false) && PullButtonHeld)
            {
                PullButtonHeld = false;
            }
            else if ((PullButtonHeld == false) && AttractButton.state)
            {
                EjectAttachedObject();
            }
        }
    }

    public void AttachObject(AttachedObjectData attachedObjData)
    {
        if ((AttachedObjData != null) && (AttachedObjData.AttachedObject == attachedObjData.AttachedObject))
        {
            return; 
        }

        Physics.IgnoreLayerCollision(DebrisLayer, GunLayer, true);
        Physics.IgnoreLayerCollision(DebrisLayer, PlayerLayer, true);

        AllowPull = false;
        Pulling = false;
        PullButtonHeld = true;
        AttachedObjData = attachedObjData;
        attachedObjData.AttachedObjectRigidBody.isKinematic = true;
        LooseAttachPoint.position = OriginalAttachPoint.position + (OriginalAttachPoint.forward * AttachedObjData.AttachDistance);

        MagnetAudioSource.Stop();
        MagnetAudioSource.clip = MagnetAttachAudio;
        MagnetAudioSource.loop = false;
        MagnetAudioSource.Play();
    }

    public void EjectAttachedObject()
    {
        AttachedObjData.AttachedObjectRigidBody.isKinematic = false;

        Physics.IgnoreLayerCollision(DebrisLayer, GunLayer, false);
        Physics.IgnoreLayerCollision(DebrisLayer, PlayerLayer, false);

        AttachedObjData.AttachedObjectRigidBody.AddForce(LooseAttachPoint.transform.forward * EjectForce);

        AllowPull = true;
        EjectButtonHeld = true;
        AttachedObjData = null;

        MagnetAudioSource.Stop();
        MagnetAudioSource.clip = MagnetEjectAudio;
        MagnetAudioSource.loop = false;
        MagnetAudioSource.Play();

        NarrativeController.EjectOccurred();
    }
}
