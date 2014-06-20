using System;
using System.Reflection;

namespace Gamesofa.Excel2ScriptObject
{
	public class ExcelColumnData
	{
		public FieldInfo fieldInfo;
		public string columnName;

		/// <summary>
		/// PrimaryKey only works on reference type value
		/// (dont work on int, I cant tell its zero or null)
		/// </summary>
		public bool IsPrimaryKey;

		public ExcelColumnData(FieldInfo fieldInfo, string columnName, bool isPrimaryKey = false)
		{
			this.fieldInfo = fieldInfo;
			this.columnName = columnName;
			this.IsPrimaryKey = isPrimaryKey;
		}
	}
}