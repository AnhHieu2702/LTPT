using System;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

class Producer
{
    static void Main(string[] args)
    {
        // Kết nối đến ActiveMQ (đổi IP theo máy chạy ActiveMQ)
        IConnectionFactory factory = new ConnectionFactory("tcp://172.20.10.3:61616");
        using (IConnection connection = factory.CreateConnection())
        {
            connection.Start();

            // Tạo session và queue
            using (ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge))
            {
                IDestination destination = session.GetQueue("TH5");

                // Tạo producer
                using (IMessageProducer producer = session.CreateProducer(destination))
                {
                    Console.WriteLine("Nhập tin nhắn vào hàng đợi (thoát gõ 'exit'):");

                    while (true)
                    {
                        Console.Write(">> ");
                        string input = Console.ReadLine();
                        if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                            break;

                        ITextMessage message = session.CreateTextMessage(input);
                        producer.Send(message);
                        Console.WriteLine("Đã gửi vào queue: " + input);
                    }
                }
            }
        }
    }
}
