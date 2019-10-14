using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public bool explodeOnImpact = false;
    public float grenadeRadius = 2f;
    public float timeToExplode = 2f;
    public float explodeForce = 30f;

    private IEnumerator explodeCoroutine;

    [Header("Layers")]
    public LayerMask playerLayer;

    void Awake()
    {
        explodeCoroutine = TimedExplode();
        StartCoroutine(explodeCoroutine);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && explodeOnImpact)
        {
            StopCoroutine(explodeCoroutine);
            PushObject(collision.collider);
            Destroy(this.gameObject);
        }
        else if (explodeOnImpact)
        {
            StopCoroutine(explodeCoroutine);
            Explode();
            Destroy(this.gameObject);
        }
    }

    public void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, grenadeRadius, playerLayer);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Debug.Log(hitColliders[i].gameObject);
            PushObject(hitColliders[i]);
            i++;
        }

        Destroy(this.gameObject);
    }

    public IEnumerator TimedExplode()
    {
        yield return new WaitForSeconds(timeToExplode);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, grenadeRadius, playerLayer);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Debug.Log(hitColliders[i].gameObject);
            PushObject(hitColliders[i]);
            i++;
        }

        if (true)
        {
            Destroy(this.gameObject);
        }
    }

    void PushObject(Collider collider)
    {
        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
        Vector3 contactDirection = (collider.gameObject.transform.position - transform.position).normalized;
        rb.AddForce(contactDirection * explodeForce, ForceMode.Impulse);
    }
}