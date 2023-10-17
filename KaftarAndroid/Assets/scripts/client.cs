using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading; 

class Client
{
    static TcpClient client;
    public Client(int Port, string connectIp)
    {
        client = new TcpClient();
        client.Connect(IPAddress.Parse(connectIp), Port);
    } 

    public void Work()
    {
        Thread clientListner = new Thread(Reader);
        clientListner.Start();
    }

    public void SendMessage(string message)
    {
        message.Trim();
        byte[] Buffer = Encoding.ASCII.GetBytes((message).ToCharArray());
        client.GetStream().Write(Buffer, 0, Buffer.Length);
        Chat.message.Add(message);
    }

    static void Reader(){
        while(true){
            NetworkStream NS = client.GetStream();
            List<Byte> Buffer = new List<byte>();
            while(NS.DataAvailable){
                int ReadByte = NS.ReadByte();
                if(ReadByte > -1){
                    Buffer.Add((byte)ReadByte);
                }
            }
            if(Buffer.Count > 0){
                Chat.message.Add(Encoding.ASCII.GetString(Buffer.ToArray()));
            }
        }
        
    }
    ~Client(){
        if(client != null){
            client.Close();
        }
    }
}
