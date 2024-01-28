using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023._7
{


	internal static class Day7
	{
		private const string FilePath = "7\\Data.txt";
		private static string[] Data => File.ReadAllLines(FilePath);



		public static void SolveOne()
		{
			var cards = ParseCards();

			cards.Sort(new CardHandComparer());

			int result = 0;

			for (int i = 0; i < cards.Count; i++)
			{
				result += ((i + 1) * cards[i].BidAmount);
			}

			Console.WriteLine($"Day7 Q1: {result}");
		}

		public static void SolveTwo()
		{
			var cards = ParseCards(true);

			cards.Sort(new CardHandComparer());

			//var grp = cards.Where(c => c.Cards.Any(x => x == 'J')).GroupBy(x => x.Type);

			int result = 0;

			string filePath = "output.txt";

			// Use a using statement to ensure the StreamWriter is properly disposed of
			using (StreamWriter writer = new StreamWriter(filePath))
			{

				for (int i = 0; i < cards.Count; i++)
				{
					result += ((i + 1) * cards[i].BidAmount);
					writer.WriteLine(result.ToString());
				}

				Console.WriteLine("Objects written to file successfully.");
			}
			Console.WriteLine($"Day7 Q2: {result}");
		}

		private static List<CardHand> ParseCards(bool userJokers = false)
		{
			List<CardHand> cards = new();

			foreach (string line in Data)
			{
				cards.Add(new CardHand(line, userJokers));
			}

			return cards;
		}
	}
}
