using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Pathea.Operate
{

	public class PESeat : Operation_Multiple 
	{

		public PESit[] sits;
		
		public override List<Operation_Single> GetSingles()
		{
			return (sits == null || sits.Length == 0) ? new List<Operation_Single>() : new List<Operation_Single>(sits);
		}
	}
}
