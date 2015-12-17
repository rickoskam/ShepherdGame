using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System;
using System.Collections;

public class PostData : MonoBehaviour
{

    public string url = "https://drproject.twi.tudelft.nl/ewi3620tu6/sendscores.php";
    public WWW www;

    public void send()
    {

        StartCoroutine(WaitForRequest());

    }

    public IEnumerator WaitForRequest()

    {

        //  yield return new WaitForEndOfFrame();
        WWWForm form = new WWWForm();
        form.AddField("username", Id.name);
        form.AddField("wheatused", GraanScript.Graangebruiktindezegame);
        form.AddField("dogsused", HondScript.Hondgebruiktindezegame);
        form.AddField("timeplayed", "" + Herder.Tijd);
        form.AddField("finished", Herder.gehaald);
        form.AddField("sheepkilled", HeksScript.aantalschapengedoodinditlevel);
        form.AddField("curedsheep", Herder.aantalschapengenezen);
        form.AddField("sheepdied", Herder.aantalschapendood);
        form.AddField("score", "" + Herder.score);
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



