using UnityEngine;
using System;

public class UINpcQuestItem : MonoBehaviour
{
    [SerializeField] UILabel textLabel;
#pragma warning disable CS0169 // titleIcon is wired via Unity Inspector
    [SerializeField] UISprite titleIcon;
#pragma warning restore CS0169

    public int index;

    public Action<UINpcQuestItem> onClick;

    public string test
    {
        get
        {
            return textLabel.text;
        }

        set
        {
            textLabel.text = value;
        }
    }

    void OnBtnClick ()
    {
        if (onClick != null)
            onClick(this);
    }
}
