using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6._3
{
	class Node
	{
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
			//generator();

			var n = int.Parse(File.ReadLines("input.txt").First());

			var arr = new int[n + 1, 3];

			var i = 1;

			foreach (var line in File.ReadLines("input.txt").Skip(1))
			{
				var nums = line.Split().Select(s => int.Parse(s)).ToList();
				arr[i, 0] = nums[0];
				arr[i, 1] = nums[1];
				arr[i, 2] = nums[2];
				i++;
			}

			Node[] nodes = new Node[n + 1];

			for (i = 1; i < n + 1; i++)
			{
				nodes[i] = new Node { key = arr[i, 0] };
			}

			for (i = 1; i < n + 1; i++)
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

			//var root = nodes.Skip(1).FirstOrDefault(node => node.parent == null);
			//var h = height(root);

			var items = nodes.Skip(1).Where(node => node.left == null && node.right == null).ToList();
			var h = 0;
			while (items.Any())
			{
				items = items.Where(item => item.parent != null).Select(item => item.parent).Distinct().ToList();
				h++;
			}

			File.WriteAllText("output.txt", h.ToString());
		}

		public static int height(Node x)
		{
			if (x == null)
				return 0;

			int h1 = x.left != null ? height(x.left) : 0;
			int h2 = x.right != null ? height(x.right) : 0; ;

			return Math.Max(h1, h2) + 1;
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