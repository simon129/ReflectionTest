using System;
using System.Collections.Generic;

public class DemoData : ScriptableObject
{
	[ExcelSheet("PlayerData")]
	public List<Character> Players;

	[ExcelSheet("EnemyData")]
	public List<Character> Enemies;

	[ExcelSheet("nullTest")]
	public List<Character> NullTest;

	[ExcelSheet("dup.Column Empty")]
	public List<Gun> Guns;
}

[System.Serializable]
public class Character
{
	[ExcelColumn("name")]
	public string Name;

	[ExcelColumn("hp")]
	public int Hp;

	[ExcelColumn("human")]
	public bool IsHuman;

	public override string ToString()
	{
		return string.Format("Name: {0}, Hp: {1}, IsHuman: {2}", Name, Hp, IsHuman);
	}
}

[System.Serializable]
public class Gun
{
	[ExcelColumn("id", true)]
	public int Id;

	[ExcelColumn("name")]
	public string Name;

	public override string ToString()
	{
		return string.Format("Id: {0}, Name: {1}", Id, Name);
	}
}