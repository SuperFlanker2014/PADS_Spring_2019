using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9._2
{
	class Program
	{
		static char[] alphabet = new char[26] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

		static void Main(string[] args)
		{
			var txt = File.ReadLines("input.txt").First().Replace(" ", "");
			long res = 0;

			var indices = new Dictionary<char, List<int>>();
			foreach (var item in alphabet)
			{
				indices[item] = new List<int>();
			}

			for (int i = 0; i < txt.Length; i++)
			{
				indices[txt[i]].Add(i);
			}

			foreach (var item in indices)
			{
				//var g = 0;
				//foreach (var index1 in item.Value)
				//{
				//	foreach (var index2 in item.Value)
				//	{
				//		var diff = Math.Abs(index2 - index1);

				//		if (diff > 1)
				//		{
				//			g += diff - 1;
				//		}
				//	}
				//}

				var distances = new long[item.Value.Count];

				for (int ind = 1; ind < item.Value.Count; ind++)
				{
					distances[0] += item.Value[ind] - item.Value[0] - 1;
				}

				for (int ind = 1; ind < item.Value.Count; ind++)
				{
					var prevDiff = item.Value[ind] - item.Value[ind - 1];
					distances[ind] =
						distances[ind - 1]
						- (item.Value.Count - ind - 1) * prevDiff
						+ (ind - 1) * prevDiff;
						;
				}

				res += distances.Sum();
			}

			File.WriteAllText("output.txt", $"{res / 2}");
		}

		static int countPS(string str)
		{
			int N = str.Length;

			// create a 2D array to store the  
			// count of palindromic subsequence 
			int[,] cps = new int[N + 1, N + 1];

			// palindromic subsequence 
			// of length 1 
			for (int i = 0; i < N; i++)
				cps[i, i] = 1;

			// check subsequence of length   
			// L is palindrome or not 
			for (int L = 2; L <= N; L++)
			{
				for (int i = 0; i < N; i++)
				{
					int k = L + i - 1;
					if (k < N)
					{
						if (str[i] == str[k])
							cps[i, k] = cps[i, k - 1] +
										cps[i + 1, k] + 1;
						else
							cps[i, k] = cps[i, k - 1] +
										cps[i + 1, k] -
										cps[i + 1, k - 1];
					}
				}
			}

			// return total palindromic 
			// subsequence 
			return cps[0, N - 1];
		}
	}
}
