using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour {
    public Vector3 GetRandomPosition(
        float minX, float maxX,
        float minY, float maxY,
        float minZ, float maxZ
        ) {

        return new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            Random.Range(minZ, maxZ)
        );
    }
}
