using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System;


//namespace Server
//{
    public class PostData
    {

       
   public void send()
        //static void Main(string[] args, int a)
        {
            
            String name = Id.name;
            String a1 = "aantal schapen dood";
            String a2 = link.a.ToString();

            Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            sck.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 23));
            sck.Listen(0);

            Socket acc = sck.Accept();

            byte[] buffer = Encoding.Default.GetBytes(name);      
           
            byte[] buffer1 = Encoding.Default.GetBytes(a1 + name);
            acc.Send(buffer, 0, buffer.Length, 0);
            acc.Send(buffer1, 0, buffer.Length, 0);

            buffer = new byte[255];
            int rec = acc.Receive(buffer, 0, buffer.Length, 0);
            Array.Resize(ref buffer, rec);

            Console.WriteLine("Received: {0}", Encoding.Default.GetString(buffer));

            sck.Close();
            acc.Close();

            Console.Read();
        }
    }
//}
