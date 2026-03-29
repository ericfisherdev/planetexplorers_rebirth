using UnityEngine;
using System.Collections;
using Pathea;

public class ServantShowItem_N : MonoBehaviour 
{
	public UITexture	mHead;
	public UISprite		mDeadSpr;
	
	public UISlider		mLife;
	public UISlider		mComfort;
	public UISlider		mOxygen;
	
	public UISprite		mState;

    PeEntity mNpc;
    public PeEntity NPC { get { return mNpc; } }
	//bool 				mIsDead;
	
	public void SetNpc(PeEntity npc)
	{
        mNpc = npc;
        if (mNpc)
        {
//            EntityInfoCmpt entityInfo = npc.GetCmpt<EntityInfoCmpt>();
            
            //mIsDead = false;
            mHead.mainTexture = npc.GetCmpt<EntityInfoCmpt>().faceTex;
            mHead.enabled = true;
            mDeadSpr.enabled = false;
            //mLife.sliderValue = npc.lifePercent;
            //mComfort.sliderValue = npc.comfortPercent;
            //mOxygen.sliderValue = npc.oxygenPercent;
            mState.enabled = true;
        }
        else
        {
            mHead.mainTexture = null;
            mHead.enabled = false;
            mDeadSpr.enabled = false;
            mLife.sliderValue = 0;
            mComfort.sliderValue = 0;
            mOxygen.sliderValue = 0;
            mState.enabled = false;
        }
	}
	
	void OnServantStateBtn()
	{
        //if(null != mNpc && Input.GetMouseButtonUp(0))
        //    MainLeftGui_N.Instance.OnServantStateBtn(this);
	}
	
	void OnServanteHead()
	{
        //if(null != mNpc && Input.GetMouseButtonUp(0))
        //    MainLeftGui_N.Instance.OnServantHead(this);
	}
}
