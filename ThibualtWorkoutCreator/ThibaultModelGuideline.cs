using System;
using System.Runtime.CompilerServices;

namespace TriathlonPhysics
{
	public class ThibaultModelGuideline
	{
		public int NumSets
		{
			get;
			set;
		}

		public int RepRecovery
		{
			get;
			set;
		}

		public int SetRecovery
		{
			get;
			set;
		}

		public ThibaultModelGuideline()
		{
		}

		public ThibaultModelGuideline(int numSets, int repRecovery, int setRecovery)
		{
			this.NumSets = numSets;
			this.RepRecovery = repRecovery;
			this.SetRecovery = setRecovery;
		}
	}
}