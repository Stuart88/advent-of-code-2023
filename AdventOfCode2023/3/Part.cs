using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023._3
{
	internal class Part
	{
		public int PartNumber { get; set; }
		public List<int> Indexes { get; set; } = new();
		public string ReferenceLine { get; set; }
		public int ReferenceLineNumber { get; set; }
		public bool IsPartNumbner { get; set; }

		public Part(string refLine, int refLineNumber)
		{
			this.ReferenceLine = refLine;
			this.ReferenceLineNumber = refLineNumber;
		}

		public bool HasSymbolsAdjacent(string line)
		{
			for (int i = 0; i < Indexes.Count; i++)
			{
				int index = Indexes[i];

				if (index > 0 && IsPartNumberSymbol(ReferenceLine[index - 1]))
				{
					return true;
				}

				if(i == Indexes.Count - 1 && index + 1 < ReferenceLine.Length && IsPartNumberSymbol(ReferenceLine[index + 1]))
				{
					return true;
				}


				for (int j = index - 1; j <= index + 1; j++)
				{
					if (j < 0 || j >= line.Length)
						continue;

					if (IsPartNumberSymbol(line[j]))
						return true;
				}
			}

			return false;
		}

		public override string ToString()
		{
			return $"{PartNumber}: [{string.Join(",", Indexes)}]";
		}

		private bool IsPartNumberSymbol(char c)
		{
			return c != '.' && !char.IsDigit(c);
		}

	}
}
