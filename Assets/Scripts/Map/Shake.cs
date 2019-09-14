using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float speed = 5f;
    public float length = 0.05f;

    private Vector3 startingPosition;
    private float offset;

    void Update()
    {
        Vector3 target = transform.localPosition;
        target.y = startingPosition.y + length * Mathf.Sin(Time.time * speed + this.offset);
        transform.localPosition = target;
    }

    public void Start()
    {
        this.offset = Random.Range(0, 3.141f);
        this.startingPosition = transform.localPosition;
    }
}