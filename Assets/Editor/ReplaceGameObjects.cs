using UnityEngine;
using UnityEditor;

/// <summary>Performs manual iteration to swap out one game object for another.</summary>
public class ReplaceGameObjects: EditorWindow
{
    /// <summary>The new object to instantiate in place of the old object.</summary>
    public GameObject newPrefab;
    /// <summary>The old objects, intended to be swapped out for iterations of
    /// the new object.</summary>
    public GameObject[] oldGameObjects;
    /// <summary>The string tag to use when replacing objects by tag.</summary>
    public string searchByTag;

    [MenuItem("Window/ReplaceGameObjects")]
    public static void ShowWindow () {
        EditorWindow.GetWindow<ReplaceGameObjects>("Replace Game Objects");
    }

    void OnGUI()
    {
    }
}