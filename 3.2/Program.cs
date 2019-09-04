using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _3._2
{
	class Program
	{
		static void Main(string[] args)
		{
			//Generator(); return;
			var firstLine = File.ReadLines("input.txt").First().Split().Select(s => int.Parse(s)).ToList();

			var n = firstLine[1];
			var m = firstLine[0];
			var k = firstLine[2];

			var strings = new byte[n][];
			int cnt = 0;

			foreach (var line in File.ReadLines("input.txt", Encoding.ASCII).Skip(1))
			{
				strings[cnt] = Encoding.ASCII.GetBytes(line);
				//GetByte(line, dd[0], m);
				cnt++;
			}

			//var lines = File.ReadLines("inputBig.txt").ToList();
			//firstLine = lines[0].Split().Select(s => int.Parse(s)).ToList();

			//var n = firstLine[1];
			//var m = firstLine[0];
			//var k = firstLine[2];

			//var strings = new byte[n, m];

			//for (int row = 0; row < n; row++)
			//{
			//	for (int column = 0; column < m; column++)
			//	{
			//		strings[row, column] = (byte)lines[row + 1][column];
			//	}
			//}

			int[] indices = new int[m];
			for (int i = 0; i < m; i++)
				indices[i] = i;

			int[] t = new int[m];

			int[] count = new int[26];
			int[] pref = new int[26];

			for (int shift = n - 1; shift >= n - k; shift -= 1)
			{
				for (int j = 0; j < count.Length; j++)
					count[j] = 0;

				for (int i = 0; i < m; i++)
				{
					//count[strings[shift, indices[i]] - 97]++;
					count[strings[shift][indices[i]] - 97]++;
				}


				pref[0] = 0;
				for (int i = 1; i < count.Length; i++)
					pref[i] = pref[i - 1] + count[i - 1];

				for (int i = 0; i < m; i++)
				{
					//t[pref[strings[shift, indices[i]] - 97]++] = indices[i];
					t[pref[strings[shift][indices[i]] - 97]++] = indices[i];
				}

				t.CopyTo(indices, 0);
			}

			for (int i = 0; i < m; i++)
				indices[i]++;

			File.WriteAllText("output.txt", string.Join(" ", indices));
		}

		//unsafe private static void GetByte(string tempText, byte[] tempByte, int len)
		//{
		//	unsafe
		//	{
		//		fixed (void* ptr = tempText)
		//		{
		//			System.Runtime.InteropServices.Marshal.Copy(new IntPtr(ptr), tempByte, 0, len);
		//		}
		//	}
		//}

		//public static string UnsafeAsciiBytesToString(byte[] buffer, int offset, int length)
		//{
		//	unsafe
		//	{
		//		fixed (byte* pAscii = buffer)
		//		{
		//			return new Char((sbyte*)pAscii, offset, length);
		//		}
		//	}
		//}

		//static void Generator()
		//{
		//	var n = 1000000;
		//	var m = 50;
		//	var k = 50;

		//	var lines = new string[m+1];
		//	lines[0] = $"{n} {m} {k}";

		//	for (int row = 1; row < m + 1; row++)
		//	{
		//		var g = new List<char>();

		//		for (int i = 0; i < n; i++)
		//		{
		//			g.Add('a');
		//		}

		//		lines[row] = string.Join("", g);
		//	}

		//	File.WriteAllLines("inputBig.txt", lines);
		//}
	}
}