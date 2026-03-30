using UnityEngine;
using System.Collections;

public class GlobalMatMgr : MonoBehaviour
{
	public Material EnergySheildMat;
	
	// Update is called once per frame
	void Update ()
	{
		if ( EnergySheildMat != null )
		{
			EnergySheildMat.SetFloat("_TimeFactor", Time.time);
		}
	}
}
