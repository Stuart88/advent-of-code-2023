using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023._7
{
	internal enum HandType
	{
		/// <summary>
		/// AAAAA
		/// </summary>
		FiveOfAKind,
		/// <summary>
		/// AAAA3
		/// </summary>
		FourOfAKind,
		/// <summary>
		/// 22444
		/// </summary>
		FullHouse,
		/// <summary>
		/// 12444
		/// </summary>
		ThreeOfAKind,
		/// <summary>
		/// 22441
		/// </summary>
		TwoPair,
		/// <summary>
		/// 22345
		/// </summary>
		OnePair,
		/// <summary>
		/// 12345 (all distinct)
		/// </summary>
		HighCard
	}

	internal class CardHand
	{
		public CardHand(string line, bool useJokers = false)
		{
			if (useJokers)
			{
				// Joker
				CardValues.Add('J', 1);
			}
			else
			{
				// Jack
				CardValues.Add('J', 11);
			}

			var data = line.Split(' ');

			this.Cards = data[0].ToArray();
			this.BidAmount = int.Parse(data[1]);

			this.Type = useJokers
				? this.FindHandTypeWithJokers()
				: this.FindHandType();
		}

		private Dictionary<char, int> CardValues = new Dictionary<char, int>()
		{
			{ '2', 2 },
			{ '3', 3 },
			{ '4', 4 },
			{ '5', 5 },
			{ '6', 6 },
			{ '7', 7 },
			{ '8', 8 },
			{ '9', 9 },
			{ 'T', 10 },
			{ 'Q', 12 },
			{ 'K', 13 },
			{ 'A', 14 },
		};

		public char[] Cards { get; set; }
		public int BidAmount { get; set; }
		public HandType Type { get; set; }

		public bool IsHigherThan(CardHand compareHand)
		{
			if (this.Type != compareHand.Type)
			{
				return this.Type < compareHand.Type;
			}

			for (int i = 0; i < this.Cards.Length; i++)
			{
				if (CardValues[this.Cards[i]] > CardValues[compareHand.Cards[i]])
					return true;

				if (CardValues[this.Cards[i]] < CardValues[compareHand.Cards[i]])
					return false;
			}

			return true;
		}

		private HandType FindHandTypeWithJokers()
		{
			if (this.Cards.All(c => c != 'J'))
				return this.FindHandType();

			var groupedCards = this.Cards.GroupBy(c => c);

			if (groupedCards.Count() <= 2)
			{
				// JJJJJ, JJJJX, JJJXX, etc
				return HandType.FiveOfAKind;
			}

			if (groupedCards.Count() == 3)
			{
				// JXYYY, XXYYJ, XXYJJ, XYJJJ

				if (groupedCards.Any(x => x.Key != 'J' && x.Count() == 3))
					//JXYYY
					return HandType.FourOfAKind;

				if (groupedCards.Where(x => x.Key == 'J').First().Count() >= 2)
					//XXYJJ, XYJJJ
					return HandType.FourOfAKind;

				// XXYYJ
				return HandType.FullHouse;
			}

			if (groupedCards.Count() == 4)
			{
				// JJXYZ, JXXYZ
				return HandType.ThreeOfAKind;
			}

			// JXYZK
			return HandType.OnePair;
		}

		private HandType FindHandType()
		{
			var groups = this.Cards.GroupBy(c => c);

			if (groups.Count() == 5)
			{
				return HandType.HighCard;
			}

			if (groups.Count() == 4)
			{
				return HandType.OnePair;
			}

			if (groups.Count() == 3)
			{
				if (groups.Any(grp => grp.Count() == 3))
				{
					return HandType.ThreeOfAKind;
				}
				else
				{
					return HandType.TwoPair;
				}
			}

			if (groups.Count() == 2)
			{
				if (groups.Any(grp => grp.Count() == 4))
				{
					return HandType.FourOfAKind;
				}
				else
				{
					return HandType.FullHouse;
				}
			}

			return HandType.FiveOfAKind;
		}

		public override string ToString()
		{
			return $"{string.Join("", this.Cards)} {this.BidAmount}";
		}
	}
}
