using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _4._1
{
	class Program
	{
		static void Main(string[] args)
		{
			var M = int.Parse(File.ReadLines("input.txt").First());

			var stack = new int[M];
			var stack_head = -1;

			var output = new List<string>();

			foreach (var line in File.ReadLines("input.txt").Skip(1))
			{
				if (line.StartsWith("+"))
				{
					stack[++stack_head] = int.Parse(line.Substring(2));
				}
				else
				{
					output.Add(stack[stack_head--].ToString());
				}
			}

			File.WriteAllLines("output.txt", output);
		}
	}
}