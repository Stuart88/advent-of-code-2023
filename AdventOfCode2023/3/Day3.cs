using AdventOfCode2023._2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023._3
{
	internal static class Day3
	{
		private const string FilePath = "3\\Data.txt";
		private static string[] Data => File.ReadAllLines(FilePath);

		public static void SolveOne()
		{

			List<Part> allParts = GetAllParts();

			int result = allParts.Where(x => x.IsPartNumbner).Sum(x => x.PartNumber);

			Console.WriteLine($"Day3 Q1: {result}");
		}

		public static void SolveTwo()
		{
			var allPartsGroupedByLine = GetAllParts().GroupBy(p => p.ReferenceLineNumber).ToArray();

			int result = 0;

			for (int i = 0; i < Data.Length; i++)
			{
				string line = Data[i];

				if (!line.Contains("*"))
					continue;

				List<int> gearIndexes = new();

				for (int j = 0; j < line.Length; j++)
				{
					if (line[j] == '*')
						gearIndexes.Add(j);
				}

				var partLinesToCheck = allPartsGroupedByLine.Where(x => x.Key == i - 1 || x.Key == i || x.Key == i + 1).ToArray();


				foreach (var g in gearIndexes)
				{
					List<int> partNums = new();
					
					foreach (var part in partLinesToCheck)
					{
						var parts = part.ToList() ?? new();


						foreach (var p in parts)
						{
							if (g >= p.Indexes.Min() - 1 && g <= p.Indexes.Max() + 1)
							{
								partNums.Add(p.PartNumber);
							}
						}

					}
					if (partNums.Count == 2)
					{
						result += partNums[0] * partNums[1];
					}
				}

			}

			Console.WriteLine($"Day3 Q2: {result}");
		}

		public static List<Part> GetAllParts()
		{
			List<Part> allParts = new();

			for (int i = 0; i < Data.Length; i++)
			{
				var parts = GetParts(Data[i], i);

				foreach (var part in parts)
				{
					var lineAbove = i > 0 ? Data[i - 1] : string.Empty;
					var lineBelow = i == Data.Length - 1 ? string.Empty : Data[i + 1];

					part.IsPartNumbner = part.HasSymbolsAdjacent(lineAbove) || part.HasSymbolsAdjacent(lineBelow);
				}

				allParts.AddRange(parts);
			}

			return allParts;
		}


		private static List<Part> GetParts(string line, int lineIndex)
		{
			// e.g. "230.........*.....*.442...563..."

			var parts = new List<Part>();

			int index = 0;
			string partNumber = string.Empty;

			Part currentPart = new Part(line, lineIndex);

			foreach (char c in line)
			{
				if (char.IsDigit(c))
				{
					// append to building part num
					partNumber += c;
					currentPart.Indexes.Add(index);
				}
				else if (partNumber.Length > 0)
				{
					currentPart.PartNumber = int.Parse(partNumber);
					parts.Add(currentPart);
					currentPart = new Part(line, lineIndex);
					partNumber = string.Empty;
				}

				index++;
			}

			// clean up at end if final part not complete
			if (partNumber.Length > 0)
			{
				currentPart.PartNumber = int.Parse(partNumber);
				parts.Add(currentPart);
			}

			return parts;
		}

	}
}
