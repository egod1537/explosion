using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameJamLibrary
{

#if UNITY_EDITOR

    public class EditorLibrary
    {

        public static void DrawLine(Color color)
        {

            EditorGUILayout.Space();

            Texture2D tex = new Texture2D(1, 1);
            float y = GUILayoutUtility.GetLastRect().yMax;

            GUI.color = color;

            GUI.DrawTexture(new Rect(0.0f, y, Screen.width, 3.0f), tex);
            tex.hideFlags = HideFlags.DontSave;

            GUI.color = Color.white;

            EditorGUILayout.Space();
        }

        public static void DrawEmptySpace(float _height)
        {

            EditorGUILayout.LabelField("", GUILayout.Height(_height));

        }

    }

#endif

}
