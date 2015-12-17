using UnityEngine;
using System.Collections;

public class Main_Menu : MonoBehaviour {
  

    private static text way;
    private int i;
    string url = "https://drproject.twi.tudelft.nl/ewi3620tu6/checkusername.php";
    public WWW www;

    public void Quitgame()
    {
        Debug.Log("Game is exiting...");
        Application.Quit();

    }

    public void StartGame(string level)
    {
        Application.LoadLevel(level);
    }

    public void disable()
    {
        gameObject.SetActive(false);
    }

   public void enable()
    {
        gameObject.SetActive(true);
    }

    public void SetGameVolume(float vol)
    {
        AudioListener.volume = vol;
    }

}
