using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    [Header("Force")]
    public float force = 10f;
    public float yAngle = 0.75f;

    public void fire(GameObject grenade, Vector3 position, Vector3 direction, float fireTime) {
        Debug.Log("FireTime: " + fireTime);

        float time = 0.5f; 
        if (fireTime > 1) {
            time = 1;
        } 

        // Set coordinates and forward direction
        grenade.transform.forward = direction + new Vector3(0, 0.75f, 0);

        // Launch grenade
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(grenade.transform.forward * time * force, ForceMode.Impulse);

        RaycastHit hitInfo;
        Ray ray = new Ray(position, direction);
        if (Physics.Raycast(ray, out hitInfo, 2f)) {
            Debug.DrawRay(position, direction, Color.red, 10);
        } else {
            Debug.DrawRay(position, direction, Color.red, 10);
        }

        // StartCoroutine(grenade.GetComponent<Grenade>().ExplodeGrenade());
    }
}
