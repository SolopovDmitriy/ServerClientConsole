using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;



namespace CocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                IPEndPoint ep = new IPEndPoint(ip, 45000);
                Socket s = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);



                Console.WriteLine("Сервер запущен: ------------------ START");
                s.Bind(ep);
                s.Listen(10);



                Socket ns = null;
                while (true)
                {
                    ns = s.Accept();

                    Console.WriteLine("Конечная точка клиента: " + ns.RemoteEndPoint.ToString());



                    // Обработка инфы от клиент ------- START
                    byte[] bytes = new byte[1024];
                    int byteLen = ns.Receive(bytes);



                    string clientRequest = Encoding.UTF8.GetString(bytes, 0, byteLen);



                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Зарос клиента: { clientRequest}");
                    Console.ResetColor();
                    // Обработка инфы от клиент ------- END



                    string prediction = "";



                    ns.Send(Encoding.UTF8.GetBytes($"Я случайное предсказание: { prediction}"));
                    ns.Shutdown(SocketShutdown.Both);
                    ns.Close();





                }




                //Console.WriteLine("Сервер запущен: ------------------ END");
            }
            catch (Exception ex)
            {



                Console.WriteLine(ex.Message);
            }
            finally
            {



            }
        }
    }
}