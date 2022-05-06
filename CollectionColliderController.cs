using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionColliderController : MonoBehaviour
{
    public NarrativeScriptingController NarrativeController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Constants.DebrisLayer)
        {
            other.gameObject.layer = Constants.DebrisCollectedLayer;
            NarrativeController.DebrisPieceCollected();
        }
    }
}
