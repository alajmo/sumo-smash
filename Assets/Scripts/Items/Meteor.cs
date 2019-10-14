
using System.Collections;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float meteorExplodeRadius = 2f;
    public float explodeForce = 30f;

    private IEnumerator explodeCoroutine;

    [Header("Layers")]
    public LayerMask playerLayer;

    void Awake()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Map"))
        {
            Explode();
        }
    }

    public void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, meteorExplodeRadius, playerLayer);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Debug.Log(hitColliders[i].gameObject);
            PushObject(hitColliders[i]);
            i++;
        }

        Destroy(this.gameObject);
    }

    void PushObject(Collider collider)
    {
        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
        Vector3 contactDirection = (collider.gameObject.transform.position - transform.position).normalized;
        rb.AddForce(contactDirection * explodeForce, ForceMode.Impulse);
    }
}