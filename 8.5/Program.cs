using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8._5
{
	class Program
	{
		public static int hashOf(string s, int multiple)
		{
			int rv = 0;
			for (int i = 0; i < s.Length; ++i)
			{
				rv = multiple * rv + s[i];
			}
			return rv;
		}

		static void Main(string[] args)
		{
			var lines = File.ReadLines("input.txt").ToList();
			var n = int.Parse(lines[0]);

			const int Q = 7;
			const int N = 1 << Q;

			char[] S = new char[N];
			char[] notS = new char[N];

			for (int i = 0; i < N; i++)
			{
				S[i] = (char)(97 + (Convert.ToString(i, 2).Count(c => c == '1') % 2));
				notS[i] = S[i] == 'a' ? 'b' : 'a';
			}

			var s1 = new string(S);
			var s2 = new string(notS);

			var sAdd = "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb";

			var strings = new List<string>();

			for (int i = 0; i < n; i++)
			{
				var arr = new BitArray(new int[] { i });

				var s = new StringBuilder();

				for (int g = 0; g < 14; g++)
				{
					//s.Append(arr[g] ? s1 + sAdd : s2 + sAdd);
					s.Append(arr[g] ? s1 : s2);
				}

				s.Append(sAdd);
				strings.Add(s.ToString());
			}

			//const int m = 777;

			//var h = hashOf(strings[0], m);

			//foreach (var item in strings)
			//{
			//	if (hashOf(item, m) != h)
			//	{
			//		break;
			//	}
			//}

			//Если к двум строкам с одинаковым значением полиномиального хеша дописать один и тот же символ в начало/конец, 
			//то хеши останутся одинаковыми.

			File.WriteAllLines("output.txt", strings);
		}
	}
}
