using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayers : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject playerPrefab;
    public GameObject map;

    private RandomPosition randomPositionScript;
    private MapPosition mapPositionScript;

    public float[,] spawnPositions;
    public Slider[] healthSliders;

    void Awake() {
        randomPositionScript = gameObject.GetComponent<RandomPosition>();
        mapPositionScript = gameObject.GetComponent<MapPosition>();

        float[,] mapArea = mapPositionScript.GetMapPosition();
        healthSliders = (Slider[]) FindObjectsOfType(typeof (Slider));
        CreatePlayers(mapArea);
    }

    void CreatePlayers(float[,] mapArea) {
        int i = 0;

        StaticValues.players = new GameObject[StaticValues.numPlayers];
        while (i < StaticValues.numPlayers) {
            Debug.Log("Spawn player " + i);
            Vector3 spawnPositions = randomPositionScript.GetRandomPosition(
                mapArea[0, 0],
                mapArea[0, 1],
                mapArea[1, 0],
                mapArea[1, 1],
                mapArea[2, 0],
                mapArea[2, 1]
            );

            GameObject playersGameObject = GameObject.FindWithTag("Players");
            GameObject player = Instantiate(playerPrefab, spawnPositions, Quaternion.identity) as GameObject;
            player.transform.parent = playersGameObject.transform;
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.InitPlayerController(i);
            StaticValues.players[i] = player;
            player.GetComponent<PlayerHealth>().setHealthbar(healthSliders[i]);
            i++;
        }
    }
}
