using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WcfServiceLibrary;
namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------WCF хост------");
            using (ServiceHost serviceHost = new ServiceHost(typeof(Service1)))
            {
                serviceHost.Open();
                Console.WriteLine("Сервис запущен...");
                Console.WriteLine("***** Host Info *****");
                Console.WriteLine("ABC 1: End1 {0} {1} {2}", serviceHost.Description.Endpoints[0].Address, serviceHost.Description.Endpoints[0].Binding.Name, serviceHost.Description.Endpoints[0].Contract.Name);
                Console.WriteLine("ABC 2: End2 {0} {1} {2}", serviceHost.Description.Endpoints[1].Address, serviceHost.Description.Endpoints[1].Binding.Name, serviceHost.Description.Endpoints[1].Contract.Name);
                Console.WriteLine("***** End Info  *****");
                Console.WriteLine("Нажать Enter для остановки...");
                Console.ReadLine();
            }
        }

        public static void PrintMessage(string str)
        {
            Console.WriteLine(str);
        }
    }
}
