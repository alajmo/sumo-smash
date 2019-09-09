using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurricane : MonoBehaviour
{

    private MapPosition mapPositionScript;
    private RandomPosition randomPositionScript;
    private List<GameObject> mapColliders = new List<GameObject>();

    [Header("Attributes")]
    public float pullRadius = 20;
    public float pullForce = 1000;
    public float hurricaneRadius = 2;

    private Coroutine hurricaneCoroutine;

    void Update()
    {
        // Draw players in
        foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius))
        {
            Vector3 forceDirection = transform.position - collider.transform.position;
            // Allow player to fall through
            forceDirection.y = 0;

            if (collider.gameObject.CompareTag("Player"))
            {
                float dist = Mathf.Abs(Vector3.Distance(transform.position, collider.transform.position));
                collider.GetComponent<Rigidbody>().AddForce(
                    forceDirection.normalized *
                    pullForce *
                    ((pullRadius - dist) / pullRadius) *
                    Time.fixedDeltaTime);
            }
        }

        // Don't display surrounding ground temporarily.
        foreach (Collider collider in Physics.OverlapSphere(transform.position, hurricaneRadius))
        {
            if (collider.gameObject.CompareTag("Map"))
            {
                mapColliders.Add(collider.gameObject);
                collider.gameObject.SetActive(false);
            }
        }
    }

    void OnDestroy()
    {
        // Bring back the elements the hurricane temporarily disabled.
        foreach (GameObject mapItem in mapColliders)
        {
            if (mapItem) {
                mapItem.SetActive(true);
            }
        }
    }
}
