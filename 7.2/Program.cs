using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._2
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

			CheckBalance(nodes, n);

			var root = nodes.Skip(1).FirstOrDefault(nd => nd.parent == null);

			if (root != null)
			{
				if (root?.right.balance == -1)
				{
					var A = root;
					var B = A.right;
					var C = B.left;
					var X = C?.left;
					var Y = C?.right;

					C.parent = null;
					C.left = A;
					C.right = B;
					A.parent = C;
					A.right = X;
					B.parent = C;
					B.left = Y;
				}
				else
				{
					var A = root;
					var B = A.right;
					var Y = B?.left;

					B.parent = null;
					B.left = A;
					A.parent = B;
					A.right = Y;
				}
			}

			var index = 1;

			var items = nodes.Skip(1).Where(nd => nd.parent == null).ToList();

			while (items.Any())
			{
				foreach (var item in items)
				{
					item.id = index++;
				}

				items = items
					.Where(nd => nd.left != null).Select(nd => nd.left)
					.Union(items.Where(nd => nd.right != null).Select(nd => nd.right))
					.ToList();
			}

			var nodes2 = new Node[n + 1];
			for (var i = 1; i <= n; i++)
			{
				nodes2[nodes[i].id] = nodes[i];
			}

			var result = new List<string>();
			result.Add(n.ToString());

			for (var i = 1; i <= n; i++)
			{
				var l = nodes2[i].left != null ? nodes2[i].left.id : 0;
				var r = nodes2[i].right != null ? nodes2[i].right.id : 0;
				result.Add($"{nodes2[i].key} {l} {r}");
			}

			//CheckBalance(nodes, n);

			File.WriteAllLines("output.txt", result);
		}

		public static void CheckBalance(Node[] nodes, int n)
		{
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
		}
	}
}
