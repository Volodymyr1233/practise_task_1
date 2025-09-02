using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace Practise_task_1
{
    internal class DataBaseSQL
    {
        public static void initializeDB()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Integrated security=SSPI;TrustServerCertificate=True;"))
                {
                    connection.Open();

                    string create_db = @"
                    IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'Calculations')
                    BEGIN
                        CREATE DATABASE Calculations;
                    END";



                    using (SqlCommand command_1 = new SqlCommand(create_db, connection))
                    {
                        command_1.ExecuteNonQuery();
                    }

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

            try
            {
                using (SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Calculations;Integrated security=SSPI;TrustServerCertificate=True;"))
                {
                    connection.Open();
                    string create_table = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'XMLFiles')
                    BEGIN
                        CREATE TABLE XMLFiles (
                            Id INT PRIMARY KEY IDENTITY(1,1),
                            XmlContent XML NOT NULL
                        );
                    END";

                    using (SqlCommand command_2 = new SqlCommand(create_table, connection))
                    {
                        command_2.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        } 

        public static void saveXMLFile(string fileName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Calculations;Integrated security=SSPI;TrustServerCertificate=True;"))
                {
                    connection.Open();
                    XmlDocument document = new XmlDocument();
                    document.Load(fileName);
                    if (document.FirstChild is XmlDeclaration declaration)
                    {
                        document.RemoveChild(declaration);
                    }

                    string xml_content = document.OuterXml;

                    string query = "INSERT INTO XMLFiles(XmlContent) VALUES (@xml)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@xml", xml_content);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Plik jest zapisany do bazy");
                    
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

        }

     public static string getXMLFile()
        {
            string content = null;
            try
            {
                using (SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Calculations;Integrated security=SSPI;TrustServerCertificate=True;"))
                {
                    connection.Open();
                    
                    string query = "SELECT TOP 1 XmlContent FROM XMLFiles ORDER BY Id DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            content = reader.GetString(0);
                        }
                    }

                    MessageBox.Show("Dane pobrane!");

                    return content;

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return content;
            }
        }

    }
}
