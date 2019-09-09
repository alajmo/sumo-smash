using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rotator : MonoBehaviour
{
    public float speed = 8.0f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(new Vector3 (15, 30, 45) * Time.deltaTime);
    }
}
