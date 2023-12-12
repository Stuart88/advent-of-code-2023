using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023._4
{
	internal class Card
	{
		public Card(string line)
		{
			// e.g. Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53

			string[] idSplit = line.Split(':');
			string idPart = string.Join("", idSplit[0].Where(c => char.IsDigit(c)).ToArray());
			this.Id = int.Parse(idPart);

			string[] numSplit = idSplit[1].Split('|');
			this.WinningNums = numSplit[0].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToList();
			this.CardNums = numSplit[1].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToList();
		}

		public int Id { get; set; }
		public List<int> WinningNums { get; set; } = new();
		public List<int> CardNums { get; set; } = new();
		public List<int> CardWinningNums => this.CardNums.Where(x => this.WinningNums.Contains(x)).ToList();
		public int CardPoints => (int)Math.Pow(2, this.CardWinningNums.Count - 1);

		//n = 0 1 2 3 4 5  6
		//x = 0 1 2 4 8 16 32
		//x = 2^(n-1)

		public int Copies { get; set; } = 1;
	}
}
