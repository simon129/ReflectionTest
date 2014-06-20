[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
public class ExcelColumn : System.Attribute
{
	public string Name;
	public int Number;
	public bool IsPrimaryKey;
	public OrderType Type = OrderType.Number;

	public ExcelColumn(string name, bool IsPrimaryKey = false)
	{
		Name = name;
		Type = OrderType.Name;
		this.IsPrimaryKey = IsPrimaryKey;
	}

	//public ExcelColumn(int number)
	//{
	//    Number = number;
	//    Type = OrderType.Number;
	//}

	public enum OrderType
	{
		Name,
		Number,
	}
}