using UnityEngine;
using System.Collections;

namespace Sol
{
	public class Planet : Orbiter 
	{
		public Planet [] Moons;

		public override double GetOrbitalScale()
		{
			return ScaleManager.Instance.AuToUnits;
		}

		public override double GetScale()
		{
			return ScaleManager.Instance.PlanetKmToUnits;
		}
	}
}
