using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10._1
{
	class Program
	{
		static void Main(string[] args)
		{
			var t = "145614561456790";// File.ReadLines("input.txt").First();
			var n = t.Length;
			var p = new int[n];

			var i = 1;
			var j = 0;

			while (i < n)
			{
				if (t[i] == t[j])
				{
					p[i] = j + 1;
					i++;
					j++;
				}
				else
				{
					if (j > 0)
					{
						j = p[j - 1];
					}
					else
					{
						p[i] = 0;
						i++;
					}
				}
			}

			File.WriteAllText("output.txt", string.Join(" ", p));
		}
	}
}
