using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttachPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MainCharacterController>() != null)
        {
            other.transform.SetParent(transform, true);
            other.GetComponent<MainCharacterController>().YouAreOnPlatform(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<MainCharacterController>() != null)
        {
            other.transform.SetParent(null, true);
            other.GetComponent<MainCharacterController>().YouAreOnPlatform(false);
        }
    }
}
