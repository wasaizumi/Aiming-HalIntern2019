using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectInit : StageSelectSceneState
{
    [SerializeField]
    private RectTransform m_ColumParent;
    [SerializeField]
    private RectTransform m_cursor;

    [SerializeField]
    private GameObject m_singlePlayBG;
    [SerializeField]
    private GameObject m_multiPlayBG;

    [SerializeField]
    private float topOffset = 1.0f;

    public override void OnStart()
    {
        if (m_scene.m_systemData.PlayMode == PlayMode.SinglePlay)
            m_singlePlayBG.SetActive(true);
        else
            m_multiPlayBG.SetActive(true);

        SetStageSelectColum();
        StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
        while (FadeController.IsActive)
            yield return null;

        m_scene.ChangeState<StageSelectSceneIdle>();
        yield break;
    }

    //画面を構成
    private void SetStageSelectColum()
    {
        int stageNum = m_scene.m_systemData.StageMenu.m_stageList.Count;
        List<GameObject> stageColums = new List<GameObject>();

        //StageSelect Colum
        GameObject prefab = Resources.Load("Prefab/Scene/StageSelect/StageSelectUI/StageColum") as GameObject;
        Rect PrefabRect = prefab.GetComponent<RectTransform>().rect;

        Vector3 offset = m_ColumParent.localPosition + new Vector3(0.0f,PrefabRect.height-topOffset,0.0f);
        //配置
        for (int count = 0; count < stageNum; count++)
        {
            GameObject instance = Instantiate<GameObject>(prefab);
            
            instance.transform.SetParent(m_ColumParent, true);
            instance.transform.localScale = Vector3.one;
            RectTransform rectTransform = instance.GetComponent<RectTransform>();
            rectTransform.localPosition = offset + new Vector3(0.0f, -PrefabRect.height * count , 0.0f);
            instance.GetComponent<StageColumUI>().SetStageSelectData(m_scene.m_systemData.StageMenu.m_stageList[count]);
            stageColums.Add(instance);
        }

        Vector3 pos = m_ColumParent.localPosition;
        pos.y = PrefabRect.height * m_scene.m_systemData.StageSelectNum;
        m_ColumParent.localPosition = pos;

        m_cursor.position = stageColums[m_scene.m_systemData.StageSelectNum].transform.position;

        //ステージアンロック
        m_scene.m_systemData.StageMenu.m_stageList[0].UnLock();
    }
}
