using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MagnetCollisionController : MonoBehaviour
{
    [Header("References")]
    public MagnetController MainMagnetControllerScript;
    public Transform PullPointTransform;

    private void OnTriggerStay(Collider other)
    {
        if (MainMagnetControllerScript.Pulling && other.CompareTag(MainMagnetControllerScript.DebrisTag))
        {
            Transform otherObjectPullDirection = other.transform.GetChild(0);
            otherObjectPullDirection.LookAt(PullPointTransform);
            other.attachedRigidbody.AddForce(otherObjectPullDirection.forward * MainMagnetControllerScript.PullForce * Time.deltaTime);
        }
    }
}
