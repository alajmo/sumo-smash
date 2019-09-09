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
            StartCoroutine(RemoveTile());
        }
    }

    IEnumerator RemoveTile()
    {
        yield return new WaitForSeconds(removeInSeconds);

        foreach (Transform item in tiles) {
            Destroy(item.gameObject);
        }
        OnBreak(this);
    }
}
