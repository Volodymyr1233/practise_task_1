using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Practise_task_1
{
    internal class CalculateService
    {
        private const string calculator_url = "http://www.dneonline.com/calculator.asmx";
        private const string target_namespace = "http://tempuri.org/";
        public async Task<int> calculateResult(int a, int b, string operation)
        {
            string operation_name = operation switch
            {
                "+" => "Add",
                "-" => "Subtract",
                "*" => "Multiply"
            }; // changing operator to string

            string soap_action = $"{target_namespace}{operation_name}";

            string soap_envelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
              <soap:Body>
                <{operation_name} xmlns=""{target_namespace}"">
                  <intA>{a}</intA>
                  <intB>{b}</intB>
                </{operation_name}>
              </soap:Body>
            </soap:Envelope>"; // request body

            using var client = new HttpClient(); //http client
            var content = new StringContent(soap_envelope, Encoding.UTF8, "text/xml"); // content creation
            content.Headers.Add("SOAPAction", soap_action); // adding headers

            var response = await client.PostAsync(calculator_url, content); // send request
            string responseXml = await response.Content.ReadAsStringAsync(); // read answer

            var tree = System.Xml.Linq.XDocument.Parse(responseXml); //parse answer as tree
            var resultElement = tree.Descendants().FirstOrDefault(element => element.Name.LocalName == $"{operation_name}Result"); //get result of calculation

            return int.Parse(resultElement.Value);
        }
    }
}

    
