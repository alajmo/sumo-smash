using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPosition : MonoBehaviour {
    [Header("Positioning")]
    public float minY = 1f;
    public float maxY = 10f;

    public float xPadding = 2;
    public float yPadding = 2;
    public float zPadding = 2;

    public float[,] GetMapPosition() {
        return GetBoundingBox();
    }

    private Vector3 GetCenterPoint() {
        return new Vector3(0, 0, 0);
    }

    private float[,] GetBoundingBox() {
        GameObject [] mapObjects = GameObject.FindGameObjectsWithTag("Map");
        float minX = 9999;
        float maxX = 0;

        float minZ = 9999;
        float maxZ = 0;

        foreach (GameObject item in mapObjects) {
            Bounds bounds = item.GetComponent<Renderer>().bounds;

            float x = item.transform.position.x;
            float z = item.transform.position.z;

            if (bounds.min.x < minX ) {
                minX = bounds.min.x;
            }

            if (bounds.max.x > maxX ) {
                maxX = bounds.max.x;
            }

            if (bounds.min.z < minZ ) {
                minZ = bounds.min.z;
            }

            if (bounds.max.z > maxZ ) {
                maxZ = bounds.max.z;
            }
        }

        // Debug.Log("");
        // Debug.Log("minX: " + minX);
        // Debug.Log("maxX: " + maxX);
        // Debug.Log("minZ: " + minZ);
        // Debug.Log("maxZ: " + maxZ);

        return new float[,] {
            { minX + xPadding, maxX - xPadding },
            { minY + yPadding, maxY - yPadding },
            { minZ + zPadding, maxZ - zPadding },
        };
    }
}