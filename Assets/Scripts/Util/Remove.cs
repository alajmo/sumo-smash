using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove : MonoBehaviour
{
    public float respawnTime = 10f;
    public float respawnTimeDeviation = 5f;
    public bool respawnTimeRandom = true;
    public bool respawn = true;

    [Header("Scripts")]
    public MapPosition mapPosition;
    public RandomPosition randomPosition;

    void Awake() {
        mapPosition = GetComponent<MapPosition>();
        randomPosition = GetComponent<RandomPosition>();
        // float [,] lala = mapPosition.GetMapPosition();
        // Debug.Log(lala[0, 1]);
    }

    public void remove()
    {
        BoxCollider[] meshColliders = GetComponents<BoxCollider>();
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        foreach(BoxCollider meshCollider in meshColliders) {
            meshCollider.enabled = false;
        }

        if (respawn) {
            StartCoroutine(TemporarilyDisablePlatform(meshColliders));
        }
    }

    IEnumerator TemporarilyDisablePlatform(BoxCollider[] meshColliders)
    {
        float respawnIn = respawnTimeRandom ? Random.Range(respawnTime - respawnTimeDeviation, respawnTime + respawnTimeDeviation) : respawnTime;
        yield return new WaitForSeconds(respawnIn);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        float [,] mapDimension = mapPosition.GetMapPosition();
        Vector3 position = randomPosition.GetRandomPosition(
            mapDimension[0, 0],
            mapDimension[0, 1],
            mapDimension[1, 0],
            mapDimension[1, 1],
            mapDimension[2, 0],
            mapDimension[2, 1]
        );

        gameObject.transform.position = position;
        foreach(BoxCollider meshCollider in meshColliders) {
            meshCollider.enabled = true;
        }
    }
}
