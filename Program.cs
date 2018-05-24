using System;
using System.Threading.Tasks;
using JobFair.Tagit.Sgtin;

namespace JobFair.Tagit
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please supply product code");
                return;
            }

            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var sgtin = new Item(args[0]);

            Console.WriteLine("Number: " + sgtin.SgtinNumber);
            Console.WriteLine("Company Prefix: " + sgtin.CompanyPrefix);
            Console.WriteLine("Item Reference: " + sgtin.ItemReference);
            Console.WriteLine("Serial Number: " + sgtin.SerialNumber);

            var webApiService = new TagitWebApiService("c2VjcmV0OnRVM2trIT94eHg=");
            var rv = await webApiService.GetProductInfo(
                sgtin.CompanyPrefix, sgtin.ItemReference);

            Console.WriteLine("Response from Tagit Web API:");
            Console.WriteLine(rv);
        }
    }
}
