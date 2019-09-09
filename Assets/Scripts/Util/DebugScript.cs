using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugScript : MonoBehaviour
{
    public void Reset()
    {
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
