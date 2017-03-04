using UnityEngine;
using System.Collections;

namespace Sol
{
	public class Star : Orbitable 
	{
		public Planet [] Planets;

		public override double GetScale()
		{
			return ScaleManager.Instance.StarKmToUnits;
		}
	}
}
