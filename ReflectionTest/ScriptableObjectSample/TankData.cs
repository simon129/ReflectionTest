using System;
using System.Collections.Generic;

public class TankDataTable : ScriptableObject
{
	[ExcelSheet("newTank")]
	public TankDataElement[] Alltanks;
}

[System.Serializable]
public class TankDataElement
{
	[ExcelColumn("id")]
	//[ExcelColumn(1)]
	public string Id;

	[ExcelColumn("armor")]
	public int Armor;

	[ExcelColumn("power")]
	public float Power;

	[ExcelColumn("reload")]
	public float Reload;

	[ExcelColumn("speed")]
	public float Speed;

	[ExcelColumn("grow")]
	public string Grow;

	[ExcelColumn("grow_cost")]
	public string GrowCost;

	public override string ToString()
	{
		return string.Format("Id: {0}, Armor: {1}, Power: {2}, Reload: {3}, Speed: {4}, Grow: {5}, GrowCost: {6}",
			Id, Armor, Power, Reload, Speed, Grow, GrowCost);
	}
}