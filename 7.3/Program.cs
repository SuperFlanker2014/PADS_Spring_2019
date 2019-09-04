using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._3
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

		public void CalcHeight()
		{
			this.height = Math.Max(this.left?.height ?? 0, this.right?.height ?? 0) + 1;
		}

		public void CalcBalance()
		{
			this.balance = (this.right?.height ?? 0) - (this.left?.height ?? 0);
		}

		public void Balance()
		{
			if (balance == 2)
			{
				LeftTurn();
			}
			if (balance == -2)
			{
				RightTurn();
			}
		}

		public void LeftTurn()
		{
			if (this?.right.balance == -1)
			{
				var A = this;
				var B = A.right;
				var C = B.left;
				var X = C?.left;
				var Y = C?.right;

				var flag_left = (A.parent != null) && (A.parent.left == A);
				C.parent = A.parent;
				if (C.parent != null)
				{
					if (flag_left)
					{
						C.parent.left = C;
					}
					else
					{
						C.parent.right = C;
					}
				}

				C.left = A;
				C.right = B;
				A.parent = C;
				A.right = X;
				B.parent = C;
				B.left = Y;

				A.CalcHeight();
				A.CalcBalance();
				B.CalcHeight();
				B.CalcBalance();
				C.CalcHeight();
				C.CalcBalance();
			}
			else
			{
				var A = this;
				var B = A.right;
				var Y = B?.left;

				var flag_left = (A.parent != null) && (A.parent.left == A);
				B.parent = A.parent;
				if (B.parent != null)
				{
					if (flag_left)
					{
						B.parent.left = B;
					}
					else
					{
						B.parent.right = B;
					}
				}
				B.left = A;
				A.parent = B;
				A.right = Y;

				A.CalcHeight();
				A.CalcBalance();
				B.CalcHeight();
				B.CalcBalance();
			}
		}

		public void RightTurn()
		{
			if (this?.left.balance == 1)
			{
				var A = this;
				var B = A.left;
				var C = B.right;
				var X = C?.right;
				var Y = C?.left;

				var flag_left = (A.parent != null) && (A.parent.left == A);
				C.parent = A.parent;
				if (C.parent != null)
				{
					if (flag_left)
					{
						C.parent.left = C;
					}
					else
					{
						C.parent.right = C;
					}
				}
				C.right = A;
				C.left = B;
				A.parent = C;
				A.left = X;
				B.parent = C;
				B.right = Y;

				A.CalcHeight();
				A.CalcBalance();
				B.CalcHeight();
				B.CalcBalance();
				C.CalcHeight();
				C.CalcBalance();
			}
			else
			{
				var A = this;
				var B = A.left;
				var Y = B?.right;

				var flag_left = (A.parent != null) && (A.parent.left == A);
				B.parent = A.parent;
				if (B.parent != null)
				{
					if (flag_left)
					{
						B.parent.left = B;
					}
					else
					{
						B.parent.right = B;
					}
				}
				B.right = A;
				A.parent = B;
				A.left = Y;

				A.CalcHeight();
				A.CalcBalance();
				B.CalcHeight();
				B.CalcBalance();
			}
		}

		public Node InsertKey(int new_key)
		{
			var node = this;

			Node result;

			do
			{
				if (new_key < node.key)
				{
					if (node.left != null)
					{
						node = node.left;
					}
					else
					{
						node.left = new Node() { key = new_key, parent = node };
						result =  node.left;
						break;
					}
				}
				else if (new_key > node.key)
				{
					if (node.right != null)
					{
						node = node.right;
					}
					else
					{
						
						node.right = new Node() { key = new_key, parent = node };
						result = node.right;
						break;
					}
				}
			}
			while (true);

			node = result;

			do
			{
				node.height = Math.Max(node.left?.height ?? 0, node.right?.height ?? 0) + 1;
				node.balance = (node.right?.height ?? 0) - (node.left?.height ?? 0);
				node.Balance();
				node = node.parent;
			}
			while (node != null);

			return result;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var lines = File.ReadLines("input.txt").ToList();
			var n = int.Parse(lines[0]);
			var nodes = LoadNodes(lines, n);
			var new_key = int.Parse(lines.Last());

			CheckBalance(nodes, n);

			var node = nodes.FirstOrDefault(nd => nd != null && nd.parent == null);

			if (node == null)
			{
				nodes[n + 1] = new Node() { key = new_key, id = n + 1 };
			}
			else
			{
				nodes[n + 1] = node.InsertKey(new_key);
				nodes[n + 1].id = n + 1;
			}

			//formatting

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

			var nodes2 = new Node[n + 2];
			for (var i = 1; i <= n + 1; i++)
			{
				nodes2[nodes[i].id] = nodes[i];
			}

			var result = new List<string>();
			result.Add((n + 1).ToString());

			for (var i = 1; i <= n + 1; i++)
			{
				var l = nodes2[i].left != null ? nodes2[i].left.id : 0;
				var r = nodes2[i].right != null ? nodes2[i].right.id : 0;
				result.Add($"{nodes2[i].key} {l} {r}");
			}

			File.WriteAllLines("output.txt", result);
		}

		public static Node[] LoadNodes(List<string> lines, int n)
		{
			var arr = new int[n + 1, 3];

			for (var i = 1; i <= n; i++)
			{
				var nums = lines[i].Split().Select(s => int.Parse(s)).ToList();
				arr[i, 0] = nums[0];
				arr[i, 1] = nums[1];
				arr[i, 2] = nums[2];
			}

			Node[] nodes = new Node[n + 1 + 1];

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

			return nodes;
		}

		public static void CheckBalance(Node[] nodes, int n)
		{
			var items = nodes.Where(nd => nd != null && nd.left == null && nd.right == null).ToList();
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