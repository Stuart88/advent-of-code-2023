using AdventOfCode2023._7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023._8
{
	internal static class Day8
	{
		private const string FilePath = "8\\Data.txt";
		private static string[] Data => File.ReadAllLines(FilePath);

		private static string Instructions = string.Empty;

		public static void SolveOne()
		{
			int result = 0;

			var data = ParseData();

			string currentKey = "AAA";

			var allKeys = data.Select(x => x.Key).ToList();
			var hitKeys = new List<string>();

			var uniqueHits = new List<string>();

			Console.WriteLine();

			int loops = 0;

			while (currentKey != "ZZZ")
			{
				foreach (char step in Instructions)
				{
					Console.SetCursorPosition(0, Console.CursorTop);
					Console.Write($"Count: {loops++} : {hitKeys.Count} / {allKeys.Count}");

					if (!hitKeys.Contains(currentKey))
					{
						hitKeys.Add(currentKey);
					}

					if (currentKey == "ZZZ")
						break;

					if (step == 'L')
					{
						currentKey = data[currentKey].L;
					}
					else
					{
						currentKey = data[currentKey].R;
					}

					result++;
				}
			}

			Console.WriteLine();


			Console.WriteLine($"Day8 Q1: {result}");
		}

		public static void SolveTwo()
		{
			long result = 0;

			var data = ParseData();

			var currentyKeys = data
				.Where(x => x.Key[2] == 'A')
				.Select(data => data.Key)
				.ToList();

			List<List<string>> visitedKeys = new List<List<string>>(currentyKeys.Select(x => new List<string>() { }).ToList());

			bool CheckCondition()
			{
				return visitedKeys.All(set => set.Any(x => x[2] == 'Z'));
			}

			while (!CheckCondition())
			{
				foreach (char step in Instructions)
				{
					for (int i = 0; i < currentyKeys.Count; i++)
					{
						if (step == 'L')
						{
							currentyKeys[i] = data[currentyKeys[i]].L;
						}
						else
						{
							currentyKeys[i] = data[currentyKeys[i]].R;
						}

						if (!(visitedKeys[i].Count > 1 && visitedKeys[i].First() == currentyKeys[i]))
						{
							visitedKeys[i].Add(currentyKeys[i]);
						}
					}

					if (CheckCondition())
						break;
				}
			}

			//138521759842 too low?!
			//14229331545

			result = FindLCM(visitedKeys.Select(x => x.Count).ToArray());

			Console.WriteLine($"Day8 Q2: {result}");
		}

		private static Dictionary<string, (string L, string R)> ParseData()
		{
			Dictionary<string, (string L, string R)> result = new();

			bool firstLine = true;

			foreach (var line in Data)
			{
				if (firstLine)
				{
					Instructions = line;
					firstLine = false;
					continue;
				}

				if (string.IsNullOrWhiteSpace(line))
					continue;

				string key = line.Substring(0, 3);
				string L = line.Substring(7, 3);
				string R = line.Substring(12, 3);

				result.Add(key, (L, R));
			}

			return result;
		}

		private static long FindLCM(int[] numbers)
		{
			var primeFactors = numbers.SelectMany(x => FindPrimeFactors(x)).Distinct().ToArray();

			var lcm = primeFactors.Aggregate((product, next) => product * next);

			return primeFactors.Aggregate((x, y) => x * y);
		}

		private static List<long> FindPrimeFactors(int number)
		{
			List<long> primeFactors = new List<long>();

			if (number < 2)
			{
				return primeFactors;
			}

			for (int i = 2; i <= number; i++)
			{
				while (number % i == 0)
				{
					primeFactors.Add(i);
					number /= i;
				}
			}

			return primeFactors;
		}
	}
}
