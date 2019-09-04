using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._5
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
				if (X != null)
				{
					X.parent = A;
				}				
				B.parent = C;
				B.left = Y;				
				if (Y != null)
				{
					Y.parent = B;
				}

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
				if (Y != null)
				{
					Y.parent = A;
				}

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
				if (X != null)
				{
					X.parent = A;
				}
				B.parent = C;
				B.right = Y;
				if (Y != null)
				{
					Y.parent = B;
				}

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
				if (Y != null)
				{
					Y.parent = A;
				}

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
						result = node.left;
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
				else if (new_key == node.key)
				{
					result = null;
					break;
				}
			}
			while (true);

			if (result == null)
			{
				return this;
			}

			node = result;

			do
			{
				node.height = Math.Max(node.left?.height ?? 0, node.right?.height ?? 0) + 1;
				node.balance = (node.right?.height ?? 0) - (node.left?.height ?? 0);
				node.Balance();

				if (node.parent == null)
				{
					return node;
				}
				else
				{
					node = node.parent;
				}
			}
			while (true);
		}

		public Node DeleteKey(int key_to_delete)
		{
			var node = this;
			Node result = null;

			do
			{
				if (key_to_delete < node.key)
				{
					node = node.left;
				}
				else if (key_to_delete > node.key)
				{
					node = node.right;
				}
				else if (key_to_delete == node.key)
				{
					result = node;
					break;
				}
			}
			while (node != null);

			if (result == null)
			{
				return null;
			}

			if (result.left == null && result.right == null)
			{
				node = result.parent;

				if (node != null)
				{
					if (node.left == result)
					{
						node.left = null;
					}
					else
					{
						node.right = null;
					}
				}
			}
			else if (result.left == null && result.right != null)
			{
				node = result.parent;

				if (node != null)
				{
					if (node.left == result)
					{
						node.left = result.right;
						node.left.parent = node;
						node = node.left;
					}
					else
					{
						node.right = result.right;
						node.right.parent = node;
						node = node.right;
					}
				}
				else
				{
					node = result.right;
					result.right.parent = null;
				}
			}
			else
			{
				var R = result.left;

				while (R.right != null)
				{
					R = R.right;
				}

				if (R != result.left)
				{
					var exRparent = R.parent;
					result.key = R.key;

					if (R.left != null)
					{
						exRparent.right = R.left;
						R.left.parent = exRparent;
					}
					else
					{
						exRparent.right = null;
					}

					node = exRparent;
				}
				else
				{
					//правых нет, а левое дерево никуда же не делось
					var exRparent = R.parent;
					result.key = R.key;

					exRparent.left = R.left;

					if (R.left != null)
					{
						R.left.parent = exRparent;
					}

					node = exRparent;
				}

				result = R;
			}

			while (node != null)
			{
				node.height = Math.Max(node.left?.height ?? 0, node.right?.height ?? 0) + 1;
				node.balance = (node.right?.height ?? 0) - (node.left?.height ?? 0);
				node.Balance();

				if (node.parent == null)
				{
					return node;
				}
				else
				{
					node = node.parent;
				}
			}

			return node;
		}

		public Node Check(int key)
		{
			var node = this;

			do
			{
				if (key < node.key)
				{
					node = node.left;
				}
				else if (key > node.key)
				{
					node = node.right;
				}
				else if (key == node.key)
				{
					break;
				}
			}
			while (node != null);

			return node;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var lines = File.ReadLines("input.txt").ToList();
			var n = int.Parse(lines[0]);

			var commands = lines.Skip(1).Select(l => l.Split()).ToList();

			var result = new List<string>();

			Node root = null;

			var i = 1;

			foreach (var item in commands)
			{
				var cmd = item[0];
				var number = int.Parse(item[1]);

				switch (cmd)
				{
					case "A":
						if (root == null)
						{
							root = new Node { key = number };
						}
						else
						{
							root = root.InsertKey(number);
						}
						result.Add(root.balance.ToString());
						break;
					case "D":
						if (root == null)
						{
							result.Add(0.ToString());
						}
						else if (root != null && root.left == null && root.right == null && root.key == number)
						{
							root = null;
							result.Add(0.ToString());
						}
						else
						{
							var res = root.DeleteKey(number);

							if (res != null)
							{
								root = res;
							}

							result.Add(root.balance.ToString());
						}
						break;
					case "C":
						if (root == null)
						{
							result.Add("N");
						}
						else
						{
							result.Add(root.Check(number) == null ? "N" : "Y");
						}
						break;
				}

				i++;
			}

			File.WriteAllLines("output.txt", result);
		}
	}
}