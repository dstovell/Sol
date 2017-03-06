using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{

	public static class OrbitalMechanics
	{
		public enum OrbitType
		{
			Circular,
			Elliptical
		}

		public static float TwoPI = 2.0f * Mathf.PI;

		public static float GetOrbitAngleAtTime(Orbiter orbiter, long time)
		{
			float angle = 0f;
			long orbitTime = orbiter.GetOrbitTime();

			if (orbiter.Orbit == OrbitType.Circular)
			{
				long remainder = time % orbitTime;
				float angleDelta = ((float)remainder / (float)orbitTime) * TwoPI;

				angle = orbiter.InitialAngle + angleDelta;
			}

			return angle;
		}

		public static Vector3 GetOrbitPositionAtTime(Orbiter orbiter, long time)
		{
			float angle = GetOrbitAngleAtTime(orbiter, time);

			if (orbiter.Orbit == OrbitType.Circular)
			{
				float orbitalRadius = orbiter.SemiMajorAxis * (float)orbiter.GetOrbitalScale();

				float x = orbitalRadius * Mathf.Cos(angle);
				float z = orbitalRadius * Mathf.Sin(angle);
				return new Vector3(x, 0f, z);
			}

			return Vector3.zero;
		}

		public static List<Vector3> GetOrbitalPath(Orbiter orbiter, int segmentCount)
		{
			List<Vector3> points = new List<Vector3>();

			if (orbiter.Orbit == OrbitType.Circular)
			{
				float deltaAngle = TwoPI / (float)segmentCount;
				Vector3 firstPoint = Vector3.zero;
				float scaler = (float)orbiter.GetOrbitalScale();
				for (float angle=0; angle<TwoPI; angle+=deltaAngle)
				{
					float x = orbiter.SemiMajorAxis * Mathf.Cos(angle) * scaler;
					float z = orbiter.SemiMajorAxis * Mathf.Sin(angle) * scaler;

					Vector3 point = new Vector3(x, 0.0f, z);
					points.Add(point);

					if (angle == 0)
					{
						firstPoint = point;
					}
				}

				if (firstPoint != Vector3.zero)
				{
					points.Add(firstPoint);
				}
			}

			return points;
		}

		public static float GetRotateAngleAtTime(Orbiter orbiter, long time)
		{
			long rotateTime = orbiter.GetRotateTime();
			if (rotateTime == 0)
			{
				return 0f;
			}

			long remainder = time % rotateTime;
			float angle = ((float)remainder / (float)rotateTime) * TwoPI;
			return angle;
		}
	}

}
