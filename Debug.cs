using UnityEngine;
using System;
using System.Collections;

namespace Sol
{
	public class Debug : MonoBehaviour 
	{
		public Planet Start;
		public Planet End;

		void Awake()
		{
		}

		double GetCourseTime(double Gs)
		{
			double courseTime = 0;
			if ((this.Start != null) && (this.End != null))
			{
				Vector3 start = this.Start.GetOrbitPositionUnscaled();
				Vector3 end = this.End.GetOrbitPositionUnscaled();

				double distance = (double)Vector3.Distance(start, end) * Sol.Math.MetersPerAU;

				courseTime = Sol.Math.GetLinearTravelTime(0, Sol.Math.OneG*Gs, distance);
			}
			return courseTime;
		}

		void OnGUI()
		{
			DateTime date = Sol.Time.GetDateTime();
			GUILayout.Label("Time: " + Sol.Time.GetTime());
			GUILayout.Label("Date: " + date.ToString());

			if ((this.Start != null) && (this.End != null))
			{
				GUILayout.Label(Start.Id + " -> " + End.Id + " courseTime(hours)");
				double lowG = 0.3;
				GUILayout.Label("         " + lowG +  " G:" + (GetCourseTime(lowG)/3600));

				double regularG = 1;
				GUILayout.Label("         " + regularG +  " G:" + (GetCourseTime(regularG)/3600));

				double highG = 9;
				GUILayout.Label("         " + highG +  " G:" + (GetCourseTime(highG)/3600));
			}
		}
	}
}
