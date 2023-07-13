using Final_C;
using System;
namespace Final_C
{

	public class Device
	{
        public int Id { get; set; }
        public string Dname { get; set; }
        public int Quantity { get; set; }

        public Device()
        {
        }

        public Device(int id, string name, int quanity)
        {
            Id = id;
            Dname = name;
            Quantity = quanity;
        }

        public override string ToString()
        {
            return Id + " | " + Dname + " | " + " | " + Quantity + " | ";
        }
    }
}

