using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DH.PaymentAllocationService.Tests.InRulePaymentAllocationService;
using DH.PaymentAllocationObjects;
using DH.PaymentAllocationService.Tests.Properties;
using DH.PaymentAllocationService;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace DH.PaymentAllocationService.Tests
{
    [TestClass]
    public class UnitTests_PaymentAllocation
    {
        [TestMethod]
        public void TestUndirectedPaymentAllocation()
        {
            using (InRuleServiceClient serviceClient = new InRuleServiceClient())
            {
                XmlSerializer reader = new XmlSerializer(typeof(InRuleRequest));
                StreamReader file = new StreamReader(Settings.Default.TestFilesPath + "InRuleRequestUndirected.xml");
                InRuleRequest request = new InRuleRequest();
                request = (InRuleRequest)reader.Deserialize(file);

                InRuleResponse response = serviceClient.PaymentAllocationRules(request);
            }
        }

        [TestMethod]
        public void TestDirectedPaymentAllocation()
        {
            using (InRuleServiceClient serviceClient = new InRuleServiceClient())
            {
                XmlSerializer reader = new XmlSerializer(typeof(InRuleRequest));
                StreamReader file = new StreamReader(Settings.Default.TestFilesPath + "InRuleRequestDirected.xml");
                InRuleRequest request = new InRuleRequest();
                request = (InRuleRequest)reader.Deserialize(file);

                InRuleResponse response = serviceClient.PaymentAllocationRules(request);
            }
        }

        [TestMethod]
        public void TestUndirectedPaymentAllocation_Jane1()
        {
            using (InRuleServiceClient serviceClient = new InRuleServiceClient())
            {
                InRuleResponse response = serviceClient.PaymentAllocationRules(GetRequestFromFile("InRuleRequestJane1.xml"));
                InRuleResponse expectedResponse = GetResponseFromFile("InRuleResponseJane1.xml");
                Assert.IsTrue(response.RuleInfo.Status == "Success");
                Assert.IsTrue(IsResponseCorrect(response, expectedResponse));
            }
        }

        [TestMethod]
        public void TestUndirectedPaymentAllocation_Jane2()
        {
            using (InRuleServiceClient serviceClient = new InRuleServiceClient())
            {
                InRuleResponse response = serviceClient.PaymentAllocationRules(GetRequestFromFile("InRuleRequestJane2.xml"));
                InRuleResponse expectedResponse = GetResponseFromFile("InRuleResponseJane2.xml");
                Assert.IsTrue(response.RuleInfo.Status == "Success");
                Assert.IsTrue(IsResponseCorrect(response, expectedResponse));
            }
        }

        [TestMethod]
        public void TestUndirectedPaymentAllocation_Jane3()
        {
            using (InRuleServiceClient serviceClient = new InRuleServiceClient())
            {
                InRuleResponse response = serviceClient.PaymentAllocationRules(GetRequestFromFile("InRuleRequestJane3.xml"));
                InRuleResponse expectedResponse = GetResponseFromFile("InRuleResponseJane3.xml");
                Assert.IsTrue(response.RuleInfo.Status == "Success");
                Assert.IsTrue(IsResponseCorrect(response, expectedResponse));
            }
        }

        [TestMethod]
        public void TestUndirectedPaymentAllocation_Jane5()
        {
            using (InRuleServiceClient serviceClient = new InRuleServiceClient())
            {
                InRuleResponse response = serviceClient.PaymentAllocationRules(GetRequestFromFile("InRuleRequestJane5.xml"));
                InRuleResponse expectedResponse = GetResponseFromFile("InRuleResponseJane5.xml");
                Assert.IsTrue(response.RuleInfo.Status == "Success");
                Assert.IsTrue(IsResponseCorrect(response, expectedResponse));
            }
        }

        [TestMethod]
        public void TestUndirectedPaymentAllocation_Jane6a()
        {
            using (InRuleServiceClient serviceClient = new InRuleServiceClient())
            {
                InRuleResponse response = serviceClient.PaymentAllocationRules(GetRequestFromFile("InRuleRequestJane6a.xml"));
                InRuleResponse expectedResponse = GetResponseFromFile("InRuleResponseJane6a.xml");
                Assert.IsTrue(response.RuleInfo.Status == "Success");
                Assert.IsTrue(IsResponseCorrect(response, expectedResponse));
            }
        }

        [TestMethod]
        public void TestUndirectedPaymentAllocation_Jane6b()
        {
            using (InRuleServiceClient serviceClient = new InRuleServiceClient())
            {
                InRuleResponse response = serviceClient.PaymentAllocationRules(GetRequestFromFile("InRuleRequestJane6b.xml"));
                InRuleResponse expectedResponse = GetResponseFromFile("InRuleResponseJane6b.xml");
                Assert.IsTrue(response.RuleInfo.Status == "Success");
                Assert.IsTrue(IsResponseCorrect(response, expectedResponse));
            }
        }

        [TestMethod]
        public void TestUndirectedPaymentAllocation_Jane6c()
        {
            using (InRuleServiceClient serviceClient = new InRuleServiceClient())
            {
                InRuleResponse response = serviceClient.PaymentAllocationRules(GetRequestFromFile("InRuleRequestJane6c.xml"));
                InRuleResponse expectedResponse = GetResponseFromFile("InRuleResponseJane6c.xml");
                Assert.IsTrue(response.RuleInfo.Status == "Success");
                Assert.IsTrue(IsResponseCorrect(response, expectedResponse));
            }
        }

        [TestMethod]
        public void TestUndirectedPaymentAllocation_MissingFederal()
        {
            using (InRuleServiceClient serviceClient = new InRuleServiceClient())
            {
                InRuleResponse response = serviceClient.PaymentAllocationRules(GetRequestFromFile("InRuleRequest_missing_federal.xml"));
                //Failure if response validation is enabled on the server
                Assert.IsTrue(response.RuleInfo.Status == "Failure");
                //Success if response validation is not enabled on the server
                //Assert.IsTrue(response.RuleInfo.Status == "Failure");
                Assert.IsTrue(response.RuleInfo.ErrorDetails.Count > 0);
            }
        }

        private InRuleRequest GetRequestFromFile(string fileName)
        {
            XmlSerializer reader = new XmlSerializer(typeof(InRuleRequest));
            StreamReader file = new StreamReader(Settings.Default.TestFilesPath + fileName);
            InRuleRequest request = new InRuleRequest();
            request = (InRuleRequest)reader.Deserialize(file);
            return request;
        }

        private InRuleResponse GetResponseFromFile(string fileName)
        {
            XmlSerializer reader = new XmlSerializer(typeof(InRuleResponse));
            StreamReader file = new StreamReader(Settings.Default.TestFilesPath + fileName);
            InRuleResponse response = new InRuleResponse();
            response = (InRuleResponse)reader.Deserialize(file);
            return response;
        }

        private bool IsResponseCorrect (InRuleResponse response, InRuleResponse expectedResponse)
        {
            List<Payment> payments = DeserializeFromXml<List<Payment>>(response.RuleResult.Replace("LoanPayments", "ArrayOfPayment"));
            List<Payment> expectedPayments = DeserializeFromXml<List<Payment>>(expectedResponse.RuleResult.Replace("LoanPayments", "ArrayOfPayment"));

            foreach (Payment payment in payments)
            {
                bool loanNumberFound = false;
                foreach (Payment expectedPayment in expectedPayments)
                {
                    if (expectedPayment.LoanNumber == payment.LoanNumber)
                    {
                        loanNumberFound = true;
                        if ((expectedPayment.InterestPayment != payment.InterestPayment) ||
                                (expectedPayment.PrincipalPayment != payment.PrincipalPayment))
                            return false;
                     }
                }
                if (!loanNumberFound)
                    return false;
            }
            return true;
        }

        public static T DeserializeFromXml<T>(string xml)
        {
            T result;
            var ser = new XmlSerializer(typeof(T));
            using (TextReader tr = new StringReader(xml))
            {
                result = (T)ser.Deserialize(tr);
            }
            return result;
        }
    }
}
