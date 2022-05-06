using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DataClasses {}


[System.Serializable]
public class WaitForReadyData
{
    public KeyCode ReadyButton;
}

[System.Serializable]
public class TakeOffData
{
    public Image FadeBackground;
    public AudioClip CountdownAudioClip;
    public AudioClip RocketAudioClip;
    public bool RocketAudioPlayed;
    public float StepDuration;
    public float StartRocketTime;

    public float RemainingStepDuration = 0f;
}

[System.Serializable]
public class OperatorIntroductionData
{
    public AudioClip OperatorIntroClip;
    public List<AudioSource> SpaceShipAmbientSources;
    public Animator FadeAnimator;
    public string FadeInTrigger = "FadeIn";

    public AudioClip RepeatInstructionsClip;
    public float InitialAudioTime;
    public float RemainingInitialAudioTIme;
    public float RepeatInstructionsAfterTime;
    public float RepeatInstructionsRemainingTime;

    public void PlayAmbients()
    {
        foreach (AudioSource ambientSource in SpaceShipAmbientSources)
        {
            ambientSource.Play();
        }
    }

    public void UpdateStepData(AudioSource helmetAudioPlayer)
    {
        if (RemainingInitialAudioTIme > 0)
        {
            RemainingInitialAudioTIme -= Time.deltaTime;
        }

        if (RemainingInitialAudioTIme <= 0)
        {
            RepeatInstructionsRemainingTime -= Time.deltaTime;
            if (RepeatInstructionsRemainingTime <= 0f)
            {
                helmetAudioPlayer.clip = RepeatInstructionsClip;
                helmetAudioPlayer.Play();
                RepeatInstructionsRemainingTime = RepeatInstructionsAfterTime;
            }
        }
    }
}

[System.Serializable]
public class OperatorIntroPickupMagnetData
{
    public AudioClip OperatorIntroPickupMagnetClip;
    
    public AudioClip RepeatInstructionsClip;
    public float InitialAudioTime;
    public float RemainingInitialAudioTIme;
    public float RepeatInstructionsAfterTime;
    public float RepeatInstructionsRemainingTime;

    public void UpdateStepData(AudioSource helmetAudioPlayer)
    {
        if (RemainingInitialAudioTIme > 0)
        {
            RemainingInitialAudioTIme -= Time.deltaTime;
        }

        if (RemainingInitialAudioTIme <= 0)
        {
            RepeatInstructionsRemainingTime -= Time.deltaTime;
            if (RepeatInstructionsRemainingTime <= 0f)
            {
                helmetAudioPlayer.clip = RepeatInstructionsClip;
                helmetAudioPlayer.Play();
                RepeatInstructionsRemainingTime = RepeatInstructionsAfterTime;
            }
        }
    }
}

[System.Serializable]
public class OperatorTestMagnetData
{
    public AudioClip OperatorTestMagnetClip;
    public GameObject ArrowMarkerGun;
    public GameObject ArrowMarkerAirlock;

    public AudioClip RepeatInstructionsClip;
    public float InitialAudioTime;
    public float RemainingInitialAudioTIme;
    public float RepeatInstructionsAfterTime;
    public float RepeatInstructionsRemainingTime;

    public void UpdateStepData(AudioSource helmetAudioPlayer)
    {
        if (RemainingInitialAudioTIme > 0)
        {
            RemainingInitialAudioTIme -= Time.deltaTime;
        }

        if (RemainingInitialAudioTIme <= 0)
        {
            RepeatInstructionsRemainingTime -= Time.deltaTime;
            if (RepeatInstructionsRemainingTime <= 0f)
            {
                helmetAudioPlayer.clip = RepeatInstructionsClip;
                helmetAudioPlayer.Play();
                RepeatInstructionsRemainingTime = RepeatInstructionsAfterTime;
            }
        }
    }
}

[System.Serializable]
public class UseMagnetToOpenDoorData
{
    public Animator OpenDoorAnimator;
    public string OpenDoorTrigger = "OpenDoor";
    public AudioClip GetToAirlockAudioClip;
}



[System.Serializable]
public class ReachAirlockData
{
    public GameObject ArrowMarkerAirlock;
    public AudioClip ReachAirlockAudioClip;
    public ParticleSystem VentParticleSystem;
    public AudioSource VentAudioSource;
    public float StepDuration;

    public float RemainingStepDuration = 0f;
}

[System.Serializable]
public class AirlockOpeningData
{
    public Animator AirlockAnimator;
    public string AirlockOpenTrigger = "AirlockOpen";
    public float StepDuration;

    public float RemainingStepDuration = 0f;
}

[System.Serializable]
public class ExitAirlockData
{
    public AudioClip IntroCleanupAudio;
    public float StepDuration;

    public float RemainingStepDuration = 0f;
}

[System.Serializable]
public class Collecting2Data
{
    public AudioClip HalfwayCollectionAudioClip;
}


[System.Serializable]
public class EjectData
{
    public AudioClip EjectAudioClip;
    public float StepDuration;

    public float RemainingStepDuration = 0f;
}

[System.Serializable]
public class OpeningDebrisHatchData
{
    public Animator EjectDoorAnimator;
    public string EjectAnimationTrigger = "OpenHatch";
    public float StepDuration;

    public float RemainingStepDuration = 0f;
}

[System.Serializable]
public class ReturnToAirlockData
{
    public GameObject ArrowMarkerContainer;
    public GameObject ArrowMarkerAirlock;
    public AudioClip ReturnToAirlockClip;
    public AudioSource EjectAudioSource;
    public Transform DebrisEjectDirection;
    public float DebrisEjectForce = 1;
    public int DebrisCollectedLayer;
    public GameObject DebrisContainer;
    public List<GameObject> CollectedDebrisList;

    public void FindCollectedDebris()
    {
        foreach (Transform debrisPiece in DebrisContainer.transform)
        {
            if (debrisPiece.gameObject.layer == DebrisCollectedLayer)
            {
                CollectedDebrisList.Add(debrisPiece.gameObject);
            }
        }
    }

    public void EjectDebris()
    {
        FindCollectedDebris();

        foreach (var debrisPiece in CollectedDebrisList)
        {
            debrisPiece.GetComponent<Rigidbody>().AddForce(DebrisEjectDirection.forward * DebrisEjectForce);
        }

        EjectAudioSource.Play();
    }
}


[System.Serializable]
public class FadeOutData
{
    public Animator FadeAnimator;
    public string FadeOutTrigger = "FadeOut";
    public float StepDuration;


    public float RemainingStepDuration = 0f;
}