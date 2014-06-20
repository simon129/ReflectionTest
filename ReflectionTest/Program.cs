using System;

namespace Gamesofa.Excel2ScriptObject
{
	class Program
	{
		static void Main(string[] args)
		{
			//Excel2ScriptObject.Create<TankDataTable>("test.xls", "Assets/CameraLocationDatabase.asset", true);

			Tank();
			Test();
			
			Console.ReadKey();
		}

		static void Tank()
		{
			Debug.Log("/****************** Class detail ******************/");

			TankDataTable tankTable = Excel2ScriptObject.Create<TankDataTable>("test.xls");

			Debug.Log(string.Empty);

			Debug.Log("/*************** Generated instance ***************/");
			Debug.Log("Tanks");
			Debug.Log(string.Empty);

			foreach (var item in tankTable.Alltanks)
				Debug.Log(item.ToString());

			Debug.Log("================================");
			Debug.Log(string.Empty);
		}

		static void Test()
		{
			Debug.Log("/****************** Class detail ******************/");

			DemoData demoData = Excel2ScriptObject.Create<DemoData>("test.xls");

			Debug.Log(string.Empty);

			Debug.Log("/*************** Generated instance ***************/");
			Debug.Log("Players");
			Debug.Log(string.Empty);

			foreach (var item in demoData.Players)
				Debug.Log(item.ToString());

			Debug.Log(string.Empty);

			Debug.Log("Enemies");
			Debug.Log(string.Empty);

			foreach (var item in demoData.Enemies)
				Debug.Log(item.ToString());

			Debug.Log(string.Empty);

			Debug.Log("Null Test");
			Debug.Log(string.Empty);

			foreach (var item in demoData.NullTest)
				Debug.Log(item.ToString());

			Debug.Log(string.Empty);

			Debug.Log("Gun");
			Debug.Log(string.Empty);

			foreach (var item in demoData.Guns)
				Debug.Log(item.ToString());

			Debug.Log("================================");
			Debug.Log(string.Empty);
		}
	}
}