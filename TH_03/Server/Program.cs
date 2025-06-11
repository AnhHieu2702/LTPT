using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server
{
    public static void Main(string[] args)
    {
        try
        {
            TcpListener server = new TcpListener(IPAddress.Any, 9999);
            server.Start();
            Console.WriteLine("Server dang chay...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client ket noi: " + ((IPEndPoint)client.Client.RemoteEndPoint).Address);

                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                string data = reader.ReadLine();
                Console.WriteLine("Nhan mang: " + data);
                string[] parts = data.Split(' ');
                int[] numbers = new int[parts.Length];

                for (int i = 0; i < parts.Length; i++)
                {
                    numbers[i] = int.Parse(parts[i]);
                }

                // Nhiệm vụ xử lý: đếm số chẵn
                int countEven = 0;
                foreach (int num in numbers)
                {
                    if (num % 2 == 0) countEven++;
                }
                Console.WriteLine("So luong so chan: " + countEven);
                writer.WriteLine("So luong so chan: " + countEven);

                client.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Loi: " + e.Message);
        }
    }
}