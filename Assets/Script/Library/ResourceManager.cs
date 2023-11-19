using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamLibrary
{

    public class ResourceManager : MonoBehaviour
    {

        public static Dictionary<string, GameObject> Cashing = new Dictionary<string, GameObject>();

        public static GameObject Load(string path)
        {

            GameObject go;

            if (!Cashing.TryGetValue(path, out go))
            {

                go = Resources.Load(path) as GameObject;
                Cashing.Add(path, go);

            }

            return go;

        }

    }

}
