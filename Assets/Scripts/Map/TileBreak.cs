using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBreak : MonoBehaviour
{
    public static event Action<TileBreak> OnBreak = delegate {};

    [Header("Timer")]
    public float removeInSeconds = 2f;
    public bool tileBreakEnabled = true;

    private Transform[] tiles;

    void Start() {
        tiles = gameObject.GetComponents<Transform>();
        if (tileBreakEnabled) {
            StartCoroutine(RemoveTiles());
        }
    }

    IEnumerator RemoveTiles()
    {
        yield return new WaitForSeconds(removeInSeconds);
        foreach (Transform item in gameObject.GetComponentInChildren<Transform>())
        {
            Destroy(item.gameObject);
            // StartCoroutine(RemoveTile(item));
        }

        OnBreak(this);
    }

    // IEnumerator RemoveTile(Transform item)
    // {
        // 1. Start animation (particle + rotation)
        // yield return new WaitForSeconds(UnityEngine.Random.Range(1, 3));
        // Anim();

        // 2. Add gravity
        // MeshCollider mc = item.gameObject.GetComponent<MeshCollider>();
        // Rigidbody rg = item.gameObject.GetComponent<Rigidbody>();
        // mc.convex = true;
        // rg.useGravity = true;

        // yield return new WaitForSeconds(2);
    //     Destroy(item.gameObject);
    // }
}
