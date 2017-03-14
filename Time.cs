using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{

	public static class Time
	{
		private static long StartTime = 1488778227;
		private static long SecondsPerRealSecond = 10;

		static void SetupTime(long startTime, int secondsPerRealSecond)
		{
			Time.StartTime = startTime;
			Time.SecondsPerRealSecond = secondsPerRealSecond;
		}

		static public long GetTime()
		{
			return GetTimeAt(DateTime.Now.DateToUnix());
		}

		static public DateTime GetDateTime()
		{
			long time = GetTime();
			DateTime d = KGFUtility.DateFromUnix(time);
			return d;
		}

		static public long GetTimeAt(long time)
		{
			long realTimeSinceStart = time - StartTime;
			long scaledTimeSinceStart = realTimeSinceStart * SecondsPerRealSecond;
			//Debug.Log("GetTimeAt time=" + time + " StartTime=" + StartTime + " realTimeSinceStart=" + realTimeSinceStart + " scaledTimeSinceStart=" + scaledTimeSinceStart + " final=" + (scaledTimeSinceStart + StartTime));
			return scaledTimeSinceStart + StartTime;
		}
	}

}

