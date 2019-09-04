using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6._1
{
	class Program
	{
		static void Main(string[] args)
		{
			var n = int.Parse(File.ReadLines("input.txt").First());
			var items = File.ReadLines("input.txt").Skip(1).First().Split().Select(s => int.Parse(s)).ToList();
			var m = int.Parse(File.ReadLines("input.txt").Skip(2).First());
			var queries = File.ReadLines("input.txt").Skip(3).First().Split().Select(s => int.Parse(s)).ToList();

			var result = new List<string>();

			foreach (var query in queries)
			{
				var top = n;
				var bottom = -1;

				int ind = -1;

				while (top > bottom + 1)
				{
					ind = (top + bottom) / 2;

					if (items[ind] < query)
					{
						bottom = ind;
					}
					else
					{
						top = ind;
					}
				}

				if (top == n || items[top] != query)
				{
					result.Add("-1 -1");
					continue;
				}

				var middle = top;

				top = n;
				bottom = middle - 1;

				while (top > bottom + 1)
				{
					ind = (top + bottom) / 2;

					if (items[ind] == query)
					{
						bottom = ind;
					}
					else
					{
						top = ind;
					}
				}

				int ind_top = top - 1;

				top = middle + 1;
				bottom = -1;

				while (top > bottom + 1)
				{
					ind = (top + bottom) / 2;

					if (items[ind] == query)
					{
						top = ind; 
					}
					else
					{
						bottom = ind;
					}
				}

				int ind_bottom = bottom + 1;

				//if (items[n - 1] == query)
				//{
				//	ind_top = n - 1;
				//}
				//else
				//{
				//	top = n - 1;
				//	bottom = ind;
				//	ind = (top + bottom) / 2;

				//	while (!((items[ind] != query) && (items[ind - 1] == query)))
				//	{
				//		if (items[ind] >= query)
				//		{
				//			top = ind;
				//		}
				//		else
				//		{
				//			bottom = ind;
				//		}

				//		ind = (top + bottom) / 2;
				//	}

				//	ind_top = ind - 1;
				//}

				//int ind_bottom;

				//if (items[0] == query)
				//{
				//	ind_bottom = 0;
				//}
				//else
				//{
				//	top = middle;
				//	bottom = 0;
				//	ind = (top + bottom) / 2;

				//	while (!((items[ind] != query) && (items[ind + 1] == query)))
				//	{
				//		if (items[ind] >= query)
				//		{
				//			top = ind;
				//		}
				//		else
				//		{
				//			bottom = ind;
				//		}

				//		ind = (top + bottom) / 2;
				//	}

				//	ind_bottom = ind + 1;
				//}

				result.Add($"{ind_bottom + 1} {ind_top + 1}");
			}

			File.WriteAllLines("output.txt", result);
		}
	}
}
