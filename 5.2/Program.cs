using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5._2
{
	/*
	I A[1] – корень дерева
	I parent(i) = b i
	2 c – индекс родителя узла с индексом i
	I left(i) = 2i – индекс левого ребенка узла с индексом i
	I right(i) = 2i + 1 – индекс правого ребенка узла с индексом i

			I невозрастающая : A[parent(i)] > A[i] для любого i
	I неубывающая : A[parent(i)] 6 A[i] для любого i
	*/

	class Program
	{
		static void Main(string[] args)
		{
			var n = int.Parse(File.ReadLines("input.txt").First());
			var commands = File.ReadLines("input.txt").Skip(1).ToList();

			int buff = 0;
			var heap = new int[n * 2 + 1];
			var heap_max_index = 0;
			var numbersToRows = new int[n];
			var rowsCurrentIndices = new int[n];

			var result = new List<string>();

			for (var rowNumber = 0; rowNumber < commands.Count; rowNumber++)
			{
				var cmd = commands[rowNumber];

				if (cmd.StartsWith("A"))
				{
					var element_new = int.Parse(cmd.Substring(2));
					heap[++heap_max_index] = element_new;

					numbersToRows[heap_max_index] = rowNumber;
					rowsCurrentIndices[rowNumber] = heap_max_index;

					var i = heap_max_index;
					var parent_i = i / 2;

					while (i > 1 && heap[parent_i] > heap[i])
					{
						buff = heap[parent_i];
						heap[parent_i] = heap[i];
						heap[i] = buff;

						buff = numbersToRows[parent_i];
						numbersToRows[parent_i] = numbersToRows[i];
						numbersToRows[i] = buff;

						rowsCurrentIndices[numbersToRows[parent_i]] = parent_i;
						rowsCurrentIndices[numbersToRows[i]] = i;

						i = parent_i;
						parent_i = parent_i / 2;
					}
				}
				else if (cmd == "X")
				{
					if (heap_max_index < 1)
					{
						result.Add("*");
					}
					else
					{
						var min = heap[1];
						result.Add(min.ToString());

						heap[1] = heap[heap_max_index--];

						numbersToRows[1] = numbersToRows[heap_max_index + 1];
						rowsCurrentIndices[numbersToRows[1]] = 1;

						var cur_i = 1;
						var left_child_index = cur_i * 2;
						var right_child_index = cur_i * 2 + 1;

						int ind = 0;

						while (cur_i <= heap_max_index && (heap[cur_i] > heap[left_child_index] || heap[cur_i] > heap[right_child_index]))
						{
							if (right_child_index <= heap_max_index)
							{
								ind = heap[left_child_index] <= heap[right_child_index] ? left_child_index : right_child_index;

								buff = heap[ind];
								heap[ind] = heap[cur_i];
								heap[cur_i] = buff;

								buff = numbersToRows[ind];
								numbersToRows[ind] = numbersToRows[cur_i];
								numbersToRows[cur_i] = buff;

								rowsCurrentIndices[numbersToRows[ind]] = ind;
								rowsCurrentIndices[numbersToRows[cur_i]] = cur_i;

								cur_i = ind;
								left_child_index = cur_i * 2;
								right_child_index = cur_i * 2 + 1;
							}
							else if (left_child_index <= heap_max_index)
							{
								ind = left_child_index;

								buff = heap[ind];
								heap[ind] = heap[cur_i];
								heap[cur_i] = buff;

								buff = numbersToRows[ind];
								numbersToRows[ind] = numbersToRows[cur_i];
								numbersToRows[cur_i] = buff;

								rowsCurrentIndices[numbersToRows[ind]] = ind;
								rowsCurrentIndices[numbersToRows[cur_i]] = cur_i;

								cur_i = ind;
								left_child_index = cur_i * 2;
								right_child_index = cur_i * 2 + 1;
							}
							else
							{
								break;
							}
						}
					}
				}
				else if (cmd.StartsWith("D"))
				{
					var items = cmd.Substring(2).Split().Select(s => int.Parse(s.Trim())).ToList();
					var old_element_line_index = items[0] - 1;
					var element_new = items[1];

					var i = rowsCurrentIndices[old_element_line_index];

					heap[i] = element_new;

					var parent_i = i / 2;

					while (i > 1 && heap[parent_i] > heap[i])
					{
						buff = heap[parent_i];
						heap[parent_i] = heap[i];
						heap[i] = buff;

						buff = numbersToRows[parent_i];
						numbersToRows[parent_i] = numbersToRows[i];
						numbersToRows[i] = buff;

						rowsCurrentIndices[numbersToRows[parent_i]] = parent_i;
						rowsCurrentIndices[numbersToRows[i]] = i;

						i = parent_i;
						parent_i = parent_i / 2;
					}
				}
			}

			File.WriteAllLines("output.txt", result);
		}

		static bool ChecHeap(int[] a, int n)
		{
			for (int i = 1; i <= n; i++)
			{
				if (2 * i <= n)
				{
					if (a[i - 1] > a[2 * i - 1])
					{
						return false;
					}
				}

				if (2 * i + 1 <= n)
				{
					if (a[i - 1] > a[2 * i])
					{
						return false;
					}
				}
			}

			return true;
		}

		static void generator1()
		{
			var cnt = 500000;
			var rnd = new Random();

			var result = new List<string>();

			result.Add((cnt * 2).ToString());

			var items = new int[cnt];

			for (int i = 0; i < cnt; i++)
			{
				result.Add("A " + rnd.Next(0, 1000000000).ToString());
			}

			for (int i = 0; i < cnt; i++)
			{
				result.Add("X");
			}

			File.WriteAllLines("input.txt", result);
		}

		static void generator2()
		{
			var cnt = 333333;
			var rnd = new Random();

			var result = new List<string>();

			result.Add((cnt * 3 + 1).ToString());

			var items = new int[cnt];

			for (int i = 0; i < cnt; i++)
			{
				items[i] = rnd.Next(0, 1000000000);
			}

			for (int i = 0; i < cnt; i++)
			{
				result.Add("A " + items[i].ToString());
			}

			for (int i = 0; i < cnt; i++)
			{
				result.Add($"D {i + 1} {items[i] - 100000000}");
			}

			for (int i = 0; i < cnt + 1; i++)
			{
				result.Add("X");
			}

			File.WriteAllLines("input.txt", result);
		}
	}
}