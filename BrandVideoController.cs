using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BrandVideoController : MonoBehaviour
{
    public VideoPlayer BrandVideoPlayer;

    private void Start()
    {
        BrandVideoPlayer.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PlayerTag))
        {
            if (BrandVideoPlayer.isPlaying == false)
            {
                BrandVideoPlayer.Stop();
                BrandVideoPlayer.Play();
            }
         }
    }
}
