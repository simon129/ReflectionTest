using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Gamesofa.Excel2ScriptObject
{
	public class Excel2ScriptObject
	{
		public static bool SHOW_CONVERT_DETAIL = false;

		public static T Create<T>(string file) where T : ScriptableObject
		{
			//T obj = ScriptableObject.CreateInstance<T>();
			T obj = Activator.CreateInstance<T>();

			List<ExcelSheetDate> fieldData = ReflectionUtilities.GetExcelSheetDate<T>();

			FieldInfo field;
			Type elementType;
			string sheetName;

			Debug.Log(string.Format("Class: {0} has the following fields", typeof(T)));
			foreach (var item in fieldData)
			{
				field = item.fieldInfo;
				elementType = item.elementType;
				sheetName = item.sheetName;

				Debug.Log(string.Format("Field: {0}, Type: {1}, sheet: {2}", field.Name, elementType, sheetName));

				IList list = GetElementsFromSheet(file, sheetName, elementType);

				if (field.FieldType.IsArray)
				{
					Array array = Array.CreateInstance(elementType, list.Count);
					list.CopyTo(array, 0);

					field.SetValue(obj, array);
				}
				else
				{
					field.SetValue(obj, list);
				}
			}

			//ReflectionUtilities.ShowAttribute<T>();

			//AssetDatabase.CreateAsset(obj, outputPath);
			//AssetDatabase.SaveAssets();
			return obj;
		}

		private static IList GetElementsFromSheet(string file, string sheetName, Type elementType)
		{
			IList list = ReflectionUtilities.CreateIList(elementType);

			List<ExcelColumnData> elementData = ReflectionUtilities.GetExcelColumnData(elementType);
			using (var fs = File.OpenRead(file))
			{
				HSSFWorkbook hssfworkbook = new HSSFWorkbook(fs);
				ISheet s = hssfworkbook.GetSheet(sheetName);

				int startingRow = 0;
				Dictionary<string, int> headers = null;

				const bool hasHeader = true;
				if (hasHeader)
				{
					startingRow = 1;
					headers = ExcelUtilities.GetHeader(s);
				}

				for (int i = startingRow; i <= s.LastRowNum; i++)
				{
					IRow row = s.GetRow(i);
					if (row == null)
					{
						Debug.LogWarning("row is null " + (i + 1));
						continue;
					}

					object element = Activator.CreateInstance(elementType);
					bool valid = true;

					foreach (ExcelColumnData d in elementData)
					{
						if (headers.ContainsKey(d.columnName))
						{
							int colIndex = headers[d.columnName];

							object value = ExcelUtilities.GetCell(row, colIndex);
							if (value == null)
							{
								if (d.IsPrimaryKey)
								{
									valid = false;
									break;
								}

								if (d.fieldInfo.FieldType.IsValueType)
									d.fieldInfo.SetValue(element, Activator.CreateInstance(d.fieldInfo.FieldType));
								else
									d.fieldInfo.SetValue(element, null);
							}
							else
							{
								try
								{
									object converted = Convert.ChangeType(value, d.fieldInfo.FieldType);
									d.fieldInfo.SetValue(element, converted);

									if (SHOW_CONVERT_DETAIL)
										Debug.Log(string.Format("{0}, cellType: {1}, value: \"{2}\"({3}), field name: {4}({5}), convert: {6}",
											ExcelUtilities.GetCellName(i, colIndex), row.GetCell(colIndex).CellType, value, value.GetType(), d.fieldInfo.Name, d.fieldInfo.FieldType, converted));
								}
								catch (Exception e)
								{
									Debug.LogError(string.Format("Error at {0}, cellType: {1}, trying to convert \"{2}\"({3}) to {4} on {5}.{6}, error: {7}",
											ExcelUtilities.GetCellName(i, colIndex), row.GetCell(colIndex).CellType, value, value.GetType(), d.fieldInfo.FieldType, elementType, d.fieldInfo.Name, e));
									throw e;
								}
							}
						}
					}

					if (valid)
						list.Add(element);
				}
			}

			return list;
		}
	}
}