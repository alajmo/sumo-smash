using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool shakeEnabled = false;
    public float speed = 0.5f;
    public float delta = 0.1f;
    public float length = 0.5f;
    public float step = 0.008f;

    void Update()
    {
        if (shakeEnabled)
        {
            float y = Random.Range(-length, length);
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(transform.position.x, y, transform.position.z),
                step
            );
        }
    }

    public void enabledShake()
    {
        shakeEnabled = true;
    }

    public void disableShake()
    {
        shakeEnabled = false;
    }
}