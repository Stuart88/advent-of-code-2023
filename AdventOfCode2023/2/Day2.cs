using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023._2
{
	internal static class Day2
	{
		private const string FilePath = "2\\Data.txt";
		private static string[] Data => File.ReadAllLines(FilePath);

		public static void SolveOne()
		{
			int result = 0;

			foreach (var line in Data)
			{
				GameData gameData = new GameData(line);
				if (gameData.IsPossible)
				{
					result += gameData.Id;
				}
			}

			Console.WriteLine($"Day2 Q1: {result}");
		}

		public static void SolveTwo()
		{
			int result = 0;

			foreach (var line in Data)
			{
				GameData gameData = new GameData(line);
				result += gameData.Power;
			}

			Console.WriteLine($"Day2 Q2: {result}");
		}
	}
}
