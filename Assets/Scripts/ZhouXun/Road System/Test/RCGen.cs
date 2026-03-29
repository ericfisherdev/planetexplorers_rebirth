using UnityEngine;
using System.Collections;

public class RCGen : MonoBehaviour
{
	public int type = 1;
	// Update is called once per frame
	void Update ()
	{
		RoadSystem.DataSource.AlterRoadCell(transform.position, new RoadCell(type));
	}
}
