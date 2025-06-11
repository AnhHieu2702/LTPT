using System;
using System.Linq;
using System.ServiceModel;

[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
public class MyServiceImpl : IMyService
{
    public int SumArray(string arrayStr)
    {
        try
        {
            // Get client information (equivalent to RemoteServer.getClientHost())
            var context = OperationContext.Current;
            if (context?.Channel?.RemoteAddress != null)
            {
                Console.WriteLine($"Client ket noi tu: {context.Channel.RemoteAddress}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Khong xac dinh duoc client host.");
        }

        if (string.IsNullOrWhiteSpace(arrayStr))
        {
            Console.WriteLine("Khong nhan duoc du lieu.");
            return 0;
        }

        string[] parts = arrayStr.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine($"Nhan mang: {string.Join(" ", parts)}");

        int sum = 0;
        foreach (string part in parts)
        {
            if (int.TryParse(part, out int value))
            {
                sum += value;
            }
            else
            {
                Console.WriteLine($"Bo qua gia tri khong hop le: {part}");
            }
        }

        Console.WriteLine($"Da gui tong: {sum}");
        return sum;
    }

    public void Acknowledge(string message)
    {
        Console.WriteLine($"Server da nhan phan hoi tu client: {message}");
    }
}
