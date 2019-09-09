using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    public int cubeId = 0;

    private void OnTriggerEnter(Collider other) {
        hideCube();
    }

    private void OnTriggerExit(Collider other) {
        showCube();
    }

    private void hideCube()
    {
        foreach (Transform child in transform.parent)
        {
            if (child.tag == "TrapDoor")
            {
                int id = child.GetComponent<TrapDoor>().cubeId;
                if (id == cubeId)
                {
                    MeshCollider[] meshColliders = child.GetComponents<MeshCollider>();
                    child.GetComponent<MeshRenderer>().enabled = false;
                    foreach(MeshCollider meshCollider in meshColliders)
                    {
                        meshCollider.enabled = false;
                    }
                }
            }
        }
    }

    private void showCube()
    {
        foreach (Transform child in transform.parent)
        {
            if (child.tag == "TrapDoor")
            {
                int id = child.GetComponent<TrapDoor>().cubeId;
                if (id == cubeId)
                {
                    MeshCollider[] meshColliders = child.GetComponents<MeshCollider>();
                    child.GetComponent<MeshRenderer>().enabled = true;
                    foreach(MeshCollider meshCollider in meshColliders)
                    {
                        meshCollider.enabled = true;
                    }
                }
            }
        }
    }
}
