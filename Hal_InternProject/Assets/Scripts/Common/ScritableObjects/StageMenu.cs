using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/StageMenu" ,fileName = "StageMenu")]
public class StageMenu : ScriptableObject
{
    public List<GameData> m_stageList;

    [ContextMenu("StageReset")]
    public void Reset()
    {
        foreach (var obj in m_stageList)
            obj.ResetState();
    }

    [ContextMenu("AllUnLock")]
    public void AllUnLock()
    {
        foreach (var obj in m_stageList)
            obj.UnLock();
    }
}
