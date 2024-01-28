using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023._7
{
	internal class CardHandComparer : IComparer<CardHand>
	{
		public int Compare(CardHand x, CardHand y)
		{
			return x.IsHigherThan(y) ? 1 : -1;
		}
	}
}
