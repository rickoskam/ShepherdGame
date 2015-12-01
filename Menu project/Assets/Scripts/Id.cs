using UnityEngine;
using System.Collections;

public class Id : MonoBehaviour
{

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
}
