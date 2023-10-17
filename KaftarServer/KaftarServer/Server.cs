using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace KaftarServer
{
    class Server
    {
        public TcpListener Listner; ///given tcp clients
        public List<ClientInfo> clients = new List<ClientInfo>();
        public List<ClientInfo> NewClients = new List<ClientInfo>();
        public static Server server;
        static System.IO.TextWriter Out;

        public Server(int Port, System.IO.TextWriter _Out)
        {
            Out = _Out;
            Server.server = this;

            ///Header for port
            Listner = new TcpListener(IPAddress.Any, Port);
            Listner.Start(); ///Started this
        }

        public void Work()
        {
            Thread clientListner = new Thread(ListnerClients);
            clientListner.Start();
            while (true)
            {
                foreach (ClientInfo client in clients)
                {
                    if (client.IsConnect)
                    {
                        NetworkStream stream = client.Client.GetStream();
                        while (stream.DataAvailable)
                        {
                            int ReadByte = stream.ReadByte();
                            if (ReadByte != -1)
                            {
                                client.buffer.Add((byte)ReadByte);
                            }
                        }
                        if (client.buffer.Count > 0)
                        {
                            Out.WriteLine("Resend");
                            foreach (ClientInfo otherClient in clients)
                            {
                                byte[] msg = client.buffer.ToArray();
                                client.buffer.Clear();
                                foreach (ClientInfo _otherClient in clients)
                                {
                                    if (_otherClient != client)
                                    {
                                        try
                                        {
                                            _otherClient.Client.GetStream().Write(msg, 0, msg.Length);
                                        }
                                        catch
                                        {
                                            _otherClient.IsConnect = false;
                                            _otherClient.Client.Close();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                clients.RemoveAll(delegate(ClientInfo CI)
                    {
                    if(!CI.IsConnect){
                        Server.Out.WriteLine("client disconnected!");
                        return true;
                    }
                    return false;
                });

                if(NewClients.Count > 0){
                    clients.AddRange(NewClients);
                    NewClients.Clear();
                }
            }
        }
        
        /// <summary>
        /// end Work server
        /// </summary>
        ~Server()
        {
            ///if slushatel was created
            if (Listner != null)
            {
                Listner.Stop();
            }
            foreach (ClientInfo client in clients)
            {
                client.Client.Close();
            }
        }

        static void ListnerClients()
        {
            while (true)
            {
                server.NewClients.Add(new ClientInfo(server.Listner.AcceptTcpClient()));
                Out.WriteLine("New Client");
            }
        }
 
    }
    class ClientInfo
    {
        public TcpClient Client;
        public List<byte> buffer = new List<byte>();
        public bool IsConnect;
        public ClientInfo(TcpClient Client)
        {
            this.Client = Client;
            IsConnect = true;
        }
    }
}
