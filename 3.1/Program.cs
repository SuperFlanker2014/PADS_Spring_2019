using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._1
{
	class Program
	{
		static void Main(string[] args)
		{
			var lines = File.ReadLines("input.txt").ToList();
			var line1 = lines[0].Split().Select(s => int.Parse(s)).ToList();

			var n = line1[0];
			var m = line1[1];
			var A = lines[1].Split().Select(s => ushort.Parse(s)).ToList();
			var B = lines[2].Split().Select(s => ushort.Parse(s)).ToList();

			var AnB = new uint[n * m];

			int cnt = 0;
			for (int indA = 0; indA < n; indA++)
			{
				for (int indB = 0; indB < m; indB++)
				{
					AnB[cnt++] = (uint)(A[indA] * B[indB]);
				}
			}

			RadixSort(AnB);
			
			ulong sum = 0;
			for (int i = 0; i < n * m; i += 10)
			{
				sum += AnB[i];
			}

			File.WriteAllText("output.txt", sum.ToString());
		}

		static void RadixSort(uint[] a)
		{
			uint[] t = new uint[a.Length];

			int r = 8; 
			int b = 32;

			int[] count = new int[1 << r];
			int[] pref = new int[1 << r];

			int groups = (int)Math.Ceiling((double)b / (double)r);
			int mask = (1 << r) - 1;

			for (int c = 0, shift = 0; c < groups; c++, shift += r)
			{
				for (int j = 0; j < count.Length; j++)
					count[j] = 0;

				for (int i = 0; i < a.Length; i++)				
					count[((a[i] >> shift) & mask)]++;				

				pref[0] = 0;
				for (int i = 1; i < count.Length; i++)
					pref[i] = pref[i - 1] + count[i - 1];

				for (int i = 0; i < a.Length; i++)
					t[pref[(a[i] >> shift) & mask]++] = a[i];

				t.CopyTo(a, 0);
			} 
		}
	}
}