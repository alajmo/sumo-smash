using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorHail : MonoBehaviour
{
    public GameObject meteorPrefab;

    [Header("Hail")]
    public bool hail = true;
    public float initialWait = 10f;
    public float hailDuration = 6f;
    public float timeBetweenHail = 20f;
    public int numMeteorsPerHail = 10;
    public float intervalBetweenMeteors = 3f;

    private float[,] mapArea;
    private IEnumerator hailCoroutine;

    private MapPosition mapPositionScript;
    private RandomPosition randomPositionScript;

    void Start()
    {
        mapPositionScript = gameObject.GetComponent<MapPosition>();
        randomPositionScript = gameObject.GetComponent<RandomPosition>();

        mapArea = mapPositionScript.GetMapPosition();

        TileBreak.OnBreak += listenOnTileBreak;

        StartCoroutine(StartHail());
    }

    private IEnumerator StartHail()
    {
        yield return new WaitForSeconds(initialWait);

        while (true)
        {
            hailCoroutine = SpawnMeteorInsideRectangle();

            if (hail)
            {
                StartCoroutine(hailCoroutine);
                yield return new WaitForSeconds(hailDuration);
                StopCoroutine(hailCoroutine);
            }
            yield return new WaitForSeconds(timeBetweenHail);
        }
    }

    private IEnumerator SpawnMeteorInsideRectangle()
    {
        GameObject meteors = GameObject.FindWithTag("MeteorHail");
        while (hail)
        {
            yield return new WaitForSeconds(intervalBetweenMeteors);
            for (int j = 0; j < numMeteorsPerHail; j++)
            {
                Vector3 position = randomPositionScript.GetRandomPosition(
                    mapArea[0, 0],
                    mapArea[0, 1],
                    mapArea[1, 0],
                    mapArea[1, 1],
                    mapArea[2, 0],
                    mapArea[2, 1]
                );
                CreateHail(meteors, position);
            }
        }
    }

    private void CreateHail(GameObject meteors, Vector3 position)
    {
        GameObject meteor = Instantiate(meteorPrefab, position, Quaternion.identity) as GameObject;
        meteor.transform.parent = meteors.transform;
    }

    void listenOnTileBreak(TileBreak tileBreak)
    {
        mapArea = mapPositionScript.GetMapPosition();
    }
}
