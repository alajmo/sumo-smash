using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBreakable : MonoBehaviour
{
    public float removeInSeconds = 2f;
    public float appearInSeconds = 2f;

    private void OnTriggerEnter(Collider other) {
        StartCoroutine(TemporarilyDisablePlatform());
    }

    IEnumerator TemporarilyDisablePlatform()
    {
        yield return new WaitForSeconds(removeInSeconds);

        MeshCollider[] meshColliders = GetComponents<MeshCollider>();
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        foreach(MeshCollider meshCollider in meshColliders)
        {
            meshCollider.enabled = false;
        }
        
        yield return new WaitForSeconds(appearInSeconds);
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        foreach(MeshCollider meshCollider in meshColliders)
        {
            meshCollider.enabled = true;
        }
    }
}
