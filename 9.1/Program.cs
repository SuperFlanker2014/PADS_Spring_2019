using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9._1
{
	class Program
	{
		static void Main(string[] args)
		{
			var lines = File.ReadLines("input.txt").ToList();
			var stringToFind = lines[0];
			var longString = lines[1];

			var result = new List<int>();

			for (int i = 0; i <= longString.Length - stringToFind.Length; i++)
			{
				var ok = true;

				for (int j = 0; j < stringToFind.Length; j++)
				{
					if (longString[i + j] != stringToFind[j])
					{
						ok = false;
						break;
					}
				}

				if (ok)
				{
					result.Add(i + 1);
				}
			}

			var res = new List<string>();
			res.Add($"{result.Count()}");
			res.Add($"{string.Join(" ", result)}");

			File.WriteAllLines("output.txt", res);
		}
	}
}
