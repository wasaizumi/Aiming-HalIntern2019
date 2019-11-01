using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SceneEditor : Editor
{
    [MenuItem("Editor/SceneEditor/Create")]
    static void Create()
    {
        var instance = CreateInstance<SceneData>();
        AssetDatabase.CreateAsset(instance, "Assets/Editor/ScritableObjects/SceneData.asset");
        AssetDatabase.Refresh();
    }
}
