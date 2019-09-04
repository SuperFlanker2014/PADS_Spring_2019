using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6m
{
	class Node
	{
		public override string ToString()
		{
			return $"{key}";
		}

		public int id;
		public int key;
		public Node left;
		public Node right;
		public Node parent;
	}

	class Program
	{
		static void Main(string[] args)
		{
			var lines = File.ReadLines("input.txt").ToList();

			var n = int.Parse(lines[0]);

			var arr = new int[n + 1, 3];

			for (var i = 1; i <= n; i++)
			{
				var nums = lines[i].Split().Select(s => int.Parse(s)).ToList();
				arr[i, 0] = nums[0];
				arr[i, 1] = nums[1];
				arr[i, 2] = nums[2];
			}

			Node[] nodes = new Node[n + 1];

			for (var i = 1; i < n + 1; i++)
			{
				nodes[i] = new Node { id = i, key = arr[i, 0] };
			}

			for (var i = 1; i < n + 1; i++)
			{
				nodes[i].left = nodes[arr[i, 1]];

				if (nodes[i].left != null)
				{
					nodes[i].left.parent = nodes[i];
				}

				nodes[i].right = nodes[arr[i, 2]];

				if (nodes[i].right != null)
				{
					nodes[i].right.parent = nodes[i];
				}
			}

			var result = "YES";

			var root = nodes.Skip(1).FirstOrDefault(node => node.parent == null);

			if (root != null)
			{
				var stack = new int[n, 3];
				stack[0, 0] = root.id;
				stack[0, 1] = int.MinValue;
				stack[0, 2] = int.MaxValue;
				var stack_head = 0;

				do
				{
					var r = nodes[stack[stack_head, 0]];
					var r_left = stack[stack_head, 1];
					var r_right = stack[stack_head, 2];
					stack_head--;

					if (r.key <= r_left || r.key >= r_right)
					{
						result = "NO";
						break;
					}

					do
					{
						if (r.key <= r_left || r.key >= r_right)
						{
							result = "NO";
							break;
						}

						if (r.right != null)
						{
							stack[++stack_head, 0] = r.right.id;
							stack[stack_head, 1] = r.key;
							stack[stack_head, 2] = r_right;
						}

						r_right = r.key;
						r = r.left;

					} while (r != null);
				} while (stack_head > -1);
			}

			File.WriteAllText("output.txt", result);
		}
	}
}
