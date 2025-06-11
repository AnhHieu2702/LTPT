using System;
using System.ServiceModel;
using System.ServiceModel.Description;

public class MyServer
{
    public static void Main(string[] args)
    {
        try
        {
            // Create service host
            var service = new MyServiceImpl();
            var host = new ServiceHost(service);

            // Configure endpoint - equivalent to port 1000 in Java
            var binding = new BasicHttpBinding();
            var address = "http://localhost:1000/abc"; // Same as Java RMI URL
            
            host.AddServiceEndpoint(typeof(IMyService), binding, address);

            // Enable metadata exchange
            var smb = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true,
                HttpGetUrl = new Uri("http://localhost:1000/abc/mex")
            };
            host.Description.Behaviors.Add(smb);

            // Start the service
            host.Open();
            Console.WriteLine(">>> Server da san sang...");
            Console.WriteLine($"Service running at: {address}");
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            host.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Lỗi Server: {e.Message}");
            Console.WriteLine(e.StackTrace);
        }
    }
}