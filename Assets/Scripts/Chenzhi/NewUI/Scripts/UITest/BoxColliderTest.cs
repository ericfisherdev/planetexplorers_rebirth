using UnityEngine;
using System.Collections;

public class BoxColliderTest : MonoBehaviour
{
#pragma warning disable CS0169 // Inspector-wired via [SerializeField]
	[SerializeField] BoxCollider mBgCollider;
	[SerializeField] BoxCollider mTopCollider;
#pragma warning restore CS0169
	//bool isCover = false;
	//Bounds bounds;

//	Rect rect
//	{
//		get
//		{
//			float left = transform.position.x - transform.localScale.x/2;
//			float top = transform.position.y + transform.localScale.y/2;
//			float right = left +  transform.localScale.x;
//			float buttom = top -  transform.localScale.y;
//			return new Rect(left,top,right,height);
//		}
//	}
}
