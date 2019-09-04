using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5._1
{
	class Program
	{
		static void Main(string[] args)
		{
			var n = int.Parse(File.ReadLines("input.txt").First());
			var a = File.ReadLines("input.txt").Skip(1).First().Split().Select(s => int.Parse(s)).ToArray();

			var result = true;

			for (int i = 1; i <= n; i++)
			{
				if (2 * i <= n)
				{
					if (a[i - 1] > a[2 * i - 1])
					{
						result = false;
						break;
					}
				}

				if (2 * i + 1 <= n)
				{
					if (a[i - 1] > a[2 * i])
					{
						result = false;
						break;
					}
				}
			}

			File.WriteAllText("output.txt", result ? "YES" : "NO");
		}
	}
}