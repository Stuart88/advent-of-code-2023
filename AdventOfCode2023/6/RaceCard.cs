using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023._6
{
	internal class RaceCard
	{
		public RaceCard(double t, double d)
		{
			this.Time = t;
			this.Distance = d;
		}

		public double Time { get; set; }
		public double Distance { get; set; }

		public int TotalPossibleChargeTimes => this.PossibleCharges();

		/// <summary>
		/// Number of possible charges that would result in a distance better than this.Distance
		/// </summary>
		private int PossibleCharges()
		{
			///  Need to find all integers c for 
			///  
			/// [t-Sqrt(t^2-4d)]/2 < c < [t+Sqrt(t^2-4d)]/2
			///  

			double min = (this.Time - Math.Sqrt(Math.Pow(this.Time,2) - 4 * this.Distance)) / 2d;
			double max = (this.Time + Math.Sqrt(Math.Pow(this.Time,2) - 4 * this.Distance)) / 2d;

			int? minInteger = null;
			int? maxInteger = null;
			
			if (IsInteger(min))
			{
				minInteger = (int)(min + 1);
			}

			if (IsInteger(max))
            {
				maxInteger = (int)(max - 1);
            }

			minInteger ??= (int)Math.Ceiling(min);
			maxInteger ??= (int)Math.Floor(max);

			return maxInteger.Value - minInteger.Value + 1;
		}

		private bool IsInteger(double number)
		{
			return Math.Abs(number - Math.Round(number)) < double.Epsilon;
		}
	}
}
