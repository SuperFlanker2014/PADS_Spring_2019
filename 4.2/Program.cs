using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _4._2
{
	class Program
	{
		static void Main(string[] args)
		{
			var M = int.Parse(File.ReadLines("input.txt").First());

			var queue = new int[M];
			var queue_head = -1;
			var queue_tail = -1;

			var output = new List<string>();

			foreach (var line in File.ReadLines("input.txt").Skip(1))
			{
				if (line.StartsWith("+"))
				{
					queue[++queue_head] = int.Parse(line.Substring(2));
				}
				else
				{
					output.Add(queue[++queue_tail].ToString());
				}
			}

			File.WriteAllLines("output.txt", output);
		}
	}
}