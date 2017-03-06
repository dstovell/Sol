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

		public static float GetOrbitAngleAtTime(float SemiMajorAxis, float Eccentricity, float Inclination, float InitialAngle, OrbitType type, long time, long orbitTime)
		{
			float angle = 0f;

			if (type == OrbitType.Circular)
			{
				long remainder = time % orbitTime;
				float angleDelta = ((float)remainder / (float)orbitTime) * TwoPI;

				angle = InitialAngle + angleDelta;
			}

			return angle;
		}

		public static Vector3 GetOrbitPositionAtTime(float SemiMajorAxis, float Eccentricity, float Inclination, float InitialAngle, OrbitType type, Vector3 targetPosition, long time, long orbitTime, float scaler = 1f)
		{
			float angle = GetOrbitAngleAtTime(SemiMajorAxis, Eccentricity, Inclination, InitialAngle, type, time, orbitTime);

			if (type == OrbitType.Circular)
			{
				float orbitalRadius = SemiMajorAxis * scaler;

				float x = orbitalRadius * Mathf.Cos(angle);
				float z = orbitalRadius * Mathf.Sin(angle);
				return new Vector3(x, 0f, z);
			}

			return Vector3.zero;
		}

		public static List<Vector3> GetOrbitalPath(float SemiMajorAxis, float Eccentricity, float Inclination, OrbitType type, int segmentCount, float scaler = 1f)
		{
			List<Vector3> points = new List<Vector3>();

			if (type == OrbitType.Circular)
			{
				float deltaAngle = TwoPI / (float)segmentCount;
				Vector3 firstPoint = Vector3.zero;
				for (float angle=0; angle<TwoPI; angle+=deltaAngle)
				{
					float x = SemiMajorAxis * Mathf.Cos(angle) * scaler;
					float z = SemiMajorAxis * Mathf.Sin(angle) * scaler;

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
	}

}
