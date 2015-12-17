using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GraanScript : MonoBehaviour {
    public static int aantalgraangekocht = GraanWinkelScript.aantalgraangekocht; //PRIVATE MAKEN!
    public Text AantalGraanText;
    public float speed = 1;

    private bool graantje = true;
    private bool aantrekking = false;

    public static int Graangebruiktindezegame;

    private int gr;

    // Use this for initialization
    void Start () {
        //gr = gr + Graangebruiktindezegame;
        //aantalgraangekocht = aantalgraangekocht - gr;
        SetAantalGraanText();
        Graangebruiktindezegame = 0;
	}
	
	// Update is called once per frame
	void Update () {
		var H = GameObject.FindGameObjectWithTag ("Herder");
		if (aantalgraangekocht > 0 && Input.GetKeyDown(KeyCode.Alpha1) && graantje == true)
		{
            transform.parent = null;
            Vector3 GraanPlek = new Vector3(H.transform.position.x, H.transform.position.y, H.transform.position.z);
			transform.position = GraanPlek;
            graantje = false;
			StartCoroutine(TimerGraan());
            aantrekking = true;
            Herder.score = Herder.score - 100;
            Graangebruiktindezegame = Graangebruiktindezegame + 1;
		}
        if (aantrekking == true)
        {
            GameObject[] schapen = GameObject.FindGameObjectsWithTag("Schaap");
            int lengte = schapen.Length;
            for (int j = 0; j < lengte; j++)
            {
                GameObject schaapje = schapen[j];
                float sheepx = schaapje.transform.position.x;
                float sheepz = schaapje.transform.position.z;
                float graanx = transform.position.x;
                float graanz = transform.position.z;
                float xrichting = graanx - sheepx;
                float zrichting = graanz - sheepz;

                schaapje.transform.Translate(new Vector3(xrichting * Time.deltaTime*speed, 0.0f, zrichting * Time.deltaTime*speed));
            }
        }
}

	IEnumerator TimerGraan()
	{
        //Hier komt de aantrekking van het graan voor de schapen
        aantalgraangekocht = aantalgraangekocht - 1;
        SetAantalGraanText();
        yield return new WaitForSeconds (10);
		gameObject.SetActive (false);
        aantrekking = false;
        if (aantalgraangekocht > 0)
        {
            ReturnBeginGraan();
        }
	}

    void ReturnBeginGraan()
    {
        gameObject.SetActive(true);
        var H = GameObject.FindGameObjectWithTag("Herder");
        Vector3 beginplekgraan = new Vector3(H.transform.position.x, H.transform.position.y, H.transform.position.z);
        transform.position = beginplekgraan;
        transform.parent = H.transform;
        graantje = true;
    }

    void SetAantalGraanText()
    {
        AantalGraanText.text = "" + aantalgraangekocht;
    }

}
