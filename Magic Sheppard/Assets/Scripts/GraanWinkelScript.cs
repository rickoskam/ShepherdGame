using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GraanWinkelScript : MonoBehaviour {
    public Text AantalGraanText;
    public Text AantalCoinsText;
    public static int aantalgraangekocht;
    public static int aantalcoins;

	// Use this for initialization
	void Start () {
        aantalgraangekocht = 0;
        aantalcoins = 10;
        SetAantalGraan();
        SetAantalCoins();
	}
	
	// Update is called once per frame
	void Update () {
        SetAantalCoins();
	}

    void OnMouseDown()
    {
        if (aantalcoins > 0)
        {
            aantalgraangekocht = aantalgraangekocht + 1;
            SetAantalGraan();
            aantalcoins = aantalcoins - 1;
            SetAantalCoins();
        }
        
    }

    void SetAantalGraan()
    {
        AantalGraanText.text = "" + aantalgraangekocht;
    }

    void SetAantalCoins()
    {
        aantalcoins = 10 - aantalgraangekocht - HondWinkelScript.aantalhondgekocht;
        AantalCoinsText.text = "" + aantalcoins;
    }
}
