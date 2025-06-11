using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

public class Client
{
    public static void Main(string[] args)
    {
        try
        {
            TcpClient client = new TcpClient("172.20.10.3", 9999);
            Console.WriteLine("Da ket noi toi server.");

            NetworkStream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            Random rand = new Random();
            int N = 10;
            StringBuilder sb = new StringBuilder();

            Console.Write("Da gui mang ngau nhien: ");
            for (int i = 0; i < N; i++)
            {
                int value = rand.Next(100);
                sb.Append(value).Append(" ");
                Console.Write(value + " ");
            }
            Console.WriteLine();

            writer.WriteLine(sb.ToString().Trim());

            string result = reader.ReadLine();
            Console.WriteLine("Ket qua tu server: " + result);

            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Loi: " + e.Message);
        }
    }
}