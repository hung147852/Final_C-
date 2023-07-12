using System;
namespace FinalC
{
	public abstract class BaseManager
	{
		public static String VERSION = "1.0";
        
        public BaseManager()
		{
			Console.WriteLine("Init BaseManager");
		}

		public abstract void AddNew();
        public abstract void Update();
        public abstract void Delete();
        public abstract void Find();
        public abstract void UserLogin();
        public abstract void ExportList();
        public abstract void Importdata();
    }
}

