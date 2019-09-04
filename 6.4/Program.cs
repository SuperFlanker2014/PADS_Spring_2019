using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6._4
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

		static Node search(Node x, int k)
		{
			if (x == null || k == x.key)
				return x;
			if (k < x.key)
				return search(x.left, k);
			else
				return search(x.right, k);
		}

		static Node minimum(Node x)
		{
			if (x.left == null)
				return x;
			return minimum(x.left);
		}

		static Node next(Node x)
		{
			if (x.right != null)
				return minimum(x.right);

			var y = x.parent;

			while (y != null && x == y.right)
			{
				x = y;
				y = y.parent;
			}

			return y;
		}

		static Node insert(Node x, Node z)
		{
			if (x == null)
				return z;

			if (z.key < x.key)
			{
				x.left = insert(x.left, z);
				return x.left;
			}
			else
			{
				x.right = insert(x.right, z);
				return x.right;
			}
		}

		static Node remove1(Node z)
		{
			if (z.left == null)
				return z.right;
			if (z.right == null)
				return z.left;
			return null;
		}

		static Node remove2(Node z)
		{
			var y = minimum(z.right);
			z.right = remove(z.right, y);
			y.right = z.right;
			y.left = z.left;
			return y;
		}

		static Node remove(Node x, Node z)
		{
			if (z.key < x.key)
			{
				x.left = remove(x.left, z);
				return x;
			}
			else if (z.key > x.key)
			{
				x.right = remove(x.right, z);
				return x;
			}
			else
			{
				if (x.left == null || x.right == null)
					return remove1(x);
				else
					return remove2(x);
			}
		}
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

			var m = int.Parse(lines[n + 1]);

			var queries = lines[n + 2].Split().Select(s => int.Parse(s)).ToList();

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
					nodes[arr[i, 1]].parent = nodes[i];
				}

				nodes[i].right = nodes[arr[i, 2]];

				if (nodes[i].right != null)
				{
					nodes[arr[i, 2]].parent = nodes[i];
				}
			}

			var result = new List<int>();

			var baseCount = nodes.Count(node => node != null);

			foreach (var query in queries)
			{
				Node nodeToDelete = null;
				var root = nodes.Skip(1).FirstOrDefault(node => node.parent == null);

				while (root != null)
				{
					if (query > root.key)
					{
						root = root.right;
					}
					else if (query < root.key)
					{
						root = root.left;
					}
					else //query == root.key
					{
						nodeToDelete = root;
						break;
					}
				}

				if (nodeToDelete != null)
				{
					if (nodeToDelete.parent != null)
					{
						if (nodeToDelete.parent.left == nodeToDelete)
						{
							nodeToDelete.parent.left = null;
						}
						else
						{
							nodeToDelete.parent.right = null;
						}
					}

					baseCount -= GetTreeNodesCount(nodeToDelete);
				}

				result.Add(baseCount);
			}

			File.WriteAllLines("output.txt", result.Select(i => i.ToString()));
		}

		public static int GetTreeNodesCount(Node x)
		{
			var leftList = x.left != null ? GetTreeNodesCount(x.left) : 0;
			var rightList = x.right != null ? GetTreeNodesCount(x.right) : 0;

			var result = 1 + leftList + rightList;

			return result;
		}

		public static void generator()
		{
			var l = 300000;

			var lines = new List<string>();

			lines.Add(l.ToString());

			var cnt = 2;
			for (int i = l; i > 0; i--)
			{
				if (i == 1)
				{
					lines.Add($"{i} 0 0");
				}
				else
				{
					lines.Add($"{i} 0 {cnt++}");
				}
			}

			File.WriteAllLines("input.txt", lines);
		}
	}
}