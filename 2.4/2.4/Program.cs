using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._4
{
	class Program
	{
		static void Main(string[] args)
		{
			var start = DateTime.Now;

			var lines = File.ReadLines("input.txt").ToList();
			var l1 = lines[0].Split().Select(s => int.Parse(s)).ToList();
			var l2 = lines[1].Split().Select(s => int.Parse(s)).ToList();

			var n = l1[0];
			var kFirst = l1[1] - 1;
			var kLast = l1[2] - 1;
			var A = l2[0];
			var B = l2[1];
			var C = l2[2];

			var l = new List<int>(10);
			l.Add(l2[3]);
			l.Add(l2[4]);

			for (int i = 2; i < n; i++)
			{
				var newItem = A * l[i - 2] + B * l[i - 1] + C;
				l.Add(newItem);
			}

			var indexPlus = 0;

			Action a = () =>
			{
				var min = l.Min();
				var max = l.Max();

				int sm = 0;
				int lastIndex = 0;
				var firstIndex = 0;

				if (min == max)
				{
					return;
				}

				if (kFirst == 0 && kLast == 0)
				{
					l[0] = min;
					return;
				}

				if (kFirst == n - 1 && kLast == n - 1)
				{
					l[n - 1] = max;
					return;
				}

				bool orderedAsc = true;
				for (int i = 0; i < n - 1; i++)
				{
					if (l[i] > l[i + 1])
					{
						orderedAsc = false;
						break;
					}
				}

				if (orderedAsc)
				{
					return;
				}

				bool orderedDesc = true;
				for (int i = 0; i < n - 1; i++)
				{
					if (l[i] < l[i + 1])
					{
						orderedDesc = false;
						break;
					}
				}

				if (orderedDesc)
				{
					l.Reverse();
					return;
				}

				if (n > 1000 && false)
				{
					var splitFactor = Math.Min(n / 200, 15);
					var stepValue = ((long)max - (long)min) / (splitFactor * 1.0);

					Dictionary<int, List<int>> steps = new Dictionary<int, List<int>>();
					for (int i = 0; i < splitFactor + 1; i++)
					{
						steps[i] = new List<int>();
					}

					for (int i = 0; i < l.Count; i++)
					{
						for (int step = 0; step < splitFactor + 1; step++)
						{
							if ((l[i] >= min + stepValue * step) && (l[i] < min + stepValue * (step + 1)))
							{
								steps[step].Add(l[i]);
								break;
							}
						}
					}

					while (sm < kLast + 1)
					{
						sm += steps[lastIndex].Count;
						lastIndex++;
					}

					for (int step = 0; step < splitFactor; step++)
					{
						var lowSum = steps.Take(step + 1).Select(kvp => kvp.Value.Count).Sum();
						var hightSum = steps.Take(step + 2).Select(kvp => kvp.Value.Count).Sum();

						if ((kFirst > lowSum) && (kFirst <= hightSum))
						{
							firstIndex = step + 1;
							indexPlus = lowSum;
							break;
						}
					}

					l.Clear();
					for (int i = firstIndex; i < lastIndex; i++)
					{
						if (steps[i].Count == 1)
						{
							l.AddRange(steps[i]);
						}
						else if (steps[i].Count > 1)
						{
							Quicksort(steps[i], 0, steps[i].Count - 1);
							l.AddRange(steps[i]);
						}
					}

					//Quicksort(l, 0, l.Count - 1);
					return;
				}

				//Quicksort(l, 0, n - 1);
				Quicksort2(l, 0, n - 1, kFirst, kLast);
			};

			//a();
			Quicksort2(l, 0, n - 1, kFirst, kLast);

			using (StreamWriter sr = new StreamWriter("output.txt"))
			{
				for (int i = kFirst - indexPlus; i < kLast - indexPlus; i++)
				{
					sr.Write($"{l[i]} ");
				}

				sr.Write(l[kLast - indexPlus]);
			}

			var end = DateTime.Now;
			var t = end.Subtract(start).TotalMilliseconds;
		}

		public static void Quicksort(List<int> elements, int left, int right)
		{
			int i = left, j = right;
			var pivot = elements[(left + right) / 2];

			while (i <= j)
			{
				while (elements[i].CompareTo(pivot) < 0)
				{
					i++;
				}

				while (elements[j].CompareTo(pivot) > 0)
				{
					j--;
				}

				if (i <= j)
				{
					// Swap
					int tmp = elements[i];
					elements[i] = elements[j];
					elements[j] = tmp;

					i++;
					j--;
				}
			}

			// Recursive calls
			if (left < j)
			{
				Quicksort(elements, left, j);
			}

			if (i < right)
			{
				Quicksort(elements, i, right);
			}
		}

		public static void Quicksort2(List<int> elements, int left, int right, int kFirst, int kLast)
		{
			int i = left, j = right;
			var pivot = elements[(left + right) / 2];

			while (i <= j)
			{
				while (elements[i].CompareTo(pivot) < 0)
				{
					i++;
				}

				while (elements[j].CompareTo(pivot) > 0)
				{
					j--;
				}

				if (i <= j)
				{
					// Swap
					int tmp = elements[i];
					elements[i] = elements[j];
					elements[j] = tmp;

					i++;
					j--;
				}
			}

			// Recursive calls
			if (left < j && kFirst <= j)
			{
				Quicksort2(elements, left, j, kFirst, kLast);
			}

			if (i < right && i <= kLast)
			{
				Quicksort2(elements, i, right, kFirst, kLast);
			}
		}
	}
}