using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

	public class Debug
	{
		internal static void Log(string s)
		{
			Console.WriteLine(s);
		}

		internal static void Log(params  object[] s)
		{
			string r = string.Empty;

			if (s != null)
			{
				for (int i = 0; i < s.Length; i++)
				{
					r += s[i].ToString() + ", ";
				}
			}
			Console.WriteLine(r);
		}

		internal static void LogWarning(string s)
		{
			Console.WriteLine(s);
		}

		internal static void LogError(string s)
		{
			Console.WriteLine(s);
		}
	}
