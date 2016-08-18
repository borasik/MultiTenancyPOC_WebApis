using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using DH.PaymentAllocationObjects;

namespace DH.PaymentAllocationService
{
    public static class Util
    {
        #region serializers
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

        public static void SerializeToXmlFile<T>(T obj, string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                var ser = new XmlSerializer(typeof(T));
                ser.Serialize(fileStream, obj);
            }
        }

        public static string SerializeToXmlString<T>(T obj)
        {
            using (var stringWriter = new StringWriter())
            {
                var ser = new XmlSerializer(typeof(T));
                ser.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }
        #endregion

        public static XmlNode RenameNode(XmlNode node, string namespaceUri, string qualifiedName)
        {
            if (node.NodeType == XmlNodeType.Element)
            {
                XmlElement oldElement = (XmlElement)node;
                XmlElement newElement =
                node.OwnerDocument.CreateElement(qualifiedName, namespaceUri);
                while (oldElement.HasAttributes)
                {
                    newElement.SetAttributeNode(oldElement.RemoveAttributeNode(oldElement.Attributes[0]));
                }
                while (oldElement.HasChildNodes)
                {
                    newElement.AppendChild(oldElement.FirstChild);
                }
                if (oldElement.ParentNode != null)
                {
                    oldElement.ParentNode.ReplaceChild(newElement, oldElement);
                }
                return newElement;
            }
            else
            {
                return null;
            }
        }

        public static string GetRuleResultXml(List<Payment> payments)
        {

            XmlDocument xmlPayments = new XmlDocument();
            xmlPayments.LoadXml(Util.SerializeToXmlString(payments));

            XmlNode node = xmlPayments.SelectSingleNode("ArrayOfPayment");

            XmlDocument newDoc = new XmlDocument();
            XmlElement ele = newDoc.CreateElement("LoanPayments");
            ele.InnerXml = node.InnerXml;

            //Commented code below can be used if the Payment node name needs to change in the request.
            //It was originally used to accommodate Loan as name.
            //foreach (XmlNode loanNode in xmlPayments.SelectNodes("//Payment"))
            //{
            //    XmlNode newNode = newDoc.CreateNode(XmlNodeType.Element, "Loan", "");
            //    newNode.InnerXml = loanNode.InnerXml;
            //    ele.AppendChild(newNode);
            //}
            newDoc.AppendChild(ele);

            return newDoc.InnerXml;
        }
    }
}
