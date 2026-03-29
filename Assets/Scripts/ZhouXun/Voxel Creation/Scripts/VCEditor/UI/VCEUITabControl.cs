using UnityEngine;
using System.Collections;

public class VCEUITabControl : MonoBehaviour
{
	public UICheckbox m_Tab;
	public GameObject m_Page;
	
	// Update is called once per frame
	void Update ()
	{
		m_Page.SetActive(m_Tab.isChecked);
	}
}
