using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			var start = DateTime.Now;

			var lines = File.ReadLines("input.txt").ToList();
			var A = lines[1].Split().Select(s => int.Parse(s)).ToList();
			var log = new List<string>();

			using (StreamWriter sr = new StreamWriter("output.txt"))
			{

				for (int i = 1; i < A.Count; i++)
				{
					var j = i;
					while ((j > 0) && (A[j - 1] > A[j]))
					{
						var buff = A[j - 1];
						A[j - 1] = A[j];
						A[j] = buff;
						//log.Add();
						sr.Write($"Swap elements at indices {j} and {j + 1}.\n");
						j = j - 1;
					}
				}

				//log.Add("No more swaps needed.\n");
				//log.Add(string.Join(" ", A));
				sr.Write("No more swaps needed.\n");
				sr.Write(string.Join(" ", A));
			}

			//File.WriteAllText("output.txt", string.Join("", log));
					   
			var end = DateTime.Now;
			var fff = end.Subtract(start).ToString();
		}
	}
}
