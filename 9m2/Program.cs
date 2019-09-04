using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9m2
{
	class Program
	{
		static int[] prefix(string t)
		{
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

			return p;
		}

		static void Main(string[] args)
		{
			var t = File.ReadLines("input.txt").First();
			var s = File.ReadLines("input.txt").Skip(1).First();

			var m = t.Length;
			var n = s.Length;

			var p = prefix(t);

			var i = 0;
			var j = 0;

			var r = new List<int>();

			if (n >= m)
			{
				while (i < n)
				{
					if (s[i] == t[j])
					{
						i++;
						j++;
					}

					if (j == m)
					{
						r.Add(i - m + 1);
						j = p[j - 1];
					}
					else if (i < n && s[i] != t[j])
					{
						if (j > 0)
						{
							j = p[j - 1];
						}
						else
						{
							i++;
						}
					}

					
				}
			}

			var result = new List<string>
			{
				$"{r.Count}",
				string.Join(" ", r)
			};

			File.WriteAllLines("output.txt", result);
		}
	}
}
