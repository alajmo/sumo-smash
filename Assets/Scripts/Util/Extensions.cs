using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public static class Extensions
{
    public static List<GameObject> OnlyAlive(this List<GameObject> objects)
    {
        return objects.Where(obj =>
          obj.GetComponent<PlayerHealth>()?.isAlive ?? false).ToList();
    }
}