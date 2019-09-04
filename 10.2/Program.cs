using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10._2
{
	class Program
	{
		static void Main(string[] args)
		{
			var s = File.ReadLines("input.txt").First();
			var n = s.Length;
			var z = new int[n - 1];

			var L = 0;
			var R = 0;

			for (int i = 1; i <= n - 1; i++)
			{
				if (i >= R)
				{
					var j = 0;
					while (i + j < n && s[i + j] == s[j])
					{
						j++;
					}
					L = i;
					R = i + j;
					z[i - 1] = j;
				}
				else
				{
					if (z[i - L - 1] < R - i)
					{
						z[i - 1] = z[i - L - 1];
					}
					else
					{
						var j = R - i;
						while (i + j < n && s[i + j] == s[j])
						{
							j++;
						}
						L = i;
						R = i + j;
						z[i - 1] = j;
					}
				}
			}

			File.WriteAllText("output.txt", string.Join(" ", z));
		}
	}
}
