﻿using UnityEngine;
using System;
using System.Collections;

namespace Sol
{

	public abstract class Orbitable : MonoBehaviour
	{
		public string Id;

		public GameObject OrbitableBody;

		public double Mass;
		public double Radius;
		public double RotationTime;

		protected void Update() 
		{
			if (this.OrbitableBody != null)
			{
				float scale = (float)(this.Radius * this.GetScale());
				this.OrbitableBody.transform.localScale = new Vector3(scale, scale, scale);
			}

			this.transform.localRotation = Quaternion.AngleAxis(this.GetRotateAngle(), this.transform.up);
		}

		public long GetRotateTime()
		{
			return ((long)this.RotationTime * 24 * 60 * 60);
		}

		public float GetRotateAngleAtTime(long time)
		{
			return Sol.Math.GetRotateAngleAtTime(this, time);
		}

		public float GetRotateAngle()
		{
			return this.GetRotateAngleAtTime( this.GetTime() );
		}

		public long GetTime()
		{
			return Sol.Time.GetTime();
		}

		public abstract double GetScale();
	}

}
