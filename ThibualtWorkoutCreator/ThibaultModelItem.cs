using System;
using System.Runtime.CompilerServices;

namespace TriathlonPhysics
{
	public class ThibaultModelItem
	{
		public int Duration
		{
			get;
			set;
		}

		public int MaxAerobicPower
		{
			get;
			set;
		}

		public int TotalReps
		{
			get;
			set;
		}

		public ThibaultModelItem()
		{
		}

		public ThibaultModelItem(int map, int reps, int duration)
		{
			this.TotalReps = reps;
			this.Duration = duration;
			this.MaxAerobicPower = map;
		}
	}
}