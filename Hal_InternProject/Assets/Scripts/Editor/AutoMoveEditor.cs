using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// インスペクターに関数実行ボタンを追加
[CustomEditor(typeof(AutoMove))]
public class AutoMoveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //元のInspector部分を表示
        base.OnInspectorGUI();

        //targetを変換して対象を取得
        AutoMove autoMove = target as AutoMove;

        if (GUILayout.Button(autoMove.TargetPointName()))
        {
            //SendMessageを使って実行
            autoMove.SendMessage("SwitchTarget", null, SendMessageOptions.DontRequireReceiver);
        }

    }
}
