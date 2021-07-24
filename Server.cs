using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;

namespace Server
{
    class Program
    {
        private static TcpListener tcpListener;
        private static List<TcpClient> tcpClientsList = new List<TcpClient>();

        static void Main(string[] args)
        {
            tcpListener = new TcpListener(IPAddress.Any, 1234);
            tcpListener.Start();

            Console.WriteLine("Server is started.");

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                tcpClientsList.Add(tcpClient);
                Console.WriteLine("Active number of connection is:" + tcpClientsList.Count);
                Thread thread = new Thread(ClientListener);
                thread.Start(tcpClient);
            }
        }

        public static void ClientListener(object obj)
        {
            TcpClient tcpClient = (TcpClient)obj;
            StreamReader reader = new StreamReader(tcpClient.GetStream());

            Console.WriteLine("A client is connected.");

            while (true)
            {
                try
                {
                    string message = reader.ReadLine();
                    BroadCast(message, tcpClient);
                    Console.WriteLine(message);
                }
                catch (IOException)
                {
                    Console.WriteLine("A connection is terminated.");
                    tcpClientsList.RemoveAt(tcpClientsList.IndexOf(tcpClient));
                    Console.WriteLine("Active number of connection is:" + tcpClientsList.Count);
                    tcpClient.Close();
                    break;
                }
            }
        }

        public static void BroadCast(string msg, TcpClient senderClient)
        {
            for(int i = 0; i < tcpClientsList.Count; i++)
            {
                if(tcpClientsList[i] != senderClient)       // Do not show the same message in the sender.
                {
                    if (tcpClientsList[i].Connected)        // Is connection alive??
                    {
                        StreamWriter sWriter = new StreamWriter(tcpClientsList[i].GetStream());
                        sWriter.WriteLine(msg);
                        sWriter.Flush();
                    }
                    else                                    // Delete not connected client from list.
                    {
                        tcpClientsList.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }
}
