using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrampoline : MonoBehaviour
{
    public float bounceForce = 2000f;

    private void OnTriggerEnter(Collider other) {
        Rigidbody player = other.gameObject.GetComponent<Rigidbody>();
        player.AddForce(Vector3.up * bounceForce);
    }
}
