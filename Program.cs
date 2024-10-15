using System;
using System.IO;
using System.Net;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Define the URL
                string url = "https://www.scstatehouse.gov/meetings.php?chamber=H";
                // Change the URL for the other link
                // url = "https://www.ncleg.gov/LegislativeCalendar/10/2024"; 


                // Optionally bypass SSL certificate validation (not for production)
                //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;

                // Create the request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;  // Disable keep-alive
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;  // Handle compression
                request.Timeout = 100000;  // Set a reasonable timeout

                // Get the response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine("Request succeeded with status code: " + response.StatusCode);

                        // Read the response stream
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            string responseText = reader.ReadToEnd();
                            Console.WriteLine("Response from the website:");
                            Console.WriteLine(responseText);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Request failed with status code: " + response.StatusCode);
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("WebException caught: " + ex.Message);
                Console.WriteLine(ex.InnerException);
                if (ex.Response != null)
                {
                    using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        string errorResponse = reader.ReadToEnd();
                        Console.WriteLine("Error response: " + errorResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: " + ex.Message);
                Console.WriteLine("Exception caught: " + ex.InnerException);
            }

            Console.ReadLine();  // Wait for user input
        }
    }
}
