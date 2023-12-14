using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023._6
{
	internal static class Day6
	{
		private const string FilePath = "6\\Data.txt";
		private static string[] Data => File.ReadAllLines(FilePath);

		public static void SolveOne()
		{
			var raceCards = ParseDataOne();

			int result = raceCards.Select(x => x.TotalPossibleChargeTimes).Aggregate((x, y) => x * y);

			Console.WriteLine($"Day6 Q1: {result}");
		}

		public static void SolveTwo()
		{
			var raceCard = ParseDataTwo();

			int result = raceCard.TotalPossibleChargeTimes;

			Console.WriteLine($"Day6 Q2: {result}");
		}

		private static List<RaceCard> ParseDataOne()
		{
			/// e.g.
			/// Time:        41     96     88     94
			/// Distance:   214   1789   1127   1055
			
			var times = Data[0].Split(' ').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList();
			var distances = Data[1].Split(' ').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList();

			var result = new List<RaceCard>();

			for (int i = 0; i < times.Count; i++) 
			{
				result.Add(new RaceCard(times[i], distances[i]));
			}

			return result;
		}

		private static RaceCard ParseDataTwo()
		{
			/// e.g.
			/// Time:        41     96     88     94
			/// Distance:   214   1789   1127   1055
			
			var time = double.Parse(Data[0].Split(':')[1].Replace(" ", ""));
			var distance = double.Parse(Data[1].Split(':')[1].Replace(" ", ""));

			return new RaceCard(time, distance);
		}
	}
}
