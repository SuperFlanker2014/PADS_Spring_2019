using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._1
{
	class Node
	{
		public override string ToString()
		{
			return $"{key}";
		}

		public int id;
		public int key;
		public int height;
		public int balance;
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

			for (var i = 1; i <= n; i++)
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

			var items = nodes.Skip(1).Where(nd => nd.left == null && nd.right == null).ToList();
			var h = 1;

			while (items.Any())
			{
				items.ForEach(nd => nd.height = h);
				h++;

				items = items.Where(nd => nd.parent != null).Select(nd => nd.parent).Distinct().ToList();
			}

			for (var i = 1; i <= n; i++)
			{
				nodes[i].balance = 
					(nodes[i].right == null ? 0 : nodes[i].right.height) -
					(nodes[i].left == null ? 0 : nodes[i].left.height);
			}

			File.WriteAllLines("output.txt", nodes.Skip(1).Select(nd => nd.balance.ToString()));
		}
	}
}
