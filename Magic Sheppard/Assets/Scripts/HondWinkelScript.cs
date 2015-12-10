using UnityEngine;
using UnityEngine.UI;

public class HondWinkelScript : MonoBehaviour {
    public Text AantalHondText;
    public static int aantalhondgekocht;

    // Use this for initialization
    void Start()
    {
        aantalhondgekocht = 0;
        SetAantalHond();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (GraanWinkelScript.aantalcoins > 0)
        {
            aantalhondgekocht = aantalhondgekocht + 1;
            SetAantalHond();
            GraanWinkelScript.aantalcoins = GraanWinkelScript.aantalcoins - 1;
        }
            
    }

    void SetAantalHond()
    {
        AantalHondText.text = "" + aantalhondgekocht;
    }

    void SetAantalCoins()
    {

    }
}