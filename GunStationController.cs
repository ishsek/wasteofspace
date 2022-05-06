using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStationController : MonoBehaviour
{
    public NarrativeScriptingController NarrativeController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PlayerTag))
        {
            NarrativeController.GunStationEntered();
        }
    }
}
