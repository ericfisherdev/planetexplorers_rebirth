using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pathea.Operate
{
	public class PETrainner : Operation_Multiple 
	{
		public PEPractice[] Instructors;
		
		public override List<Operation_Single> GetSingles()
		{
			return new List<Operation_Single>(Instructors);
		}

	}
}
