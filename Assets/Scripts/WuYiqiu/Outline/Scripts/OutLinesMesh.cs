using UnityEngine;
using System.Collections;

public class OutLinesMesh : MonoBehaviour 
{
	public Material mat;
	
	// Update is called once per frame
	void Update () 
	{
	
		MeshFilter mf = gameObject.GetComponent<MeshFilter>();
		if (mf == null)
			return;

		Graphics.DrawMesh(mf.mesh, transform.position, transform.rotation, mat, 0);
	}
	
}
