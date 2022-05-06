using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAudioPlayer : MonoBehaviour
{
    public AudioSource SourceToUse;

    public void PlayAnimAudio()
    {
        SourceToUse.Play();
    }
}
