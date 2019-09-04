using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8._3
{
	public class HashTable
	{
		long?[] hashTableItems;

		int n = 14165497;//14000029;//13861489;//13464239;
		int h2Const = 17;//если больше n, то остаток может быть равен n, тогда i не поменяет ситуацию
		//11,13,17,19,23

		public HashTable()
		{
			hashTableItems = new long?[n];
		}

		private long k(long value)
		{
			if (value < 0)
			{
				return -value;
			}
			else
			{
				return value;
			}
		}

		private int hash(long k)
		{
			var i = 0;
			do
			{
				var j = openHash(k, i);
				if (hashTableItems[j] == null)
				{
					return j;
				}
				else
				{
					i++;
				}
			} while (i < n);

			return -1;//end of table
		}

		private int openHash(long k, int i)
		{
			var v = (k + i * ((k % h2Const) + 1)) % n;
			return (int)v;
		}

		public void Add(long value)
		{
			var h = hash(k(value));
			hashTableItems[h] = value;
		}

		public bool Check(long value)
		{
			var i = 0;
			var j = 0;
			do
			{
				j = openHash(value, i);
				if (hashTableItems[j] == value)
				{
					return true;
				}
				else
				{
					i++;
				}
			} while (hashTableItems[j] != null && i < n);

			return false;
		}

		public int CountFree()
		{
			var cnt = 0;
			for (int i = 0; i < n; i++)
			{
				if (hashTableItems[i] == null)
				{
					cnt++;
				}
			}
			return cnt;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var lines = File.ReadLines("input.txt").ToList();
			var line1 = lines[0].Split().Select(s => long.Parse(s)).ToList();
			var line2 = lines[1].Split().Select(s => long.Parse(s)).ToList();

			var N = line1[0];
			var X = line1[1];
			var A = line1[2];
			var B = line1[3];

			var Ac = line2[0];
			var Bc = line2[1];
			var Ad = line2[2];
			var Bd = line2[3];

			var hashTable = new HashTable();
			var result = new List<string>();

			for (int i = 0; i < N; i++)
			{
				if (hashTable.Check(X))
				{
					A = (A + Ac) % 1000;
					B = (B + Bc) % 1000000000000000;
				}
				else
				{
					hashTable.Add(X);
					A = (A + Ad) % 1000;
					B = (B + Bd) % 1000000000000000;
				}

				X = (X * A + B) % 1000000000000000;
			}

			File.WriteAllText("output.txt", $"{X} {A} {B}");
		}
	}
}

