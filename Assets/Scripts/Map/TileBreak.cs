﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBreak : MonoBehaviour
{
    public static event Action<TileBreak> OnBreak = delegate {};

    public GameObject dirtParticlePrefab;

    [Header("Timer")]
    public float removeInSeconds = 2f;
    public bool tileBreakEnabled = true;

    public float torqueForceMin = 0.5f;
    public float torqueForceMax = 1f;
    public float downwardForceMin = 1f;
    public float downwardForceMax = 3f;

    private float speed = 1f;
    private float delta = 3f;  //delta is the difference between min y to max y.

    public GameObject[] tiles;

    Renderer rend;
    Color tileBreakColor;
    public Color normalTileColor = new Color32(185, 185, 185, 255);
    public Color darkerTileColor = new Color32(87, 150, 109, 255);
    List<Renderer> renderers = new List<Renderer>();


    void Start() {
        StartCoroutine(RemoveTiles());
    }

    void Update() {
        foreach(Renderer rends in renderers) {
            // make the breaking tiles darker in color, -2 in offset because it already starts too dark, 0.3f is for making it slower in transiton
            tileBreakColor = Color.Lerp(normalTileColor, darkerTileColor, Mathf.Min((Time.time - 2f) * 0.3f, 1));
            rends.material.SetColor("_Color", tileBreakColor);
        }
    }

    IEnumerator RemoveTiles()
    {
        yield return new WaitForSeconds(removeInSeconds);
        foreach (GameObject tile in tiles) {
            StartCoroutine(RemoveTile(tile));
            rend = tile.GetComponent<Renderer>();
            renderers.Add(rend);
        }
        OnBreak(this);
    }

    public IEnumerator RemoveTile(GameObject tile)
    {
        StartDirtParticles(tile);
        tile.AddComponent<Shake>();
        yield return new WaitForSeconds(6);

        Destroy(tile.GetComponent<Shake>());
        LoosenTile(tile);
        yield return new WaitForSeconds(10);

        Destroy(tile);
    }

    void StartDirtParticles(GameObject tile) {
        GameObject particles = Instantiate(dirtParticlePrefab, Vector3.zero, Quaternion.identity);

        particles.transform.parent = tile.transform;
        particles.transform.localPosition = new Vector3(0f, 0.5f, 0f);
        particles.GetComponent<ParticleSystem>().Play();
    }

    void LoosenTile(GameObject tile) {
        // Make tile slightly smaller so it can fall easier
        tile.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        MeshCollider mc = tile.GetComponent<MeshCollider>();
        Rigidbody rb = tile.GetComponent<Rigidbody>();
        mc.convex = true;
        rb.isKinematic = false;
        rb.useGravity = true;

        AddTileForce(rb);
    }

    void AddTileForce(Rigidbody rb) {
        float xRotation= UnityEngine.Random.Range(1, 3);
        float yRotation= UnityEngine.Random.Range(1, 3);
        float zRotation= UnityEngine.Random.Range(1, 3);
        rb.AddTorque(new Vector3(xRotation, yRotation, zRotation) * UnityEngine.Random.Range(torqueForceMin, torqueForceMin));
        rb.AddForce(transform.forward * UnityEngine.Random.Range(downwardForceMin, downwardForceMax));
    }
}