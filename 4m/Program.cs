using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _4m
{
	class Program
	{
		static void Main(string[] args)
		{
			var M = int.Parse(File.ReadLines("input.txt").First());
			var elements = File.ReadLines("input.txt").Skip(1).First().Split();

			var multiplier = 150000;
			elements = string.Join(" ", Enumerable.Repeat(File.ReadLines("input.txt").Skip(1).First(), multiplier).ToArray()).Split();

			var stack = new int[M * multiplier];
			var stack_head = -1;

			foreach (var item in elements)
			{
				if (item.StartsWith("+"))
				{
					var b = stack[stack_head--];
					var a = stack[stack_head--];

					stack[++stack_head] = a + b;
				}
				else if (item.StartsWith("-"))
				{
					var b = stack[stack_head--];
					var a = stack[stack_head--];

					stack[++stack_head] = a - b;
				}
				else if (item.StartsWith("*"))
				{
					var b = stack[stack_head--];
					var a = stack[stack_head--];

					stack[++stack_head] = a * b;
				}
				else
				{
					stack[++stack_head] = int.Parse(item);
				}
			}

			File.WriteAllText("output.txt", stack[stack_head].ToString());
		}
	}
}