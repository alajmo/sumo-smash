using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {
    [Header("Game Objects")]
    public GameObject foodPrefab;

    private MapPosition mapPositionScript;
    private RandomPosition randomPositionScript;

    private float[,] mapArea;

    [Header("Attributes")]
    public int numFood = 3;
    public float respawnTime = 5f;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 4f;

    void Start() {
        mapPositionScript = gameObject.GetComponent<MapPosition>();
        randomPositionScript = gameObject.GetComponent<RandomPosition>();

        mapArea = mapPositionScript.GetMapPosition();

        InitFoodSpawn();

        TileBreak.OnBreak += listenOnTileBreak;
        Food.OnRemove += listenOnFoodDestroy;
    }

    void OnDestroy() {
        Food.OnRemove -= listenOnFoodDestroy;
        TileBreak.OnBreak -= listenOnTileBreak;
    }

    void InitFoodSpawn() {
        for (int i = 0; i < numFood; i++) {
            StartCoroutine(CreateFood());
        }
    }

    private IEnumerator CreateFood() {
        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

        Vector3 position = randomPositionScript.GetRandomPosition(
            mapArea[0, 0],
            mapArea[0, 1],
            mapArea[1, 0],
            mapArea[1, 1],
            mapArea[2, 0],
            mapArea[2, 1]
        );

        InstantiateFood(position);
    }

    private void InstantiateFood(Vector3 position) {
        GameObject foodGameObject = GameObject.FindWithTag("FoodSpawnArea");
        GameObject food = Instantiate(foodPrefab, position, Quaternion.identity) as GameObject;
        food.transform.parent = foodGameObject.transform;
    }

    void listenOnFoodDestroy(Food food) {
        StartCoroutine(CreateFood());
    }

    void listenOnTileBreak(TileBreak tileBreak) {
        mapArea = mapPositionScript.GetMapPosition();
    }
}
