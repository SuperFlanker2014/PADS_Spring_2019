using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _4._4
{
	class Program
	{
		static void Main(string[] args)
		{
			var M = int.Parse(File.ReadLines("input.txt").First());

			var queue = new int[M];
			var queue_mins = new int[M];
			for (int i = 0; i < M; i++)
			{
				queue_mins[i] = int.MaxValue;
			}

			var queue_head = -1;
			var queue_tail = 0;

			var queue_min_index_head = -1;
			var queue_divider_index = -1;

			var output = new List<string>();

			foreach (var line in File.ReadLines("input.txt").Skip(1))
			{
				if (line.StartsWith("+"))
				{
					queue[++queue_head] = int.Parse(line.Substring(2));

					if ((queue_min_index_head == -1) || (queue[queue_head] < queue[queue_min_index_head]))
					{
						queue_min_index_head = queue_head;
					}
				}
				else if (line.StartsWith("-"))
				{
					if (queue_divider_index < queue_tail)
					{
						queue_divider_index = queue_head;
						queue_min_index_head = -1;

						for (int i = queue_divider_index; i >= queue_tail; i--)
						{
							queue_mins[i] = queue[i] < queue_mins[i + 1] ? queue[i] : queue_mins[i + 1];
						}
					}

					queue_tail++;
				}
				else if (line.StartsWith("?"))
				{
					output.Add(Math.Min(queue_min_index_head == -1 ? int.MaxValue : queue[queue_min_index_head], queue_mins[queue_tail]).ToString());
				}
			}

			File.WriteAllLines("output.txt", output);
		}
	}
}