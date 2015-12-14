using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System;
using System.Collections;

public class PostData2 : MonoBehaviour {

  public string url = "https://drproject.twi.tudelft.nl/ewi3620tu6/sendscores.php";
    public WWW www;

    public void send() { 

            StartCoroutine(WaitForRequest());

    }

     public IEnumerator WaitForRequest()

    {

      //  yield return new WaitForEndOfFrame();
        WWWForm form = new WWWForm();
            form.AddField("username", Id.name);
            form.AddField("wheatbought", f.aaa);
            form.AddField("dogsbought", 3);
            form.AddField("timeplayed", 4);
            form.AddField("timeplayedlvl1", 5);
            form.AddField("timeplayedlvl2", 6);
            form.AddField("timeplayedlvl3", 7);
            form.AddField("deathsinlvl1", 8);
            form.AddField("deathsinlvl2", 9);
            form.AddField("deathsinlvl3", 10);
            form.AddField("sheepkilledinlvl1", 11);
            form.AddField("sheepkilledinlvl2", 12);
            form.AddField("sheepkilledinlvl3", 13);
            form.AddField("score", 14);
            www = new WWW(url, form);
            yield return www;





        if (!string.IsNullOrEmpty(www.error))
        {
            print(www.error);
        }
        else
        {
            print("Finished Uploading scores");
        }


    }
}
    

	
