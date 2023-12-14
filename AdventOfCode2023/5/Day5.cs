using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023._5
{
	internal static class Day5
	{
		private const string FilePath = "5\\Data.txt";
		private static string[] Data => File.ReadAllLines(FilePath);
		private static List<long> MappedValues = new();

		public static void SolveOne()
		{
			var items = ParseData();

			for (int i = 0; i < MappedValues.Count; i++)
			{
				foreach (var item in items)
				{
					MappedValues[i] = item.CalculateMapping(MappedValues[i]);
				}
			}

			long result = MappedValues.Min();

			Console.WriteLine($"Day5 Q1: {result}");
		}

		private static object _locky = new();

		public static void SolveTwo()
		{
			var items = ParseData();

			var rangePairs = new List<(long start, long end)>();

			bool onStart = true;
			(long start, long end) adding = new();
			foreach (long i in MappedValues)
			{
				if (onStart)
				{
					adding.start = i;
					onStart = false;
				}
				else
				{
					adding.end = adding.start + i;
					onStart = true;
					rangePairs.Add(adding);
					adding = new();
				}
			}

			MappedValues.Clear();

			long result = 0;
			long count = 0;

			rangePairs = rangePairs.OrderBy(x => x.start).ToList();

			Console.WriteLine("Starting...");

			Parallel.ForEach(rangePairs, pair =>
			{
				//147005974
				for (long i = pair.start; i < pair.end; i++)
				{
					count++;
					if (count % 1000000 == 0)
					{
						Console.SetCursorPosition(0, Console.CursorTop);
						Console.Write($"Count: {count:N0}");
					}

					long mapping = i;

					foreach (var item in items)
					{
						mapping = item.CalculateMapping(mapping);
					}

					lock (_locky)
					{
						if (result == 0 || result > mapping)
						{
							result = mapping;
						}
					}

				}
			});

			Console.WriteLine($"Day5 Q1: {result}");
		}

		private static List<AlmanacItem> ParseData()
		{
			var items = new List<AlmanacItem>();
			bool firstLine = true;

			AlmanacItem addingItem = new();

			foreach (var line in Data)
			{
				if (firstLine)
				{
					// first line is seeds
					var numsPart = line.Split(':')[1];
					MappedValues = numsPart.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Select(long.Parse).ToList();
					firstLine = false;
					continue;
				}

				if (string.IsNullOrWhiteSpace(line))
				{
					if (addingItem.HasData)
					{
						items.Add(addingItem);
						addingItem = new();
					}

					continue;
				}

				if (line.Contains("map"))
				{
					addingItem.Name = line;
				}
				else
				{
					addingItem.Maps.Add(new Map(line));
				}
			}

			if (addingItem.HasData)
			{
				items.Add(addingItem);
				addingItem = new();
			}

			return items;
		}
	}
}
