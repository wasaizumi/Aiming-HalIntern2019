using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Goal : PlayerState
{
    public override void OnUpdate()
    {
        // 状態遷移があればこれ以上更新しない
        if (StateTransition())
            return;

    }

    protected override bool StateTransition()
    {
        // ToDo::自動遷移の条件はここに書く

        return false;
    }



}
