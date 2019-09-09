using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTilt : MonoBehaviour {
    public GameObject map;

    public bool mapTiltEnabled = false;
    public float timeUntilStart = 30f;
    public float angleX = 15f;
    public float angleY = 5f;
    public float angleZ = 15f;
    public float period = 2f;

    private float time = 0f;

    void Awake() {
        StartCoroutine(BeginMapTilt());
    }

    void Update() {
        if (mapTiltEnabled) {
            time = time + Time.deltaTime;
            float phase = Mathf.Sin(time / period);
            map.transform.localRotation = Quaternion.Euler(
                new Vector3(phase * angleX, phase * angleY, phase * angleZ)
            );
        }
    }

    IEnumerator BeginMapTilt() {
        yield return new WaitForSeconds(timeUntilStart);
        mapTiltEnabled = true;
    }
}
