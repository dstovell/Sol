using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

namespace Sol
{

	public abstract class Orbiter : Orbitable 
	{
		public Orbitable OrbiterTarget;

		public OrbitalMechanics.OrbitType Orbit = OrbitalMechanics.OrbitType.Circular;

		public float SemiMajorAxis;
		public float Eccentricity;
		public float Inclination;
		public float OrbitTime;
		public float InitialAngle;

		private VectorLine OrbitalLine;

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

			RenderOrbit();
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
			return OrbitalMechanics.GetOrbitAngleAtTime(this, time);
		}

		public Vector3 GetOrbitPositionAtTime(long time)
		{
			return OrbitalMechanics.GetOrbitPositionAtTime(this, time);
		}

		public Vector3 GetOrbitTargetPosition()
		{
			return (this.OrbiterTarget != null) ? this.OrbiterTarget.transform.position : Vector3.zero;
		}

		public Vector3 GetOrbitPosition()
		{
			return this.GetOrbitPositionAtTime( GetTime() );
		}

		public void RenderOrbit()
		{
			float lineWidth = 2.0f;
			List<Vector3> points = OrbitalMechanics.GetOrbitalPath(this, 100);
			this.OrbitalLine = new VectorLine("Orbit_"+this.Id, points, lineWidth, LineType.Continuous, Joins.Weld); 
			this.OrbitalLine.Draw3DAuto();
		}

		public abstract double GetOrbitalScale();
	}

}
