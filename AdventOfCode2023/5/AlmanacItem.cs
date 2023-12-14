using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023._5
{
	internal class AlmanacItem
	{
		public string Name { get; set; }
		public List<Map> Maps { get; set; } = new();

		public bool HasData => !string.IsNullOrEmpty(Name) && Maps.Count > 0;

		public long CalculateMapping(long val)
		{
			long result = val;

			foreach (var map in Maps)
			{
				if (val >= map.SourceRangeStart && val < map.SourceRangeStart + map.RangeLength)
				{
					result = val + map.MapDiff;
				}
			}

			return result;
		}
	}
}
