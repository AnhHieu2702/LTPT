using System;
using System.ServiceModel;
using System.Text;

namespace _2
{
    public class MyClient
    {
        public static void Main(string[] args)
        {
            try
            {
                // IP của server (đổi theo máy bạn) - equivalent to Java's Naming.lookup
                var binding = new BasicHttpBinding();
                var endpoint = new EndpointAddress("http://172.20.10.4:1000/abc"); // Same IP as Java
                var factory = new ChannelFactory<IMyService>(binding, endpoint);

                // Create channel - equivalent to (MyService) Naming.lookup
                var service = factory.CreateChannel();

                // Generate random array - same logic as Java
                int[] array = new int[2];
                Random rand = new Random();
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = rand.Next(100); // từ 0 đến 99
                    sb.Append(array[i]).Append(" ");
                }

                string arrayStr = sb.ToString().Trim();
                Console.WriteLine($"Client da gui mang: {arrayStr}");

                // Call service method
                int result = service.SumArray(arrayStr);
                Console.WriteLine($"Tong nhan tu server: {result}");

                // Gửi phản hồi lại cho server
                service.Acknowledge($"Da nhan tong = {result}");

                // Close connection
                ((IClientChannel)service).Close();
                factory.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi Client: {e.Message}");
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}