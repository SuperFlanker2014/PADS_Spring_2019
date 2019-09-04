using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6._2
{
	class Program
	{
		static void Main(string[] args)
		{
			var line = File.ReadLines("input.txt").First().Split();

			var itemsCount = int.Parse(line[0]);
			var A = double.Parse(line[1], System.Globalization.CultureInfo.InvariantCulture);

			var res = new double[itemsCount + 1];

			int cnt;
			for (cnt = 1; cnt <= itemsCount; cnt++)
			{
				res[cnt] = A / cnt - cnt + 1;

				if (res[cnt] < 0)
					break;
			}

			cnt = Math.Min(cnt, itemsCount);

			var res1 = new double[itemsCount + 1];
			res1[1] = A;
			res1[cnt] = 0;

			for (int j = cnt - 1; j > 1; j--)
			{
				res1[j] = res1[1] / j + ((j - 1)*1.0 / j) * res1[j + 1] - j + 1;
			}

			for (int j = cnt + 1; j <= itemsCount; j++)
			{
				res1[j] = 2 * res1[j - 1] - res1[j - 2] + 2;
			}

			File.WriteAllText("output.txt", res1[itemsCount].ToString("G17", System.Globalization.CultureInfo.InvariantCulture));
		}
	}
}
