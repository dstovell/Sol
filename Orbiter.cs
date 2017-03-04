using UnityEngine;
using System;
using System.Collections;

namespace Sol
{

	public abstract class Orbiter : Orbitable 
	{
		public GameObject OrbiterTarget;

		public float SemiMajorAxis;
		public float Eccentricity;
		public float Inclination;
		public float OrbitTime;
		public float InitialAngle;

		void Start() 
		{
			//DateTime.Now.DateToUnix();
			if (this.OrbiterTarget != null)
			{
				if (this.transform.parent != this.OrbiterTarget.transform)
				{
					this.transform.SetParent(this.OrbiterTarget.transform);
				}
			}
		}
		
		protected void Update() 
		{
			base.Update();
			this.transform.localPosition = this.GetOrbitPosition();
		}

		public long GetOrbitTime()
		{
			return ((long)this.OrbitTime * 24 * 60 * 60);
		}

		public float GetOrbitAngleAtTime(long time)
		{
			long orbitTime = GetOrbitTime();
			long remainder = time % orbitTime;
			float angleDelta = ((float)remainder / (float)orbitTime) * 2.0f * Mathf.PI;

			return this.InitialAngle + angleDelta;
		}

		public Vector3 GetOrbitPositionAtTime(long time)
		{
			float orbitalRadius = this.SemiMajorAxis * (float)this.GetOrbitalScale();
			float angle = this.GetOrbitAngleAtTime(time);

			float x = orbitalRadius * Mathf.Cos(angle);
			float z = orbitalRadius * Mathf.Sin(angle);
			return new Vector3(x, 0f, z);
		}

		public Vector3 GetOrbitPosition()
		{
			return this.GetOrbitPositionAtTime( GetTime() );
		}

		public abstract double GetOrbitalScale();
	}

}
