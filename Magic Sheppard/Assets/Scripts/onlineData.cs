using UnityEngine;
using System.Collections;
using System.Net;
using System;
using System.IO;

namespace MakeAGETRequest_charp
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
   public class onlineData
    {
      public void Main()
        {
            string sURL;
            sURL = "http://www.microsoft.com";

            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);

            WebProxy myProxy = new WebProxy("myproxy", 22);
            myProxy.BypassProxyOnLocal = true;

            wrGETURL.Proxy = WebProxy.GetDefaultProxy();

            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;

            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                    Console.WriteLine("{0}:{1}", i, sLine);
            }
            Console.ReadLine();
        }
    }
}