using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Dropdown numPlayerSelectDropdown;
    public TMP_Dropdown sceneSelectDropdown;

    private int selectedNumPlayers = 2;
    private int selectedScene = 0;

    private Dictionary<int, int> playerMap = new Dictionary<int, int>() {
        { 0, 2},
        { 1, 3},
        { 2, 4}
    };

    private Dictionary<int, string> sceneMap = new Dictionary<int, string>() {
        { 0, "TestScene"},
        { 1, "Level5-Scene"},
        { 2, "Level4-scene"},
        { 3, "Level3-scene"},
        { 4, "Level1-scene"},
        { 5, "MenuScene"},
        { 6, "DemoScene"}
    };

    public void Start() {
        numPlayerSelectDropdown = GetComponentInChildren<TMP_Dropdown>();
        sceneSelectDropdown = GetComponentInChildren<TMP_Dropdown>();
    }

    public void Play() {
        SceneManager.LoadScene("DemoScene");

        // switch (selectedScene)
        // {
        //     case 0:
        //         SceneManager.LoadScene("TestScene");
        //         break;
        //     case 1:
        //         SceneManager.LoadScene("Level5-Scene");
        //         break;
        //     case 2:
        //         SceneManager.LoadScene("Level4-Scene");
        //         break;
        //     case 3:
        //         SceneManager.LoadScene("Level3-Scene");
        //         break;
        //     case 4:
        //         SceneManager.LoadScene("Level1-Scene");
        //         break;
        //     case 5:
        //         SceneManager.LoadScene("DemoScene");
        //         break;
        //     default:
        //         SceneManager.LoadScene("MenuScene");
        //         break;
        // }
    }

    public void SetNumPlayers(int playerNumIndex) {
        selectedNumPlayers = playerNumIndex;
        StaticValues.numPlayers = playerMap[playerNumIndex];
    }

    public void SetScene(int sceneIndex) {
        selectedScene = sceneIndex;
        StaticValues.scene = sceneMap[sceneIndex];
    }
}
