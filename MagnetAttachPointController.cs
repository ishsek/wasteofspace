using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetAttachPointController : MonoBehaviour
{
    [Header("References")]
    public MagnetController MainMagnetControllerScript;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(MainMagnetControllerScript.DebrisTag) && MainMagnetControllerScript.Pulling) 
        {
            DebrisObjectScript debrisController = other.GetComponent<DebrisObjectScript>();

            MainMagnetControllerScript.AttachObject(new MagnetController.AttachedObjectData()
            {
                AttachedObject = other.gameObject,
                AttachedObjectRigidBody = other.attachedRigidbody,
                AttachedObjectPullPoint = debrisController.PullPointChild,
                AttachDistance = debrisController.AttachDistance,
            });
        }
    }
}
