using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8._2
{
	public class HashTableItem
	{
		public string key;
		public string value;
		public HashTableItem nextItem;

		public HashTableItem nextItemLinear;
		public HashTableItem prevItemLinear;

		public override string ToString()
		{
			return $"{key}: {value}";
		}
	}

	public class HashTable
	{
		HashTableItem lastAddedItem = null;

		HashTableItem[] hashTableItems;
		int splitFactor = 2;
		int maxItemsCount;
		int n;

		int t = 29;
		int[] g_indices = new int[20];

		public HashTable(int maxItemsCount)
		{
			this.maxItemsCount = maxItemsCount;
			n = Math.Max(maxItemsCount / splitFactor, 2);
			hashTableItems = new HashTableItem[n];

			var random = new Random();

			for (int i = 0; i < 20; i++)
			{
				while (true)
				{
					var new_coeff = random.Next(11, 1111);

					var isUnique = true;

					for (int j = 0; j < 20; j++)
					{
						if (new_coeff == g_indices[j])
						{
							isUnique = false;
							break;
						}
					}

					if (isUnique)
					{
						g_indices[i] = new_coeff;
						break;
					}
				}
			}
		}

		private int IntPow(int x, uint pow)
		{
			int result = 1;

			for (long i = 0; i < pow; i++)
			{
				result = (result * x) % n;
			}
			return result;
		}

		private string k(string key)
		{
			return key;
		}

		private int h(string k)
		{
			var bytes = Encoding.ASCII.GetBytes(k);

			var summ = 0;

			for (uint ind = 0; ind < bytes.Length; ind++)
			{
				summ += (bytes[ind] * g_indices[ind]) % n;
			}

			return summ % n;
		}

		private int h1(string k)
		{
			var bytes = Encoding.ASCII.GetBytes(k);

			var summ = 0;

			for (uint ind = 0; ind < bytes.Length; ind++)
			{
				summ += bytes[ind] * IntPow(t, ind);
			}

			return summ % n;
		}

		public void Put(string key, string value)
		{
			var newItem = new HashTableItem { key = key, value = value };
			var hash = h(k(key));

			var found = false;

			if (hashTableItems[hash] == null)
			{
				hashTableItems[hash] = newItem;
			}
			else
			{
				var item = hashTableItems[hash];

				while (item != null)
				{
					if (item.key == key)
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
				else
				{
					item.value = value;
				}
			}

			if (!found)
			{
				if (lastAddedItem != null)
				{
					lastAddedItem.nextItemLinear = newItem;
					newItem.prevItemLinear = lastAddedItem;
					lastAddedItem = newItem;
				}
				else
				{
					lastAddedItem = newItem;
				}
			}
		}

		public void Delete(string key)
		{
			var hash = h(k(key));

			HashTableItem prevItem = null;
			var item = hashTableItems[hash];

			while (item != null)
			{
				if (item.key == key)
				{
					break;
				}

				prevItem = item;
				item = item.nextItem;
			}

			if (item != null)
			{
				if (prevItem == null)
					hashTableItems[hash] = item.nextItem;
				else
					prevItem.nextItem = item.nextItem;

				if (item.prevItemLinear != null)
				{
					item.prevItemLinear.nextItemLinear = item.nextItemLinear;
				}
				if (item.nextItemLinear != null)
				{
					item.nextItemLinear.prevItemLinear = item.prevItemLinear;
				}

				//крайний правый элемент// item == lastAddedItem)
				if (item.nextItemLinear == null && item.prevItemLinear != null )
				{
					lastAddedItem = item.prevItemLinear;
					lastAddedItem.nextItemLinear = null;
				}

				//крайний левый элемент
				if (item.prevItemLinear == null && item.nextItemLinear != null)
				{
					item.nextItemLinear.prevItemLinear = null;
				}

				//единственный элемент
				if (item.prevItemLinear == null && item.nextItemLinear == null)
				{
					lastAddedItem = null;
				}

				item = null;
			}
		}

		public string Get(string key)
		{
			var hash = h(k(key));

			var item = hashTableItems[hash];

			while (item != null)
			{
				if (item.key == key)
				{
					return item.value;
				}

				item = item.nextItem;
			}

			return "<none>";
		}

		public string Prev(string key)
		{
			var hash = h(k(key));

			var item = hashTableItems[hash];

			while (item != null)
			{
				if (item.key == key)
				{
					return item.prevItemLinear != null ? item.prevItemLinear.value : "<none>";
				}

				item = item.nextItem;
			}

			return "<none>";
		}

		public string Next(string key)
		{
			var hash = h(k(key));

			var item = hashTableItems[hash];

			while (item != null)
			{
				if (item.key == key)
				{
					return item.nextItemLinear != null ? item.nextItemLinear.value : "<none>";
				}

				item = item.nextItem;
			}

			return "<none>";
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

			var commands = lines.Skip(1).ToList();

			foreach (var item in commands)
			{
				if (item.StartsWith("put"))
				{
					var l = item.Split();
					hashTable.Put(l[1], l[2]);
				}
				else if (item.StartsWith("delete"))
				{
					var l = item.Split();
					hashTable.Delete(l[1]);
				}
				else if (item.StartsWith("get"))
				{
					var l = item.Split();
					result.Add(hashTable.Get(l[1]));
				}
				else if (item.StartsWith("prev"))
				{
					var l = item.Split();
					result.Add(hashTable.Prev(l[1]));
				}
				else if (item.StartsWith("next"))
				{
					var l = item.Split();
					result.Add(hashTable.Next(l[1]));
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
			result.Add((n * 3).ToString());

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
