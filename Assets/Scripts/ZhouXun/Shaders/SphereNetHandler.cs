using UnityEngine;
using System.Collections;

public class SphereNetHandler : MonoBehaviour
{
	private float m_TimeFactor = 0;
	// Update is called once per frame
	void Update ()
	{
		m_TimeFactor += Time.deltaTime;
		GetComponent<Renderer>().material.SetFloat("_TimeFactor", m_TimeFactor);
	}
}
