using UnityEngine;
using System.Collections;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField]
    float mValue = 1;
    //[SerializeField]
    //float mInvoteTime = 0.2f;
    //[SerializeField]
    //bool bPlayEffect = true;

#pragma warning disable CS0169
    [SerializeField]
    UISprite mSprBg; // Inspector-wired
#pragma warning restore CS0169
    [SerializeField]
    UIFilledSprite mSprBgNormal;
    [SerializeField]
    UIFilledSprite mSprBgAdd;
    [SerializeField]
    UIFilledSprite mSprBgMinus;
#pragma warning disable CS0169
    [SerializeField]
    UIHpSpecularHandler handler; // Inspector-wired
#pragma warning restore CS0169

    public float Value { get { return mValue; } set { mValue = value; } }

    public float MAXVALUE = 500f;
    public float mMaxValue;
    public float mCurValue;

    void Start()
    {
        mMaxValue = MAXVALUE;
        mCurValue = MAXVALUE;

        mSprBgNormal.fillAmount = 0;
        mSprBgAdd.fillAmount = 0;
        mSprBgMinus.fillAmount = 0;
    }

    void Update()
    {
        if (mValue > 1)
            mValue = 1;
        if (mValue < 0)
            mValue = 0;

        if (mMaxValue == MAXVALUE)//上限500
        {
            mSprBgAdd.fillAmount = 0f;
            mSprBgMinus.fillAmount = 0f;
            mSprBgNormal.fillAmount = mCurValue / mMaxValue;

        }
        else if (mMaxValue > MAXVALUE)//上限大于500
        {
            mSprBgMinus.fillAmount = 0f;
            if (mCurValue == MAXVALUE)//当前500
            {
                mSprBgAdd.fillAmount = 0f;
                mSprBgNormal.fillAmount = 1f;
            }
            else if (mCurValue < MAXVALUE)//当前小于500
            {
                mSprBgAdd.fillAmount = 0f;
                mSprBgNormal.fillAmount = mCurValue / MAXVALUE;
            }
            else if (mCurValue > MAXVALUE)//当前大于500
            {
                mSprBgNormal.fillAmount = 1f;
                mSprBgAdd.fillAmount = (mCurValue - MAXVALUE) / MAXVALUE;
            }
        }
        else if (mMaxValue < MAXVALUE)//上限小于500
        {
            mSprBgAdd.fillAmount = 0f;
            mSprBgMinus.fillAmount = (MAXVALUE - mMaxValue) / MAXVALUE;
            mSprBgNormal.fillAmount = (mCurValue / mMaxValue) * (1 - mSprBgMinus.fillAmount);
        }
    }
}
