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
    public bool hurricaneActive = false;

    public float torqueForceMin = 0.5f;
    public float torqueForceMax = 1f;
    public float downwardForceMin = 1f;
    public float downwardForceMax = 3f;

    private Coroutine hurricaneCoroutine;

    void Start() {
        StartHurricane();
    }

    void StartHurricane() {
        // Don't display surrounding ground temporarily.
        foreach (Collider collider in Physics.OverlapSphere(transform.position, hurricaneRadius)) {
            if (collider.gameObject.CompareTag("Map")) {
                StartCoroutine(StartTileHurricane(collider.gameObject));
                mapColliders.Add(collider.gameObject);
            }
        }
    }

    IEnumerator StartTileHurricane(GameObject tile) {
        // 1. Emit particles
        TileParticle ps = tile.GetComponent<TileParticle>();
        ps.PlayAir();

        // 2. Add gravity and let tiles fall down
        yield return new WaitForSeconds(1);
        MeshCollider mc = tile.GetComponent<MeshCollider>();
        Rigidbody rb = tile.GetComponent<Rigidbody>();
        mc.convex = true;
        rb.isKinematic = false;
        rb.useGravity = true;
        AddTileForce(rb);

        // 3. Enable drag
        hurricaneActive = true;

        yield return new WaitForSeconds(10);
        Destroy(tile);
    }

    void Update() {
        if (hurricaneActive ) {
            // Draw players in
            foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius)) {
                Vector3 forceDirection = transform.position - collider.transform.position;
                // Allow player to fall through
                forceDirection.y = 0;

                if (collider.gameObject.CompareTag("Player")) {
                    float dist = Mathf.Abs(Vector3.Distance(transform.position, collider.transform.position));
                    collider.GetComponent<Rigidbody>().AddForce(
                        forceDirection.normalized *
                        pullForce *
                        ((pullRadius - dist) / pullRadius) *
                        Time.fixedDeltaTime);
                }
            }
        }
    }

    void AddTileForce(Rigidbody rb) {
        float xRotation= UnityEngine.Random.Range(1, 3);
        float yRotation= UnityEngine.Random.Range(1, 3);
        float zRotation= UnityEngine.Random.Range(1, 3);
        rb.AddTorque(new Vector3(xRotation, yRotation, zRotation) * UnityEngine.Random.Range(torqueForceMin, torqueForceMin));
        rb.AddForce(transform.forward * UnityEngine.Random.Range(downwardForceMin, downwardForceMax));
    }

    void OnDestroy()
    {
        // foreach (GameObject mapItem in mapColliders)
        // {
        //     if (mapItem)
        //     {
        //         mapItem.SetActive(true);
        //     }
        // }
    }
}
