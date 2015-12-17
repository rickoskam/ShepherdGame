using UnityEngine;
using System.Collections;

public class Id : MonoBehaviour
{
    public Canvas canvas1;
    public Canvas canvas2;
    public GameObject t1;
    string url = "https://drproject.twi.tudelft.nl/ewi3620tu6/checkusername.php";
    public WWW www;

    public void checkname()
    {


        StartCoroutine(WaitForRequest());

    }

    public IEnumerator WaitForRequest()

    {

        //  yield return new WaitForEndOfFrame();
        WWWForm form = new WWWForm();
        form.AddField("username", Id.name);
        www = new WWW(url, form);
        yield return www;
        t1.gameObject.SetActive(false);





        if (!string.IsNullOrEmpty(www.error))
        {
            print(www.error);
        }
        else
        {

            print("Finished checking name");
        }
        //waitname();

            if (www.text.Trim().Equals("1"))

            {

                canvas1.gameObject.SetActive(true);
                canvas2.gameObject.SetActive(false);

            }
            else
            {
                //Debug.Log(www.text);
                Debug.Log("wrong name");
            t1.gameObject.SetActive(true);
            }
        }



    public void waitname()
    {
        StartCoroutine(wait());
    }

        IEnumerator wait()
    {
            Debug.Log("Before Waiting 2 seconds");
            yield return new WaitForSeconds(5);
            Debug.Log("After Waiting 2 Seconds");
        }

    

    public static string name = "LOL";

    public void disable()
    {
        gameObject.SetActive(false);
    }

    public void enable()
    {
        gameObject.SetActive(true);
    }

    public void Quitgame()
    {
        Debug.Log("Game is exiting...");
        Application.Quit();

    }
    public void setname(string n)
    {
        name = n;
        Debug.Log(name);
    }

public string getname()
    {
        return name;
    }

}
