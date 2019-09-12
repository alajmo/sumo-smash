﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBreak : MonoBehaviour
{
    public static event Action<TileBreak> OnBreak = delegate {};

    [Header("Timer")]
    public float removeInSeconds = 2f;
    public bool tileBreakEnabled = true;
    public float torqueForce = 1f;
    public float downwardForce = 2f;

    private float speed = 1f;
    private float delta = 3f;  //delta is the difference between min y to max y.

    public GameObject[] tiles;

    void Start() {
        StartCoroutine(RemoveTiles());
    }

    IEnumerator RemoveTiles()
    {
        yield return new WaitForSeconds(removeInSeconds);
        foreach (GameObject tile in tiles) {
            StartCoroutine(RemoveTile(tile));
        }

        OnBreak(this);
    }

    IEnumerator RemoveTile(GameObject tile)
    {
        shakeTile(tile);
        yield return new WaitForSeconds(UnityEngine.Random.Range(1, 3));

        EnableParticle(tile);
        yield return new WaitForSeconds(2f);

        LoosenTile(tile);
        yield return new WaitForSeconds(5);

        Destroy(tile);
    }

    void shakeTile(GameObject tile) {
        tile.GetComponent<Shake>().enabledShake();
    }

    void EnableParticle(GameObject tile) {
        tile.GetComponent<ParticleSystem>().Play();
    }

    void LoosenTile(GameObject tile) {
        tile.GetComponent<Shake>().disableShake();
        MeshCollider mc = tile.GetComponent<MeshCollider>();
        Rigidbody rb = tile.GetComponent<Rigidbody>();
        mc.convex = true;
        rb.isKinematic = false;
        rb.useGravity = true;

        float xRotation= UnityEngine.Random.Range(1, 3);
        float yRotation= UnityEngine.Random.Range(1, 3);
        float zRotation= UnityEngine.Random.Range(1, 3);
        rb.AddTorque(new Vector3(xRotation, yRotation, zRotation) * torqueForce);
        rb.AddForce(transform.forward * downwardForce);
    }
}