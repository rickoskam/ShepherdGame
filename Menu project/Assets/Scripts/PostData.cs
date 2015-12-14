using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System;


//namespace Server
//{
    public class PostData: MonoBehaviour
    {

       
   public static void send()
        //static void Main(string[] args, int a)
        {
            
            String name = Id.name;
            String a2 = link.a.ToString();
            int y = 9;
            String a3 = y.ToString();
            Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            sck.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 23));
            sck.Listen(0);

            Socket acc = sck.Accept();
        // all the data will  be sent in one buffer.
            byte[] buffer = Encoding.Default.GetBytes(name + a2 + a3);      
           
            acc.Send(buffer, 0, buffer.Length, 0);
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
