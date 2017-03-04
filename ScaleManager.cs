using UnityEngine;
using System.Collections;

namespace Sol
{

	public class ScaleManager : MonoBehaviour
	{
		public static ScaleManager Instance = null;

		public Scale [] Scales;
		public int CurrentScaleIndex;

		public Scale CurrentScale			{ get{ return this.Scales[this.CurrentScaleIndex]; } }

		public double AuToUnits				{ get{ return this.CurrentScale.AuToUnits; } }

		public double StarKmToUnits			{ get{ return this.CurrentScale.StarKmToUnits; } }
		public double PlanetKmToUnits		{ get{ return this.CurrentScale.PlanetKmToUnits; } }
		public double MoonKmToUnits			{ get{ return this.CurrentScale.MoonKmToUnits; } }

		public double PlanetOrbitKmToUnits	{ get{ return this.CurrentScale.PlanetOrbitKmToUnits; } }
		public double MoonOrbitKmToUnits	{ get{ return this.CurrentScale.MoonOrbitKmToUnits; } }

		void Awake()
		{
			ScaleManager.Instance = this;

			this.CurrentScaleIndex = 0;
		}
	}

}

