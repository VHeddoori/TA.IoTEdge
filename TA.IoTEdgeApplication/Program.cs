using System;

namespace TA.IoTEdgeApplication
{
    enum DataType
    {
        Bool,
        String,
        Float,
        Double,
        Int16,
        UInt16,
        Int32,
        UInt32,
        Int64,
        UInt64,
    }

    class Program
    {
        static void Main(string[] args)
        {
            string b = "bool";
            Console.WriteLine(b.Equals(DataType.Bool.ToString(), StringComparison.OrdinalIgnoreCase));
            Console.ReadKey();
        }
    }
}
