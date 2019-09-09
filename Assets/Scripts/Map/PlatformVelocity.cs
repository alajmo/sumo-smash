using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformVelocity : MonoBehaviour
{
    public GameObject Player;
    public float pushForce = 1000f;

    private bool playerOn;
    private Rigidbody player;

    private void Update() {
        if (playerOn)
        {
            Vector3 vec = new Vector3(0, 0, -1f);
            player.AddForce(vec * pushForce);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            playerOn = true;
            player = other.gameObject.GetComponent<Rigidbody>();

        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            playerOn = false;
            player = other.gameObject.GetComponent<Rigidbody>();
        }
    }
}
