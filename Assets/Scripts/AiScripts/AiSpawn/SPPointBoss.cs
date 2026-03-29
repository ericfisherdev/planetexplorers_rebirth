using UnityEngine;
using System.Collections;

public class SPPointBoss : SPPoint 
{
    new public void OnDestroy()
    {
        base.OnDestroy();

//        if(GameGui_N.Instance != null)
//			GameUI.Instance.mLimitWorldMapGui.RemoveBoss(this);
    }
}
