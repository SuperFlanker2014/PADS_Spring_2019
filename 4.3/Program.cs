using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _4._3
{
	class Program
	{
		static void Main(string[] args)
		{
			var N = int.Parse(File.ReadLines("input.txt").First());

			var output = new List<string>();

			foreach (var line in File.ReadLines("input.txt").Skip(1))
			{
				var stack = new char[line.Length];
				var stack_head = -1;
				var result = "YES";

				foreach (var c in line)
				{
					if (c == '(' || c == '[')
					{
						stack[++stack_head] = c;
					}
					else if (c == ')')
					{
						if (stack_head < 0 || stack[stack_head--] != '(')
						{
							result = "NO";
							break;
						}
					}
					else if (c == ']')
					{
						if (stack_head < 0 || stack[stack_head--] != '[')
						{
							result = "NO";
							break;
						}
					}
				}

				if (stack_head > -1)
				{
					result = "NO";
				}

				output.Add(result);
			}

			File.WriteAllLines("output.txt", output);
		}
	}
}