using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TriathlonPhysics
{

    
    public static class IntervalCalculations  //ThibualtIntervalCalculations
    {
		private static List<IntervalWorkoutConfig> BuildThibaultInterval(List<ThibaultModelItem> list)
		{
			List<ThibaultModelGuideline> thibaultGuidelines = IntervalCalculations.GetThibaultGuidelines();
			List<IntervalWorkoutConfig> intervalWorkoutConfigs = new List<IntervalWorkoutConfig>();
			foreach (ThibaultModelItem thibaultModelItem in list)
			{
				if ((thibaultModelItem.TotalReps <= 24 ? false : thibaultModelItem.TotalReps <= 30))
				{
					ThibaultModelGuideline thibaultModelGuideline = thibaultGuidelines.First<ThibaultModelGuideline>((ThibaultModelGuideline x) => x.NumSets == 4);
					intervalWorkoutConfigs.Add(IntervalCalculations.ConfigureThibaultWorkout(thibaultModelGuideline, thibaultModelItem, thibaultModelItem.MaxAerobicPower));
				}
				else if ((thibaultModelItem.TotalReps <= 14 ? false : thibaultModelItem.TotalReps <= 24))
				{
					ThibaultModelGuideline thibaultModelGuideline1 = thibaultGuidelines.First<ThibaultModelGuideline>((ThibaultModelGuideline x) => x.NumSets == 3);
					intervalWorkoutConfigs.Add(IntervalCalculations.ConfigureThibaultWorkout(thibaultModelGuideline1, thibaultModelItem, thibaultModelItem.MaxAerobicPower));
				}
				else if ((thibaultModelItem.TotalReps <= 7 ? false : thibaultModelItem.TotalReps <= 14))
				{
					ThibaultModelGuideline thibaultModelGuideline2 = thibaultGuidelines.First<ThibaultModelGuideline>((ThibaultModelGuideline x) => x.NumSets == 2);
					intervalWorkoutConfigs.Add(IntervalCalculations.ConfigureThibaultWorkout(thibaultModelGuideline2, thibaultModelItem, thibaultModelItem.MaxAerobicPower));
				}
				else if ((thibaultModelItem.TotalReps < 3 ? false : thibaultModelItem.TotalReps <= 7))
				{
					ThibaultModelGuideline thibaultModelGuideline3 = thibaultGuidelines.First<ThibaultModelGuideline>((ThibaultModelGuideline x) => x.NumSets == 1);
					intervalWorkoutConfigs.Add(IntervalCalculations.ConfigureThibaultWorkout(thibaultModelGuideline3, thibaultModelItem, thibaultModelItem.MaxAerobicPower));
				}
			}
			return intervalWorkoutConfigs;
		}

		private static IntervalWorkoutConfig ConfigureThibaultWorkout(ThibaultModelGuideline g, ThibaultModelItem i, int maxAerobicPower)
		{
            IntervalWorkoutConfig intervalWorkoutConfig = new IntervalWorkoutConfig();

            intervalWorkoutConfig.IntervalIntensity = string.Concat(maxAerobicPower.ToString(), "% MAP");
            intervalWorkoutConfig.NumberOfRepsPerSet = System.Convert.ToInt32(Math.Round((double)i.TotalReps / (double)g.NumSets));
            intervalWorkoutConfig.NumberOfSets = g.NumSets;
            intervalWorkoutConfig.TotalNumberOfReps = intervalWorkoutConfig.NumberOfRepsPerSet * intervalWorkoutConfig.NumberOfSets;
            //intervalWorkoutConfig.RecoveryIntensity = "<60% MAP"; //this is based on user input now.  Get value from the Settings class
            intervalWorkoutConfig.RepDuration = i.Duration;
            intervalWorkoutConfig.RepRecovery = g.RepRecovery;
            intervalWorkoutConfig.SetRecovery = g.SetRecovery;
            intervalWorkoutConfig.TotalDuration = intervalWorkoutConfig.TotalNumberOfReps * (intervalWorkoutConfig.RepDuration + intervalWorkoutConfig.RepRecovery) + intervalWorkoutConfig.NumberOfSets * intervalWorkoutConfig.SetRecovery;
            intervalWorkoutConfig.PercentMAP = (maxAerobicPower / 100.0); 

            return intervalWorkoutConfig;
		}

		public static List<IntervalWorkoutConfig> CreateThibaultIntervalWorkout(int maxAerobicPower, int intervalDuration)
		{
			List<IntervalWorkoutConfig> intervalWorkoutConfigs = new List<IntervalWorkoutConfig>();
			List<ThibaultModelItem> thibaultModel = IntervalCalculations.GetThibaultModel();
			List<ThibaultModelItem> list = (
				from x in thibaultModel
				where (x.MaxAerobicPower != maxAerobicPower ? false : x.Duration == intervalDuration)
				select x).ToList<ThibaultModelItem>();
			return IntervalCalculations.BuildThibaultInterval(list);
		}

		public static List<IntervalWorkoutConfig> CreateThibaultIntervalWorkout2(int totalReps, int intervalDuration)
		{
			List<IntervalWorkoutConfig> intervalWorkoutConfigs = new List<IntervalWorkoutConfig>();
			List<ThibaultModelItem> thibaultModel = IntervalCalculations.GetThibaultModel();
			List<ThibaultModelItem> list = (
				from x in thibaultModel
				where (x.TotalReps != totalReps ? false : x.Duration == intervalDuration)
				select x).ToList<ThibaultModelItem>();
			return IntervalCalculations.BuildThibaultInterval(list);
		}

		public static List<IntervalWorkoutConfig> CreateThibaultIntervalWorkout3(int maxAerobicPower, int totalReps)
		{
			List<IntervalWorkoutConfig> intervalWorkoutConfigs = new List<IntervalWorkoutConfig>();
			List<ThibaultModelItem> thibaultModel = IntervalCalculations.GetThibaultModel();
			List<ThibaultModelItem> list = (
				from x in thibaultModel
				where (x.MaxAerobicPower != maxAerobicPower ? false : x.TotalReps == totalReps)
				select x).ToList<ThibaultModelItem>();
			return IntervalCalculations.BuildThibaultInterval(list);
		}

		public static List<IntervalWorkoutConfig> CreateThibaultIntervalWorkoutByDuration(int intervalDuration)
		{
			List<IntervalWorkoutConfig> intervalWorkoutConfigs = new List<IntervalWorkoutConfig>();
			List<ThibaultModelItem> thibaultModel = IntervalCalculations.GetThibaultModel();
			List<ThibaultModelItem> list = (
				from x in thibaultModel
				where x.Duration == intervalDuration
				select x).ToList<ThibaultModelItem>();
			return IntervalCalculations.BuildThibaultInterval(list);
		}

		public static List<IntervalWorkoutConfig> CreateThibaultIntervalWorkoutByPercentageMAP(int maxAerobicPower)
		{
			List<IntervalWorkoutConfig> intervalWorkoutConfigs = new List<IntervalWorkoutConfig>();
			List<ThibaultModelItem> thibaultModel = IntervalCalculations.GetThibaultModel();
			List<ThibaultModelItem> list = (
				from x in thibaultModel
				where x.MaxAerobicPower == maxAerobicPower
				select x).ToList<ThibaultModelItem>();
			return IntervalCalculations.BuildThibaultInterval(list);
		}

		public static List<IntervalWorkoutConfig> CreateThibaultIntervalWorkoutByTotalReps(int totalReps)
		{
			List<IntervalWorkoutConfig> intervalWorkoutConfigs = new List<IntervalWorkoutConfig>();
			List<ThibaultModelItem> thibaultModel = IntervalCalculations.GetThibaultModel();
			List<ThibaultModelItem> list = (
				from x in thibaultModel
				where x.TotalReps == totalReps
				select x).ToList<ThibaultModelItem>();
			return IntervalCalculations.BuildThibaultInterval(list);
		}

		private static List<ThibaultModelGuideline> GetThibaultGuidelines()
		{
			List<ThibaultModelGuideline> thibaultModelGuidelines = new List<ThibaultModelGuideline>();
			ThibaultModelGuideline thibaultModelGuideline = new ThibaultModelGuideline(4, 60, 180);
			ThibaultModelGuideline thibaultModelGuideline1 = new ThibaultModelGuideline(3, 120, 300);
			ThibaultModelGuideline thibaultModelGuideline2 = new ThibaultModelGuideline(2, 180, 600);
			ThibaultModelGuideline thibaultModelGuideline3 = new ThibaultModelGuideline(1, 300, 0);
			thibaultModelGuidelines.Add(thibaultModelGuideline);
			thibaultModelGuidelines.Add(thibaultModelGuideline1);
			thibaultModelGuidelines.Add(thibaultModelGuideline2);
			thibaultModelGuidelines.Add(thibaultModelGuideline3);
			return thibaultModelGuidelines;
		}

		private static List<ThibaultModelItem> GetThibaultModel()
		{
			List<ThibaultModelItem> thibaultModelItems = new List<ThibaultModelItem>();
			List<ThibaultModelItem> thibaultModelItems1 = new List<ThibaultModelItem>();
			thibaultModelItems.Add(new ThibaultModelItem(110, 22, 30));
			thibaultModelItems.Add(new ThibaultModelItem(110, 8, 60));
			thibaultModelItems.Add(new ThibaultModelItem(110, 4, 90));
			thibaultModelItems.Add(new ThibaultModelItem(110, 1, 120));
			thibaultModelItems.Add(new ThibaultModelItem(105, 30, 30));
			thibaultModelItems.Add(new ThibaultModelItem(105, 12, 60));
			thibaultModelItems.Add(new ThibaultModelItem(105, 7, 90));
			thibaultModelItems.Add(new ThibaultModelItem(105, 4, 120));
			thibaultModelItems.Add(new ThibaultModelItem(100, 18, 60));
			thibaultModelItems.Add(new ThibaultModelItem(100, 10, 90));
			thibaultModelItems.Add(new ThibaultModelItem(100, 6, 120));
			thibaultModelItems.Add(new ThibaultModelItem(100, 4, 150));
			thibaultModelItems.Add(new ThibaultModelItem(95, 24, 60));
			thibaultModelItems.Add(new ThibaultModelItem(95, 14, 90));
			thibaultModelItems.Add(new ThibaultModelItem(95, 9, 120));
			thibaultModelItems.Add(new ThibaultModelItem(95, 7, 150));
			thibaultModelItems.Add(new ThibaultModelItem(95, 5, 180));
			thibaultModelItems.Add(new ThibaultModelItem(95, 3, 210));
			thibaultModelItems.Add(new ThibaultModelItem(90, 20, 90));
			thibaultModelItems.Add(new ThibaultModelItem(90, 14, 120));
			thibaultModelItems.Add(new ThibaultModelItem(90, 10, 150));
			thibaultModelItems.Add(new ThibaultModelItem(90, 8, 180));
			thibaultModelItems.Add(new ThibaultModelItem(90, 6, 210));
			thibaultModelItems.Add(new ThibaultModelItem(90, 4, 240));
			thibaultModelItems.Add(new ThibaultModelItem(90, 3, 270));
			thibaultModelItems.Add(new ThibaultModelItem(85, 30, 90));
			thibaultModelItems.Add(new ThibaultModelItem(85, 22, 120));
			thibaultModelItems.Add(new ThibaultModelItem(85, 16, 150));
			thibaultModelItems.Add(new ThibaultModelItem(85, 13, 180));
			thibaultModelItems.Add(new ThibaultModelItem(85, 10, 210));
			thibaultModelItems.Add(new ThibaultModelItem(85, 8, 240));
			thibaultModelItems.Add(new ThibaultModelItem(85, 7, 270));
			thibaultModelItems.Add(new ThibaultModelItem(85, 6, 300));
			thibaultModelItems.Add(new ThibaultModelItem(85, 5, 330));
			thibaultModelItems.Add(new ThibaultModelItem(85, 4, 360));
			thibaultModelItems.Add(new ThibaultModelItem(85, 3, 390));
			return thibaultModelItems;
		}



	}

    public static class ThibaultMapPercentages
    {
   

        public const int _85 = 85;
        public const int _90 = 90;
        public const int _95 = 95;
        public const int _100 = 100;
        public const int _105 = 105;
        public const int _110 = 110;
    }
}