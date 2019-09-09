using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHurricane : MonoBehaviour
{

    public GameObject hurricanePrefab;

    [Header("Attributes")]
    public float initialWait = 10f;
    public float timeBetweenHuricanes = 10f;
    public float hurricaneDuration = 10f;

    private float[,] mapArea;
    private MapPosition mapPositionScript;
    private RandomPosition randomPositionScript;

    void Start()
    {
        mapPositionScript = gameObject.GetComponent<MapPosition>();
        randomPositionScript = gameObject.GetComponent<RandomPosition>();

        mapArea = mapPositionScript.GetMapPosition();

        StartCoroutine(startHurricane());
    }

    IEnumerator startHurricane()
    {
        yield return new WaitForSeconds(initialWait);
        while (true)
        {
            Vector3 position = randomPositionScript.GetRandomPosition(
                mapArea[0, 0],
                mapArea[0, 1],
                mapArea[1, 0],
                mapArea[1, 1],
                mapArea[2, 0],
                mapArea[2, 1]
            );
            GameObject hurricane = InstantiateHurricane(position);
            yield return new WaitForSeconds(hurricaneDuration);
            Destroy(hurricane);
            yield return new WaitForSeconds(timeBetweenHuricanes);
        }
    }

    private GameObject InstantiateHurricane(Vector3 position)
    {
        GameObject hurricanes = GameObject.FindWithTag("Hurricane");
        GameObject hurricane = Instantiate(hurricanePrefab, position, Quaternion.identity) as GameObject;
        hurricane.transform.parent = hurricanes.transform;
        return hurricane;
    }
}
