using System;
using System.Reflection;

namespace Gamesofa.Excel2ScriptObject
{
	public class ExcelSheetDate
	{
		public FieldInfo fieldInfo;
		public string sheetName;
		public Type elementType;

		public ExcelSheetDate(FieldInfo fieldInfo, string sheetName, Type elementType)
		{
			this.fieldInfo = fieldInfo;
			this.sheetName = sheetName;
			this.elementType = elementType;
		}
	}
}