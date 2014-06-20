using System;
using NPOI.SS.UserModel;
using System.Collections.Generic;

namespace Gamesofa.Excel2ScriptObject
{
	public class ExcelUtilities
	{
		public static Dictionary<string, int> GetHeader(ISheet s)
		{
			Dictionary<string, int> headers = new Dictionary<string, int>();

			IRow row = s.GetRow(0);
			for (int i = 0; i <= row.LastCellNum; i++)
			{
				object header = GetCell(row, i);
				if (header != null && (header.GetType() == typeof(string) && (string)header != string.Empty))
				{
					var key = header.ToString();
					if (headers.ContainsKey(key))
					{
						Debug.LogWarning(string.Format("Duplicate header at {0}.{1}: \"{2}\"", s.SheetName, GetCellName(row.RowNum, i), key));
					}
					else
					{
						headers.Add(key, i);
					}
				}
			}

			return headers;
		}

		public static string GetCellName(int row, int cell)
		{
			int dividend = cell + 1;
			string columnName = string.Empty;
			int modulo;

			while (dividend > 0)
			{
				modulo = (dividend - 1) % 26;
				columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
				dividend = (int)((dividend - modulo) / 26);
			}

			return columnName + (++row).ToString();
		}

		public static object GetCell(IRow row, int j)
		{
			var cell = row.GetCell(j);

			if (cell == null)
				return null;

			switch (cell.CellType)
			{
				case CellType.Blank:
					return null;

				case CellType.Boolean:
					return cell.BooleanCellValue;

				case CellType.Error:
					throw new NotImplementedException("CellType.Error at (" + row.RowNum + ", " + j + ")");

				case CellType.Formula:
					return cell.NumericCellValue;

				case CellType.Numeric:
					return cell.NumericCellValue;

				case CellType.String:
					return cell.StringCellValue;

				case CellType.Unknown:
					throw new NotImplementedException("CellType.Unknown at (" + row.RowNum + ", " + j + ")");

				default:
					throw new NotImplementedException("No CellType found at (" + row.RowNum + ", " + j + ")");
			}
		}
	}
}