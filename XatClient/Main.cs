using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace XatClient
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Client client = new Client("127.0.0.1", 6969);
			
			if (client.ConnectToServer())
			{
                string frase = "";
                Console.WriteLine("T'has connectat al servidor (per sortir 'disconnect')");

                Console.WriteLine("Has de posar el teu nom d'usuari.");
                while (frase != "disconnect")
                {
                    frase = Console.ReadLine();
                    if (frase != "disconnect")
                        client.WriteLine(frase);
                }
			}
		}
	}
	
	public class Client
	{
		private NetworkStream netStream;
		private StreamReader readerStream;
		private StreamWriter writerStream;
		private IPEndPoint server_endpoint;
		private TcpClient tcpClient;
		
		public Client(string ip, int port)
		{
			IPAddress address = IPAddress.Parse(ip);
			server_endpoint = new IPEndPoint(address, port);
		}

		public string ReadLine()
		{
			return readerStream.ReadLine();
		}
		
		public void WriteLine(string str)
		{
			writerStream.WriteLine(str);
			writerStream.Flush();
		}
		
		public bool ConnectToServer()
		{
			try
			{
				// tcpClient = new TcpClient(server_endpoint);
				tcpClient = new TcpClient("127.0.0.1", 6969);
				
				netStream = tcpClient.GetStream();
				readerStream = new StreamReader(netStream);
				writerStream = new StreamWriter(netStream);
			}
			catch(Exception e)
			{
				Console.WriteLine(e.StackTrace);
				Console.WriteLine(e.Message);
				return false;
			}

			Console.WriteLine("M'he connectat amb el servidor");

			return true;		
		}
	}
}
