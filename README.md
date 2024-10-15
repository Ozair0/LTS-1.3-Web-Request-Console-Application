
# LTS-1.3 Web Request Console Application

## Overview

This is a simple .NET Framework 4.8 console application that performs an HTTP GET request to a specified URL and handles the response. It demonstrates how to work with the `HttpWebRequest` and `HttpWebResponse` classes to make web requests, manage SSL/TLS protocols, and handle potential errors.

## Features
- **TLS 1.3 support**: Configures the security protocol to use TLS 1.3 for secure communication.
- **Handles GZip and Deflate Compression**: Automatically decompresses the response if it's compressed.
- **Error handling**: Catches both general exceptions and web-specific errors (e.g., failed requests).
- **SSL Certificate Bypass (optional)**: Can optionally bypass SSL certificate validation (commented out for security reasons).
- **Response Logging**: Outputs the full response content to the console.
- **Timeout configuration**: Allows setting a custom timeout for the request.
- **Keep-Alive disabled**: Disables persistent connections to simplify the handling of the request.

## Prerequisites
- .NET Framework 4.8 or higher
- Visual Studio 2019 or later (or any compatible IDE)
- Internet access to send the web requests

## How to Run
1. Clone or download this repository.
2. Open the project in Visual Studio or any compatible IDE.
3. Build the solution.
4. Run the application. It will:
   - Make a request to `https://www.scstatehouse.gov/meetings.php?chamber=H`.
   - Output the HTTP status code and the full HTML content of the page.

   Alternatively, you can switch the URL by uncommenting the second URL (`https://www.ncleg.gov/LegislativeCalendar/10/2024`) to see a different response.

## Code Breakdown

### 1. Define the URL
In the main method, the URL of the webpage to scrape is defined:

```csharp
string url = "https://www.scstatehouse.gov/meetings.php?chamber=H";
// Optionally, switch to another URL
// url = "https://www.ncleg.gov/LegislativeCalendar/10/2024";
```

### 2. Configure TLS and Create Request
The code sets the security protocol to TLS 1.3 and creates an HTTP web request:

```csharp
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
request.KeepAlive = false;  // Disable keep-alive for simplicity
request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;  // Handle compression
request.Timeout = 100000;  // Timeout set to 100 seconds
```

### 3. Handle the Response
If the response is successful (status code 200), it reads the response body and prints it to the console:

```csharp
using (StreamReader reader = new StreamReader(response.GetResponseStream()))
{
    string responseText = reader.ReadToEnd();
    Console.WriteLine("Response from the website:");
    Console.WriteLine(responseText);
}
```

### 4. Error Handling
In case of any `WebException`, the error is caught, and if available, the error response body is read and displayed:

```csharp
catch (WebException ex)
{
    Console.WriteLine("WebException caught: " + ex.Message);
    if (ex.Response != null)
    {
        using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
        {
            string errorResponse = reader.ReadToEnd();
            Console.WriteLine("Error response: " + errorResponse);
        }
    }
}
```

### 5. Optional SSL Certificate Validation Bypass
If you need to bypass SSL certificate validation for testing purposes (not recommended for production), you can uncomment the following line:

```csharp
//ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
```

## Notes
- This project is intended for educational purposes. In production environments, make sure to handle SSL certificate validation properly and avoid bypassing security protocols.
- Be aware of the legal and ethical implications of web scraping. Always ensure you have permission to scrape websites.

## License
This project is open-source and freely available for modification and use.

---

Feel free to reach out if you have any questions or issues while using this project!
