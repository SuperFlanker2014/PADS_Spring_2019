using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10._3
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
			var result = new List<Tuple<string, int>>();

			int i;
			int[] p;

			var s = t;

			while (s.Any())
			{
				p = prefix(s);

				for (i = 0; i < s.Length; i++)
				{
					if (p[i] > 0)
					{
						break;
					}
				}

				var i1 = i;

				int i2;
				int div;
				int mod;

				if (i < s.Length)
				{
					var indicator = p[i] - p[i - 1];

					while (indicator == 1 && i < s.Length)
					{
						indicator = p[i] - p[i - 1];
						i++;
					}

					i2 = i == s.Length ? i : i - 1; //правильно ли?

					div = i2 / i1;
					mod = i2 % i1;
				}
				else //первый символ строки нигде не повторяется
				{
					i2 = i == s.Length ? i : i - 1;

					div = 1;
					mod = 0;
					i1 = 1;
				}

				int cnt;
				int len;

				if (mod == 0)
				{
					cnt = div;
					len = i1;
				}
				else
				{
					if (div > 0)
					{
						cnt = div;
						len = i1;
					}
					else
					{
						cnt = 1;
						len = i2 - i1;
					}
				}

				result.Add(new Tuple<string, int>(s.Substring(0, len), cnt));

				s = s.Substring(len * cnt);
			}

			var result2 = new List<string>();

			for (int item = 0; item < result.Count; item++)
			{
				if (result[item].Item2 > 1)
				{
					result2.Add($"{result[item].Item1}*{result[item].Item2}");
				}
				else
				{
					var startItem = item;

					while (item < result.Count && result[item].Item2 == 1)
					{
						item++;
					}

					item = item - 1;

					var str = new StringBuilder();

					for (int v = startItem; v <= item; v++)
					{
						str.Append(result[v].Item1);
					}

					result2.Add(str.ToString());
				}
			}

			var res = string.Join("+", result2);

			File.WriteAllText("output.txt", res.Length > t.Length ? t : res );
		}
	}
}
