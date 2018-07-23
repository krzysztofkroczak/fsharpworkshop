using System;

namespace interConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var cs = new Work.CustomerService();
            var customer = cs.UpgradeCustomer(2);
            Console.WriteLine(customer);
            Console.ReadKey();
        }
    }
}
