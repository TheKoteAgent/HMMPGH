using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ConsoleNetApp2
{
    internal class Program
    {
        static string[] ultrakill = new string[]
         {
            "Кров - це паливо",
            "Людство - Вимерло",
            "Пекло - переповнене",
         };

        static void Main(string[] args)
        {
            Console.WriteLine("Socket Serv");

            try
            {
                IPAddress iPSer = IPAddress.Parse("127.0.0.1");

                IPEndPoint iPEnd = new IPEndPoint(iPSer, 45000);

                Socket server = new Socket(iPSer.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                server.Bind(iPEnd);

                server.Listen(10);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Server Started");
                Console.ResetColor();

                while (true)
                {
                    Socket ns = server.Accept();

                    Console.WriteLine("Client connected: " + ns.RemoteEndPoint.ToString());

                    byte[] bytes = new byte[1024];

                    int byteRec = ns.Receive(bytes);

                    string clientReq = Encoding.UTF8.GetString(bytes, 0, byteRec);

                    PrintMessage($"Received: {clientReq}", ConsoleColor.Green);

                    Random rand = new Random();
                    string randomultrakill = ultrakill[rand.Next(ultrakill.Length)];
                    ns.Send(Encoding.UTF8.GetBytes(randomultrakill));
                    ns.Shutdown(SocketShutdown.Both);
                    ns.Close();
                }
            }
            catch (Exception ex)
            {
                PrintMessage(ex.Message, ConsoleColor.Red);
                PrintMessage(ex.StackTrace, ConsoleColor.Magenta);
            }
        }

        private static void PrintMessage(string message, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
