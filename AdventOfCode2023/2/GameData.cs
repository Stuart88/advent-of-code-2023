using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023._2
{
	internal class GameData
	{
		public GameData(string line) 
		{
			//e.g. Game 1: 3 blue, 4 red; 3 blue, 4 red; 2 green
			var splitAtID = line.Split(':');

			this.Id = int.Parse(splitAtID[0].Split(' ')[1].ToString());

			var splitBySection = splitAtID[1].Split(";");
			// e.g.
			//  3 blue, 4 red
			//  3 blue, 4 red
			//  2 green

			foreach(var section in splitBySection)
			{
				int red = TryGetColour(section, "red");
				int green = TryGetColour(section, "green");
				int blue = TryGetColour(section, "blue");

				if(red > this.RedMax) { this.RedMax = red; }
				if(green > this.GreenMax) { this.GreenMax = green; }
				if(blue > this.BlueMax) { this.BlueMax = blue; }
			}
		}

		private static int TryGetColour(string colourLine, string colour)
		{
			//e.g.  1 red, 16 green, 3 blue
			var colourSplit = colourLine.Split($" {colour}");
			//e.g. [{1 red, 16}, { 3 blue}]

			// Need to pick out '16'
			return colourSplit.Length > 1 
				? int.Parse(colourSplit[0].Split(' ')[^1].ToString())
				: 0;
		}

		public int Id { get; set; }
		public int RedMax { get; set; } = 0;
		public int GreenMax { get; set; } = 0;
		public int BlueMax { get; set; } = 0;

		/// <summary>
		/// "The Elf would first like to know which games would have been possible if the bag contained only 12 red cubes, 13 green cubes, and 14 blue cubes?"
		/// </summary>
		public bool IsPossible => this.RedMax <= 12 && this.GreenMax <= 13 && this.BlueMax <= 14;
		public int Power => this.RedMax * this.GreenMax * this.BlueMax;
	}
}
