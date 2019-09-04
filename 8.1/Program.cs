using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8._1
{
	public class HashTableItem
	{
		public long value;
		public HashTableItem nextItem;

		public override string ToString()
		{
			return value.ToString();
		}
	}

	public class HashTable
	{
		HashTableItem[] hashTableItems;
		int splitFactor = 2;
		int maxItemsCount;
		int n;

		public HashTable(int maxItemsCount)
		{
			this.maxItemsCount = maxItemsCount;
			n = Math.Max(maxItemsCount / splitFactor, 2);
			hashTableItems = new HashTableItem[n];
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

		private int h(long k)
		{
			return (int)(k % n);
		}

		public void Add(long value)
		{
			var newItem = new HashTableItem { value = value };
			var hash = h(k(value));

			if (hashTableItems[hash] == null)
			{
				hashTableItems[hash] = newItem;
			}
			else
			{
				var item = hashTableItems[hash];
				var found = false;

				while (item != null)
				{
					if (item.value == value)
					{
						found = true;
						break;
					}

					item = item.nextItem;
				}

				if (!found)
				{
					newItem.nextItem = hashTableItems[hash];
					hashTableItems[hash] = newItem;
				}
			}
		}

		public void Delete(long value)
		{
			var hash = h(k(value));

			HashTableItem prevItem = null;
			var item = hashTableItems[hash];

			while (item != null)
			{
				if (item.value == value)
				{
					break;
				}

				prevItem = item;
				item = item.nextItem;
			}

			if (prevItem == null)
			{
				if (item != null)
				{
					hashTableItems[hash] = item.nextItem;
				}
			}
			else
			{
				prevItem.nextItem = item?.nextItem;
			}
		}

		public bool Check(long value)
		{
			var hash = h(k(value));

			var item = hashTableItems[hash];

			while (item != null)
			{
				if (item.value == value)
				{
					return true;
				}

				item = item.nextItem;
			}

			return false;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			//generate();

			var lines = File.ReadLines("input.txt").ToList();
			var n = int.Parse(lines[0]);

			var hashTable = new HashTable(n);
			var result = new List<string>();

			var commands = lines.Skip(1).Select(l => l.Split()).ToList();

			foreach (var item in commands)
			{
				var cmd = item[0];
				var number = long.Parse(item[1]);

				switch (cmd)
				{
					case "A":
						hashTable.Add(number);
						break;
					case "D":
						hashTable.Delete(number);
						break;
					case "?":
						result.Add(hashTable.Check(number) ? "Y" : "N");
						break;
				}
			}

			File.WriteAllLines("output.txt", result);
		}

		public static void generate()
		{
			var random = new Random();
			
			var n = 1000;

			var commands = new Char[] { 'A', 'D', '?' };
			var numbers = new long[] { long.MinValue + 1, long.MinValue + int.MaxValue, int.MaxValue, int.MinValue, long.MaxValue };

			var result = new List<string>();
			result.Add((n*3).ToString());

			for (int i = 0; i < n; i++)
			{
				var commandIndex = random.Next(0, commands.Length);
				var numberIndex = random.Next(0, numbers.Length);
				result.Add($"{commands[commandIndex]} {numbers[numberIndex]}");
			}

			File.WriteAllLines("input.txt", result);
		}
	}
}
