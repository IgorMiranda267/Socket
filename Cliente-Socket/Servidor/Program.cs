using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Cliente
{
    public class Program
    {
        static void Main(string[] args)
        {
            int porta = 8080;
            IPAddress enderecoIp = IPAddress.Parse("127.0.0.1");

            try
            {
                // Cria um objeto Socket e conecta-se ao servidor
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(new IPEndPoint(enderecoIp, porta));

                byte[] imagem = File.ReadAllBytes("E:\\Estudos\\Socket\\abacaxi_de_oculos.jpg");

                // envia a mensagem
                socket.Send(imagem);

                // Fecha a Conexão.
                socket.Close();

            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro ao enviar a imagem: " + ex.Message);
            }
        }
    }
}