using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{

	public static class Math
	{
		public enum OrbitType
		{
			Circular,
			Elliptical
		}

		public static float TwoPI = 2.0f * Mathf.PI;
		public static double OneG = 9.8f;

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

		public static float GetRotateAngleAtTime(Orbitable orbiter, long time)
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

		public static double AccelerationToGs(double acceleration)
		{
			return (acceleration / OneG);
		}

		public static double GsToAcceleration(double gs)
		{
			return (gs * OneG);
		}

		public static double GetLinearTravelTime(double velocityInitial, double acceleration, double distance )
		{
			double firstPart = (-1 * velocityInitial);
			double secondPart = System.Math.Sqrt(velocityInitial*velocityInitial + 2*acceleration*distance) / acceleration;

			double solutionA = firstPart + secondPart;
			double solutionB = firstPart - secondPart;

			return (solutionA > solutionB) ? solutionA : solutionB;
		}

		public static double GetLinearCourseTime(double velocityInitialFinal, double acceleration, double distance )
		{
			double halfDistance = distance / 2;

			double accelerationTime = GetLinearTravelTime(velocityInitialFinal, acceleration, halfDistance);
			double decelerationTime = accelerationTime;

			return (acceleration + decelerationTime);
		}

		//public static double GetLinearCourseTime(double velocityInitial, double velocityFinal, double acceleration, double distance )
		//{
		//}
	}

}
