using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticValues : MonoBehaviour
{
    static public int numPlayers = 2;
    static public string scene = "MenuScene";
    static public GameObject[] players;

    void Update() {
        // Debug.Log(numPlayers);
    }
}
