using System;
using System.Runtime.CompilerServices;


namespace TriathlonPhysics
{
	public class IntervalWorkoutConfig
	{
        public IntervalWorkoutConfig()
        {
        }

        #region Properties
        public double PercentMAP { get; set; }
        public string IntervalIntensity
		{
			get;
			set;
		}

		public int NumberOfRepsPerSet
		{
			get;
			set;
		}

		public int NumberOfSets
		{
			get;
			set;
		}

		public string RecoveryIntensity
		{
			get;
			set;
		}

		public int RepDuration
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

		public int TotalDuration
		{
			get;
			set;
		}

		public int TotalNumberOfReps
		{
			get;
			set;
		}

        #endregion

        public override string ToString()
        {

            string text = String.Empty;

            if (this.NumberOfSets > 1)
                text = String.Format("{0} x [{1} x {2} with {3} recovery], with {4} recovery between each set. ", NumberOfSets, NumberOfRepsPerSet, RepDuration, RepRecovery, SetRecovery);
            else
                text = String.Format("[{0} x {1} with {2} recovery]", NumberOfRepsPerSet, RepDuration, RepRecovery);


            return text; 
           // return String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}");
        }



    }
}