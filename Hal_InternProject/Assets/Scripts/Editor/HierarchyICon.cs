using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

public class HierarchyICon
{
    private const int WIDTH = 16;

    private static readonly MethodInfo mGetIconForObject = typeof(EditorGUIUtility)
        .GetMethod("GetIconForObject", BindingFlags.NonPublic | BindingFlags.Static);

    [InitializeOnLoadMethod]
    private static void Example()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI(int instanceID, Rect selectionRect)
    {
        var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (go == null)
        {
            return;
        }

        var parameters = new object[] { go };
        var icon = mGetIconForObject.Invoke(null, parameters) as Texture;

        if (icon == null)
        {
            return;
        }

        var pos = selectionRect;
        pos.x = pos.xMax - WIDTH;
        pos.width = WIDTH;

        GUI.DrawTexture(pos, icon, ScaleMode.ScaleToFit, true);
    }
}
