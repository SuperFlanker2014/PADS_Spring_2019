using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9m
{
	class Program
	{
		static long p = 137447;
		static long x = 2341;

		static long p1 = 337081;
		static long x1 = 641;

		public static long hashOf(string s, long multiple, long mod)
		{
			long rv = 0;
			for (int i = 0; i < s.Length; ++i)
			{
				rv = (multiple * rv + s[i]) % mod;
			}
			return rv;
		}

		public static long longPow(long x, int pow, long mod)
		{
			long result = 1;

			for (long i = 0; i < pow; i++)
			{
				result = (result * x) % mod;
			}
			return result;
		}

		static void Main(string[] args)
		{
			var result = new List<int>();

			var lines = File.ReadLines("input720000.txt").ToList();
			var stringToFind = lines[0];
			var longString = lines[1];

			var n = longString.Length;
			var m = stringToFind.Length;

			var ggg = 0;

			if (m <= n)
			{
				var stringToFindHash = hashOf(stringToFind, x, p);
				long xx = longPow(x, m, p);
				var H = new long[n - m + 1];
				H[0] = hashOf(longString.Substring(0, m), x, p);
				for (int i = 0; i <= n - m - 1; i++)
				{
					H[i + 1] = (p * p + H[i] * x - (Convert.ToByte(longString[i]) * xx) % p + Convert.ToByte(longString[i + m])) % p;
				}

				var stringToFindHash1 = hashOf(stringToFind, x1, p1);
				long xx1 = longPow(x1, m, p1);
				var H1 = new long[n - m + 1];
				H1[0] = hashOf(longString.Substring(0, m), x1, p1);
				for (int i = 0; i <= n - m - 1; i++)
				{
					H1[i + 1] = (p1 * p1 + H1[i] * x1 - (Convert.ToByte(longString[i]) * xx1) % p1 + Convert.ToByte(longString[i + m])) % p1;
				}

				for (int i = 0; i <= n - m; i++)
				{
					if (H[i] != stringToFindHash)
					{
						continue;
					}

					//if ((H[i] == stringToFindHash) && (H1[i] == stringToFindHash1))
					//{
					//	result.Add(i + 1);
					//	continue;
					//}

					var ok = true;

					for (int j = 0; j < m; j++)
					{
						if (longString[i + j] != stringToFind[j])
						{
							ok = false;
							break;
						}

						ggg++;
					}

					if (ok)
					{
						result.Add(i + 1);
					}
				}
			}

			var res = new List<string>();
			res.Add($"{result.Count}");
			res.Add($"{string.Join(" ", result)}");

			File.WriteAllLines("output.txt", res);
		}
	}
}