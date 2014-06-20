[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
public class ExcelSheet : System.Attribute
{
	public string Name;
	public int Number;
	public OrderType Type = OrderType.Number;

	public ExcelSheet(string name)
	{
		Name = name;
		Type = OrderType.Name;
	}

	//public ExcelSheet(int number)
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