using UnityEngine;
using System.Collections;

public class UITalkItem : MonoBehaviour
{

	public UILabel mText;

	public void SetText(string strtext)
	{
		if(mText == null)
			return;
		mText.text = strtext;
	}
}
