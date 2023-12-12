using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023._4
{
	internal static class Day4
	{
		private const string FilePath = "4\\Data.txt";
		private static string[] Data => File.ReadAllLines(FilePath);

		public static void SolveOne()
		{
			List<Card> cards = ParseCards();

			int result = cards.Sum(x => x.CardPoints);

			Console.WriteLine($"Day4 Q1: {result}");
		}

		public static void SolveTwo()
		{
			List<Card> cards = ParseCards();

			for (int i = 0; i < cards.Count; i++)
			{
				Card card = cards[i];

				foreach (int j in Enumerable.Range(0, card.Copies))
				{
					foreach (int k in Enumerable.Range(1, card.CardWinningNums.Count))
					{
						if (i + k < cards.Count)
						{
							cards[i + k].Copies += 1;
						}
					}
				}
			}

			int result = cards.Sum(x => x.Copies);

			Console.WriteLine($"Day4 Q2: {result}");
		}

		private static List<Card> ParseCards()
		{
			List<Card> cards = new();

			foreach (var line in Data)
			{
				cards.Add(new Card(line));
			}

			return cards;
		}
	}
}
