using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public enum MapType
{
    Square
};


public class Map : MonoBehaviour
{
    public int size = 20;
    
    public MapType type;

    public NamedPrefab[] tilePrefabs;
    public GameObject[] playerPrefabs;

    public Dictionary<string, GameObject> prefabs;

    protected void Awake()
    {
        this.prefabs = new Dictionary<string, GameObject>();
        foreach (var item in this.tilePrefabs)
        {
            this.prefabs.Add(item.name, item.prefab);
        }
    }

    private void Start()
    {
        Generate();
    }

    void Generate()
    {
        switch (this.type)
        {
            case MapType.Square:
                var gen = new SquareMap(this);
                gen.Generate();
                break;
        }
    }
}

[Serializable]
public struct NamedPrefab
{
    public string name;
    public GameObject prefab;
}

public struct SquareMap
{
    Map map;

    public SquareMap(Map map)
    {
        this.map = map;
    }

    public void Generate()
    {
        for (int x = 0; x < map.size; x++)
        {
            for (int y = 0; y < map.size; y++)
            {
                map.StartCoroutine(AddGrass(x, y));
            }
        }

        int playerCount = map.playerPrefabs.Length;
        float angleDelta = 2f * 3.1415f / (float)playerCount;
        for (int i = 0; i < playerCount; i++)
        {
            var position = new Vector3(map.size * 0.3f * Mathf.Cos(angleDelta * i), 1, map.size * 0.3f * Mathf.Sin(angleDelta * i));
            AddPlayer(i, position);
        }
    }

    public void AddPlayer(int id, Vector3 position)
    {
        var prefab = this.map.playerPrefabs[id];
        GameObject player = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        player.transform.parent = map.transform;
        player.transform.localPosition = position;
        player.GetComponent<PlayerController>().InitPlayerController(id);
    }

    public IEnumerator AddGrass(int x, int y)
    {
        var position = new Vector3(x - map.size / 2f, 0, y - map.size / 2f);
        var distanceToCenter = Vector3.Distance(position, Vector3.zero);
        var grass = map.prefabs["grass"];

        GameObject tile = GameObject.Instantiate(grass, Vector3.zero, Quaternion.identity);
        tile.transform.parent = map.transform;
        tile.transform.localPosition = position;

        var delay = TileBreakDelay(position);
        if (delay > 0) {
            yield return new WaitForSeconds(delay);
            yield return map.gameObject.GetComponent<TileBreak>().RemoveTile(tile);
        }
    }

    public float TileBreakDelay(Vector3 position) {
        var distanceToCenter = Vector3.Distance(position, Vector3.zero);
        
        var px = Mathf.Abs(position.x);
        var py = Mathf.Abs(position.z);

        // Final shape
        if (distanceToCenter < map.size * 0.15f) {
            return 0;
        }

        // Part 3
        if (px < map.size * 0.4f && py < map.size * 0.4f && (px < 2f || py < 2f)) {
            return 50;
        }

        // Part 2
        if (px < 3f || py < 3f) {
            return 30;
        }

        // Part 1
        return 20 - distanceToCenter;
    }
}