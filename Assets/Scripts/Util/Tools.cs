using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public static class Tools
{
    public static List<GameObject> GetPlayers()
    {
        return new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
    }

    public static GameObject GetClosest(List<GameObject> objects, GameObject to)
    {
        return GetClosest(objects, to.transform.position);
    }

    public static GameObject GetClosest(List<GameObject> objects, Vector3 to)
    {
        GameObject closest = null;
        float distance = 99999f;
        foreach (GameObject obj in objects)
        {
            float dist = Vector3.Distance(to, obj.transform.position);
            if (dist < distance)
            {
                closest = obj;
                distance = dist;
            }
        }
        return closest;
    }
}