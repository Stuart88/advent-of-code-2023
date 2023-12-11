using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023._1
{
	internal static class Day1
	{
		#region Private Fields

		private static string[] numbers =
		{
			"1", "2", "3", "4",
			"5", "6", "7", "8", "9",
			"one", "two", "three", "four",
			"five", "six", "seven", "eight", "nine"
		};

		private const string FilePath = "1\\Data.txt";

		private static string[] Data => File.ReadAllLines(FilePath);

		#endregion Private Fields

		#region Private Enums

		private enum FirstOrLast
		{
			First,
			Last
		}

		#endregion Private Enums

		#region Public Methods

		public static void SolveOne()
		{
			int result = 0;

			foreach (var line in Data)
			{
				string digits = string.Join("", line.Where(char.IsNumber));

				if (digits.Length > 0)
				{
					result += int.Parse(digits.First().ToString() + digits.Last().ToString());
				}
			}

			Console.WriteLine($"Day1 Q1: {result}");
		}

		public static void SolveTwo()
		{
			int result = 0;

			foreach (var line in Data)
			{
				string first = GetNumber(line, FirstOrLast.First);
				string last = GetNumber(line, FirstOrLast.Last);

				result += int.Parse(first.ToString() + last.ToString());
			}

			Console.WriteLine($"Day1 Q2: {result}");
		}

		#endregion Public Methods

		#region Private Methods

		private static string ConvertToDigit(string numberString)
		{
			return numberString switch
			{
				"one" => "1",
				"two" => "2",
				"three" => "3",
				"four" => "4",
				"five" => "5",
				"six" => "6",
				"seven" => "7",
				"eight" => "8",
				"nine" => "9",
				_ => numberString
			};
		}

		private static string GetNumber(string line, FirstOrLast firstOrLast)
		{
			string x = string.Empty;

			if (firstOrLast == FirstOrLast.Last)
			{
				line = string.Join("", line.Reverse());
			}

			foreach (char c in line)
			{
				if (firstOrLast == FirstOrLast.First)
				{
					x += c;
				}
				else
				{
					x = c + x;
				}

				var containedNumber = numbers.FirstOrDefault(n => x.Contains(n));

				if (containedNumber != null)
					return ConvertToDigit(containedNumber);
			}

			throw new Exception("Number not found!");
		}

		#endregion Private Methods
	}
}