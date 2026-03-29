using UnityEngine;
using System.Collections;

public class CSPersonnelObject : MonoBehaviour 
{
	public CSPersonnel 	m_Personnel;

	void OnDestroy()
	{
		//CSBehaveMgr.ClearBehaves(m_Personnel.ID);
	}
}

