using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023._5
{
	internal class Map
	{
		public Map(string line) 
		{
			long d, s, r;
			var split = line.Split(' ');
			d = long.Parse(split[0]);
			s = long.Parse(split[1]);
			r = long.Parse(split[2]);
			this.DestinationRangeStart = d;
			this.SourceRangeStart = s;
			this.RangeLength = r;
		}

		public long DestinationRangeStart { get; set; }
		public long SourceRangeStart { get; set; }
		public long RangeLength { get; set; }
		public long MapDiff => DestinationRangeStart - SourceRangeStart;
	}
}
