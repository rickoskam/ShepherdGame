using UnityEngine;
using System.Collections;

public class Main_Menu : MonoBehaviour {

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
