using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Gamesofa.Excel2ScriptObject
{
	public class ReflectionUtilities
	{
		public static List<ExcelSheetDate> GetExcelSheetData(Type t)
		{
			List<ExcelSheetDate> list = new List<ExcelSheetDate>();

			string sheetName;
			Type elementType;

			foreach (var field in t.GetFields())
			{
				if (TryGetExcelSheetAttribute(field, out sheetName))
				{
					if (TryGetElementType(t, field, out elementType))
					{
						list.Add(new ExcelSheetDate(field, sheetName, elementType));
					}
				}
			}
			return list;
		}

		public static List<ExcelSheetDate> GetExcelSheetDate<T>()
		{
			return GetExcelSheetData(typeof(T));
		}

		public static List<ExcelColumnData> GetExcelColumnData(Type t)
		{
			List<ExcelColumnData> list = new List<ExcelColumnData>();

			string columnName;
			bool isPrimaryKey;

			foreach (var field in t.GetFields())
			{
				if (TryGetExcelColumnAttribute(field, out columnName, out isPrimaryKey))
				{
					list.Add(new ExcelColumnData(field, columnName, isPrimaryKey));
				}
			}
			return list;
		}

		public static List<ExcelColumnData> GetExcelColumnData<T>()
		{
			return GetExcelColumnData(typeof(T));
		}

		public static bool TryGetExcelSheetAttribute(FieldInfo fieldInfo, out string sheetName)
		{
			ExcelSheet[] attributes = fieldInfo.GetCustomAttributes(typeof(ExcelSheet), true) as ExcelSheet[];
			if (attributes.Length > 0)
			{
				sheetName = attributes[0].Name;
				return true;
			}
			else
			{
				sheetName = null;
				return false;
			}
		}

		public static bool TryGetExcelColumnAttribute(FieldInfo fieldInfo, out string columnName, out bool isPrimaryKey)
		{
			ExcelColumn[] attributes = fieldInfo.GetCustomAttributes(typeof(ExcelColumn), true) as ExcelColumn[];
			if (attributes.Length > 0)
			{
				columnName = attributes[0].Name;
				isPrimaryKey = attributes[0].IsPrimaryKey;
				return true;
			}
			else
			{
				columnName = null;
				isPrimaryKey = false;
				return false;
			}
		}

		public static bool TryGetElementType(Type type, FieldInfo fieldInfo, out Type elementType, bool debug = false)
		{
			Type fieldType = fieldInfo.FieldType;

			if (fieldType.IsArray)
			{
				elementType = fieldType.GetElementType();
				if (debug)
					Debug.Log(string.Format("{0}.{1} is array of <b>{2}</b>", type, fieldInfo.Name, elementType));
				return true;
			}
			else if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(List<>))
			{
				elementType = fieldType.GetGenericArguments()[0];
				if (debug)
					Debug.Log(string.Format("{0}.{1} is List<> of <b>{2}</b>", type, fieldInfo.Name, elementType));
				return true;
			}
			else
			{
				elementType = null;
				if (debug)
					Debug.LogWarning(string.Format("{0}.{1} is not array or List<>", type, fieldInfo.Name));
				return false;
			}
		}

		public static IList CreateIList(Type elementType)
		{
			Type listType = typeof(List<>).MakeGenericType(elementType);
			IList list = (IList)Activator.CreateInstance(listType);
			return list;
		}

		public static void ShowAttribute<T>() where T : ScriptableObject
		{
			foreach (var field in typeof(T).GetFields())
			{
				string sheetName;
				if (TryGetExcelSheetAttribute(field, out sheetName))
				{
					Debug.Log(string.Format("{0}.{1} read sheet <b><color=red>{1}</color></b>", typeof(T), field.Name, sheetName));

					Type elementType;
					if (TryGetElementType(typeof(T), field, out elementType))
					{
						foreach (var elementField in elementType.GetFields())
						{
							string colName;
							bool isPrimaryKey;
							if (TryGetExcelColumnAttribute(elementField, out colName, out isPrimaryKey))
							{
								Debug.Log(string.Format("{0}.{1} read column <b><color=green>{2}</color></b>", elementType, elementField.Name, colName));
							}
						}
					}
					else
					{
						continue;
					}
				}
			}
		}
	}
}