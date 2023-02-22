using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Servidor
{
    public class Program
    {
        static void Main(string[] args)
        {
            int porta = 8080;
            IPAddress enderecoIP = IPAddress.Parse("127.0.0.1");

            // Cria um objeto Socket e conecta-se ao servidor
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(enderecoIP, porta));
            listener.Listen(10);

            Console.WriteLine("Aguardando conexão do cliente...");

            Socket socket = listener.Accept();
            Console.WriteLine("Conecxão estabelecida");
            
            try
            {
                byte[] buffer = new byte[4096];
                int bytesRecebidos = 0;
                MemoryStream memoryStream = new MemoryStream();
                do
                {
                    bytesRecebidos = socket.Receive(buffer);
                    memoryStream.Write(buffer, 0, bytesRecebidos);
                } while (bytesRecebidos > 0);

                // Salva os dados recebidos em um arquivo de imagem
                byte[] imagemBytes = memoryStream.ToArray();
                File.WriteAllBytes("E:\\Estudos\\Socket\\imagem_recebida.jpg", imagemBytes);

                Console.WriteLine("Imagem recebida com sucesso!");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao enviar a imagem: " + ex.Message);
            }
            finally
            {
                // Encerra a conexão
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                listener.Close();
            }
            Console.ReadKey();
        }
    }
}