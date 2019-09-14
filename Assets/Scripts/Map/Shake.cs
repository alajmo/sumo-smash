using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool shakeEnabled = false;
    public float speed = 5f;
    public float length = 0.05f;

    private Vector3 startingPosition;
    private float offset;

    void Update()
    {
        if (shakeEnabled)
        {
            Vector3 target = transform.position;
            target.y = startingPosition.y + length * Mathf.Sin(Time.time * speed + this.offset);
            transform.position = target;
        }
    }

    public void enabledShake()
    {
        shakeEnabled = true;
        this.offset = Random.Range(0, 3.141f);
        this.startingPosition = transform.position;
    }

    public void disableShake()
    {
        shakeEnabled = false;
    }
}