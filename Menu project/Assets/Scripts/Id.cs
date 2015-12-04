using UnityEngine;
using System.Collections;

public class Id : MonoBehaviour
{

    public static string name;

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
