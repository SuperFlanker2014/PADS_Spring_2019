using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._5
{
	class Program
	{
		static void Main(string[] args)
		{
			var commands = File.ReadLines("input.txt").ToList();

			var queue = new ushort[100000];
			var queue_head = -1;
			var queue_tail = 0;

			var registers = new ushort[26];

			var result = new StringBuilder();
			var cr = Convert.ToChar(10);

			var labels = new Dictionary<string, int>();
			for (int cmd_index = 0; cmd_index < commands.Count; cmd_index++)
			{
				var cmd = commands[cmd_index];
				if (cmd.StartsWith(":"))
				{
					labels[cmd.Substring(1)] = cmd_index;
				}
			}

			for (int cmd_index = 0; cmd_index < commands.Count; cmd_index++)
			{
				var cmd = commands[cmd_index];

				if (cmd == "+")
				{
					var a = queue[queue_tail++];
					var b = queue[queue_tail++];

					queue[++queue_head] = (ushort)(a + b);
				}
				else if (cmd == "-")
				{
					var a = queue[queue_tail++];
					var b = queue[queue_tail++];

					queue[++queue_head] = (ushort)(a - b);
				}
				else if (cmd == "*")
				{
					var a = queue[queue_tail++];
					var b = queue[queue_tail++];

					queue[++queue_head] = (ushort)(a * b);
				}
				else if (cmd == "/")
				{
					var a = queue[queue_tail++];
					var b = queue[queue_tail++];

					queue[++queue_head] = b == 0 ? (ushort)0 : (ushort)(a / b);
				}
				else if (cmd == "%")
				{
					var a = queue[queue_tail++];
					var b = queue[queue_tail++];

					queue[++queue_head] = b == 0 ? (ushort)0 : (ushort)(a % b);
				}
				else if (cmd.StartsWith(">"))
				{
					var reg_index = Convert.ToByte(cmd[1]) - 97;

					registers[reg_index] = queue[queue_tail++];
				}
				else if (cmd.StartsWith("<"))
				{
					var reg_index = Convert.ToByte(cmd[1]) - 97;

					queue[++queue_head] = registers[reg_index];
				}
				else if (cmd == "P")
				{
					var a = queue[queue_tail++];
					result.Append(a);
					result.Append(cr);
				}
				else if (cmd.StartsWith("P"))
				{
					var reg_index = Convert.ToByte(cmd[1]) - 97;
					result.Append(registers[reg_index]);
					result.Append(cr);
				}
				else if (cmd == "C")
				{
					var a = (byte)(queue[queue_tail++] % 256);
					result.Append(Convert.ToChar(a));
				}
				else if (cmd.StartsWith("C"))
				{
					var reg_index = Convert.ToByte(cmd[1]) - 97;
					result.Append(Convert.ToChar(registers[reg_index] % 256));
				}
				else if (cmd.StartsWith(":"))
				{
					continue;
				}
				else if (cmd.StartsWith("J"))
				{
					cmd_index = labels[cmd.Substring(1)];
				}
				else if (cmd.StartsWith("Z"))
				{
					var reg_index = Convert.ToByte(cmd[1]) - 97;
					if (registers[reg_index] == 0)
					{
						cmd_index = labels[cmd.Substring(2)];
					}
				}
				else if (cmd.StartsWith("E"))
				{
					var reg_index1 = Convert.ToByte(cmd[1]) - 97;
					var reg_index2 = Convert.ToByte(cmd[2]) - 97;
					if (registers[reg_index1] == registers[reg_index2])
					{
						cmd_index = labels[cmd.Substring(3)];
					}
				}
				else if (cmd.StartsWith("G"))
				{
					var reg_index1 = Convert.ToByte(cmd[1]) - 97;
					var reg_index2 = Convert.ToByte(cmd[2]) - 97;
					if (registers[reg_index1] > registers[reg_index2])
					{
						cmd_index = labels[cmd.Substring(3)];
					}
				}
				else if (cmd == "Q")
				{
					break;
				}
				else
				{
					queue[++queue_head] = ushort.Parse(cmd);
				}
			}

			File.WriteAllText("output.txt", result.ToString());
		}
	}
}