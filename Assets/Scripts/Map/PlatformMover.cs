using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    private Vector3 startPosition;

    public float speed = 5f;
    public float xDirection = 4f;
    public float yDirection = 0f;
    public float zDirection = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float xPosition = startPosition.x;
        float yPosition = startPosition.y;
        float zPosition = startPosition.z;

        if (xDirection > 0)
        {
            xPosition = Mathf.PingPong(Time.time * speed, Mathf.Abs(xDirection)) + startPosition.x;
        } else if (xDirection < 0)
        {
            xPosition = -Mathf.PingPong(Time.time * speed, Mathf.Abs(xDirection)) + startPosition.x;
        }

        if (yDirection > 0)
        {
            yPosition = Mathf.PingPong(Time.time * speed, Mathf.Abs(yDirection)) + startPosition.y;
        } else if (yDirection < 0)
        {
            yPosition = -Mathf.PingPong(Time.time * speed, Mathf.Abs(yDirection)) + startPosition.y;
        }

        if (zDirection > 0)
        {
            zPosition = Mathf.PingPong(Time.time * speed, Mathf.Abs(zDirection)) + startPosition.z;
        } else if (zDirection < 0)
        {
            zPosition = -Mathf.PingPong(Time.time * speed, Mathf.Abs(zDirection)) + startPosition.z;
        }

        transform.position = new Vector3(
            xPosition,
            yPosition,
            zPosition
        );
    }
}
