using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeScriptingController : MonoBehaviour
{
    public enum StoryStep
    {
        WaitForReady,
        SpaceshipTakeoff,
        OperatorIntroduction,
        OperatorIntroPickupMagnet,
        OperatorTestMagnet,
        UseMagnetToOpenDoor,
        ReachAirlock,
        AirlockOpening,
        ExitAirlock,
        Collecting,
        Collecting2,
        EjectDebris,
        OpeningEjectHatch,
        ReturnToAirlock,
        FadeOut,
        Complete,
    }

    [Header("References")]
    public AudioSource HelmetAudioPlayer;
    public AudioSource ShuttleAudioPlayer;

    [Header("Data")]
    public int DebrisStep1Target = 5;
    public int DebrisStep2Target = 10;
    public WaitForReadyData WaitForReadyStepData;
    public TakeOffData TakeOffStepData;
    public OperatorIntroductionData OperatorIntroductionStepData;
    public OperatorIntroPickupMagnetData OperatorIntroPickupMagnetStepData;
    public OperatorTestMagnetData OperatorTestMagnetStepData;
    public UseMagnetToOpenDoorData UseMagnetToOpenDoorStepData;
    public ReachAirlockData ReachAirlockStepData;
    public AirlockOpeningData AirlockOpeningStepData;
    public ExitAirlockData ExitAirlockStepData;
    public Collecting2Data Collecting2StepData;
    public EjectData EjectStepData;
    public OpeningDebrisHatchData OpeningDebrisHatchStepData;
    public ReturnToAirlockData ReturnToAirlockStepData;
    public FadeOutData FadeOutStepData;

    [Header("Status")]
    public StoryStep CurrentStep;
    public int DebrisCollected = 0;

    public void Start()
    {
        SetStep(StoryStep.WaitForReady);
    }

    public void SetStep(StoryStep newStep)
    {
        switch (newStep)
        {
            case StoryStep.WaitForReady:
                TakeOffStepData.FadeBackground.color = Color.black;
                TakeOffStepData.FadeBackground.gameObject.SetActive(true);
                break;

            case StoryStep.SpaceshipTakeoff:
                ShuttleAudioPlayer.clip = TakeOffStepData.CountdownAudioClip;
                ShuttleAudioPlayer.Play();
                TakeOffStepData.RemainingStepDuration = TakeOffStepData.StepDuration;
                break;

            case StoryStep.OperatorIntroduction:
                OperatorIntroductionStepData.FadeAnimator.SetTrigger(OperatorIntroductionStepData.FadeInTrigger);
                HelmetAudioPlayer.clip = OperatorIntroductionStepData.OperatorIntroClip;
                HelmetAudioPlayer.Play();
                OperatorIntroductionStepData.PlayAmbients();
                OperatorIntroductionStepData.RemainingInitialAudioTIme = OperatorIntroductionStepData.InitialAudioTime;
                OperatorIntroductionStepData.RepeatInstructionsRemainingTime = OperatorIntroductionStepData.RepeatInstructionsAfterTime;
                break;

            case StoryStep.OperatorIntroPickupMagnet:
                HelmetAudioPlayer.clip = OperatorIntroPickupMagnetStepData.OperatorIntroPickupMagnetClip;
                HelmetAudioPlayer.Play();
                OperatorIntroPickupMagnetStepData.RemainingInitialAudioTIme = OperatorIntroPickupMagnetStepData.InitialAudioTime;
                OperatorIntroPickupMagnetStepData.RepeatInstructionsRemainingTime = OperatorIntroPickupMagnetStepData.RepeatInstructionsAfterTime;
                break;

            case StoryStep.OperatorTestMagnet:
                OperatorTestMagnetStepData.ArrowMarkerGun.SetActive(false);
                OperatorTestMagnetStepData.ArrowMarkerAirlock.SetActive(true);
                HelmetAudioPlayer.clip = OperatorTestMagnetStepData.OperatorTestMagnetClip;
                HelmetAudioPlayer.Play();
                OperatorTestMagnetStepData.RemainingInitialAudioTIme = OperatorTestMagnetStepData.InitialAudioTime;
                OperatorTestMagnetStepData.RepeatInstructionsRemainingTime = OperatorTestMagnetStepData.RepeatInstructionsAfterTime;
                break;

            case StoryStep.UseMagnetToOpenDoor:
                UseMagnetToOpenDoorStepData.OpenDoorAnimator.SetTrigger(UseMagnetToOpenDoorStepData.OpenDoorTrigger);
                HelmetAudioPlayer.clip = UseMagnetToOpenDoorStepData.GetToAirlockAudioClip;
                HelmetAudioPlayer.Play();
                break;

            case StoryStep.ReachAirlock:
                ReachAirlockStepData.ArrowMarkerAirlock.SetActive(false);
                ReachAirlockStepData.RemainingStepDuration = ReachAirlockStepData.StepDuration;
                HelmetAudioPlayer.clip = ReachAirlockStepData.ReachAirlockAudioClip;
                HelmetAudioPlayer.Play();
                ReachAirlockStepData.VentParticleSystem.Play();
                ReachAirlockStepData.VentAudioSource.Play();
                break;

            case StoryStep.AirlockOpening:
                AirlockOpeningStepData.RemainingStepDuration = AirlockOpeningStepData.StepDuration;
                AirlockOpeningStepData.AirlockAnimator.SetTrigger(AirlockOpeningStepData.AirlockOpenTrigger);
                break;

            case StoryStep.ExitAirlock:
                ExitAirlockStepData.RemainingStepDuration = ExitAirlockStepData.StepDuration;
                HelmetAudioPlayer.clip = ExitAirlockStepData.IntroCleanupAudio;
                HelmetAudioPlayer.Play();
                break;

            case StoryStep.Collecting2:
                // Play halfway completion audio
                HelmetAudioPlayer.clip = Collecting2StepData.HalfwayCollectionAudioClip;
                HelmetAudioPlayer.Play();
                break;

            case StoryStep.EjectDebris:
                // Play ejecting audio
                EjectStepData.RemainingStepDuration = EjectStepData.StepDuration;
                HelmetAudioPlayer.clip = EjectStepData.EjectAudioClip;
                HelmetAudioPlayer.Play();
                break;

            case StoryStep.OpeningEjectHatch: 
                OpeningDebrisHatchStepData.EjectDoorAnimator.SetTrigger(OpeningDebrisHatchStepData.EjectAnimationTrigger);
                OpeningDebrisHatchStepData.RemainingStepDuration = OpeningDebrisHatchStepData.StepDuration;
                break;

            case StoryStep.ReturnToAirlock:
                ReturnToAirlockStepData.ArrowMarkerContainer.SetActive(false);
                ReturnToAirlockStepData.ArrowMarkerAirlock.SetActive(true);
                ReturnToAirlockStepData.EjectDebris();
                HelmetAudioPlayer.clip = ReturnToAirlockStepData.ReturnToAirlockClip;
                HelmetAudioPlayer.Play();
                break;

            case StoryStep.FadeOut:
                FadeOutStepData.RemainingStepDuration = FadeOutStepData.StepDuration;
                FadeOutStepData.FadeAnimator.SetTrigger(FadeOutStepData.FadeOutTrigger);
                break;

            default:
                break;
        }

        CurrentStep = newStep;
    }

    public void Update()
    {
        switch (CurrentStep)
        {
            case StoryStep.WaitForReady:
                if (Input.GetKeyDown(WaitForReadyStepData.ReadyButton))
                {
                    SetStep(StoryStep.SpaceshipTakeoff);
                }
                break;

            case StoryStep.SpaceshipTakeoff:
                TakeOffStepData.RemainingStepDuration -= Time.deltaTime;
                if (!TakeOffStepData.RocketAudioPlayed && (TakeOffStepData.RemainingStepDuration <= TakeOffStepData.StartRocketTime))
                {
                    ShuttleAudioPlayer.Stop();
                    ShuttleAudioPlayer.clip = TakeOffStepData.RocketAudioClip;
                    ShuttleAudioPlayer.Play();
                    TakeOffStepData.RocketAudioPlayed = true;
                }
                if (TakeOffStepData.RemainingStepDuration <= 0)
                {
                    SetStep(StoryStep.OperatorIntroduction);
                }
                break;

            case StoryStep.OperatorIntroduction:
                OperatorIntroductionStepData.UpdateStepData(HelmetAudioPlayer);
                break;

            case StoryStep.OperatorIntroPickupMagnet:
                OperatorIntroPickupMagnetStepData.UpdateStepData(HelmetAudioPlayer);
                break;

            case StoryStep.OperatorTestMagnet:
                OperatorTestMagnetStepData.UpdateStepData(HelmetAudioPlayer);
                break;

            case StoryStep.ReachAirlock:
                ReachAirlockStepData.RemainingStepDuration -= Time.deltaTime;
                if (ReachAirlockStepData.RemainingStepDuration <= 0f)
                {
                    SetStep(StoryStep.AirlockOpening);
                }
                break;

            case StoryStep.AirlockOpening:
                AirlockOpeningStepData.RemainingStepDuration -= Time.deltaTime;
                if (AirlockOpeningStepData.RemainingStepDuration <= 0f)
                {
                    SetStep(StoryStep.ExitAirlock);
                }
                break;

            case StoryStep.ExitAirlock:
                ExitAirlockStepData.RemainingStepDuration -= Time.deltaTime;
                if (ExitAirlockStepData.RemainingStepDuration <= 0f)
                {
                    SetStep(StoryStep.Collecting);
                }
                break;

            case StoryStep.EjectDebris:
                EjectStepData.RemainingStepDuration -= Time.deltaTime;
                if (EjectStepData.RemainingStepDuration <= 0f)
                {
                    SetStep(StoryStep.OpeningEjectHatch);
                }
                break;

            case StoryStep.OpeningEjectHatch:
                OpeningDebrisHatchStepData.RemainingStepDuration -= Time.deltaTime;
                if (OpeningDebrisHatchStepData.RemainingStepDuration <= 0f)
                {
                    SetStep(StoryStep.ReturnToAirlock);
                }
                break;

            case StoryStep.FadeOut:
                FadeOutStepData.RemainingStepDuration -= Time.deltaTime;
                if (FadeOutStepData.RemainingStepDuration <= 0f)
                {
                    SetStep(StoryStep.Complete);
                }
                break;
 
            default:
                break;
        }
    }

    public void GunStationEntered()
    {
        if (CurrentStep == StoryStep.OperatorIntroduction)
        {
            SetStep(StoryStep.OperatorIntroPickupMagnet);
        }
    }

    public void GunPickedUp()
    {
        if (CurrentStep == StoryStep.OperatorIntroPickupMagnet)
        {
            SetStep(StoryStep.OperatorTestMagnet);
        }
    }

    public void EjectOccurred()
    {
        if (CurrentStep == StoryStep.OperatorTestMagnet)
        {
            SetStep(StoryStep.UseMagnetToOpenDoor);
        }
    }

    public void AirlockEntered()
    {
        if (CurrentStep == StoryStep.UseMagnetToOpenDoor)
        {
            SetStep(StoryStep.ReachAirlock);
        }
        else if (CurrentStep == StoryStep.ReturnToAirlock)
        {
            SetStep(StoryStep.FadeOut);
        }
    }

    public void DebrisPieceCollected()
    {
        DebrisCollected++;

        if ((CurrentStep == StoryStep.Collecting) && (DebrisCollected >= DebrisStep1Target))
        {
            SetStep(StoryStep.Collecting2);
        }
        else if ((CurrentStep == StoryStep.Collecting2) && (DebrisCollected >= DebrisStep2Target))
        {
            SetStep(StoryStep.EjectDebris); 
        }
    }
}
