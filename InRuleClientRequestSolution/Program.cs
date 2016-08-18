using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DH.PaymentAllocationService;

namespace InRuleClientRequestSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Request started at:{DateTime.Now}");
                // var e = 3;
                var reader = new XmlSerializer(typeof(InRuleRequest));
                var file = new StreamReader("InRuleRequestJane1.xml");
                //var request = new InRuleRequest();
                var request = (InRuleRequest)reader.Deserialize(file);
                var inRuleClient = new InRuleServiceClient();
                Console.WriteLine($"Request hit service at:{DateTime.Now}");
                var response = inRuleClient.PaymentAllocationRules(request);
                Console.WriteLine($"Service replied at:{DateTime.Now}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Service replied at:{DateTime.Now}");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
