using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System;
using System.Collections;

public class PostData2 : MonoBehaviour {

    public void sendData() {
        String name = Id.name;
        String a1 = "aantal schapen dood";
        int a2 = link.a;
        int y = 1;

        string url = "https://drproject.twi.tudelft.nl/ewi3620tu6/";

        WWWForm form = new WWWForm();
        // adding an int to a form
        form.AddField("score", a2);
        //adding a String to a form
        form.AddField(name, 1);
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        //check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.data);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }

        }
    }

	
