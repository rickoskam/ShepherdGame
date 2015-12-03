using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Herder : MonoBehaviour {
	float speed = 15;
	float rotatespeed = 10;
    float rotatecamera = 8;
    float positiecameray = 4;
    float positiecameraz = 1;
    public Text Wintext;
    public Slider Ziekschaapslider;
    public Image Ziekschaapimage;
    public Text Aantalschapeninveld;
    public Text Aantalschapeninbarn;
    public Button Pausebutton;
    public Button Resumebutton;
    public Button FFButton1;
    public Button FFButton2;
    int keuz;


    // Use this for initialization
    void Start () {
        Wintext.text = "";
        Ziekschaapslider.gameObject.SetActive(false);
        Ziekschaapimage.gameObject.SetActive(false);
        Pausebutton.gameObject.SetActive(true);
        Resumebutton.gameObject.SetActive(false);
        FFButton1.gameObject.SetActive(true);
        FFButton2.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        var C = GameObject.FindGameObjectWithTag("MainCamera");
        if (Input.GetAxis("Mouse X")>0)
		{
            transform.Rotate(new Vector3(0, rotatespeed * Input.GetAxis("Mouse X"), 0));
		}
        if (Input.GetAxis("Mouse X")<0)
		{
			transform.Rotate(new Vector3(0, rotatespeed * Input.GetAxis("Mouse X"),0));
		}
        if (Input.GetAxis("Mouse Y")>0)
        {
            C.transform.Rotate(new Vector3(rotatecamera * Input.GetAxis("Mouse Y")*Time.deltaTime,0,0));
            C.transform.Translate(new Vector3(0, positiecameray * Input.GetAxis("Mouse Y") * Time.deltaTime, positiecameraz * Input.GetAxis("Mouse Y") * Time.deltaTime));
        }
        if (Input.GetAxis("Mouse Y")<0)
        {
            C.transform.Rotate(new Vector3(rotatecamera * Input.GetAxis("Mouse Y")* Time.deltaTime, 0, 0));
            C.transform.Translate(new Vector3(0, positiecameray * Input.GetAxis("Mouse Y") * Time.deltaTime, positiecameraz * Input.GetAxis("Mouse Y") * Time.deltaTime));
        }

		if(Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate(new Vector3(0,0,speed * Time.deltaTime));
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate(new Vector3(0,0,-speed * Time.deltaTime));
		}
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }

        GameObject[] ziek = GameObject.FindGameObjectsWithTag("ZiekSchaap");
        int leng = ziek.Length;

        if (leng == 0)
        {
            keuz = 0;
        }

        if (leng > 0)
        {
            if (keuz == 0)
            {
                keuz = 1;
                Ziekschaapslider.gameObject.SetActive(true);
                Ziekschaapimage.gameObject.SetActive(true);
            }
            for (int m = 0; m < leng; m++)
            {
                GameObject ziekschaap = ziek[m];
                float xposs = ziekschaap.transform.position.x;
                float zposs = ziekschaap.transform.position.z;
                float xposh = transform.position.x;
                float zposh = transform.position.z;
                float xvers = Mathf.Abs(xposs - xposh);
                float zvers = Mathf.Abs(zposs - zposh);
                if (xvers <10f && zvers<10f && Input.GetKey(KeyCode.Alpha3))
                {
                    ziekschaap.tag = "Schaap";
                    ziekschaap.GetComponent<Renderer>().material.color = Color.grey;
                    keuz = 0;
                    Ziekschaapslider.value = 20;
                    Ziekschaapslider.gameObject.SetActive(false);
                    Ziekschaapimage.gameObject.SetActive(false);
                }
            }
        }

        if (keuz == 1)
        {
            StartCoroutine(Timerziekschaap());
        }

        if (keuz == 2)
        {
            StartCoroutine(TimerziekschaapA());
        }

        if (keuz == 3)
        {
            StartCoroutine(TimerziekschaapB());
        }

        if (keuz == 4)
        {
            StartCoroutine(TimerziekschaapC());
        }

        if (keuz == 5)
        {
            StartCoroutine(TimerziekschaapD());
        }

        if (keuz == 6)
        {
            StartCoroutine(TimerziekschaapE());
        }

        if (keuz == 7)
        {
            StartCoroutine(TimerziekschaapF());
        }

        if (keuz == 8)
        {
            StartCoroutine(TimerziekschaapG());
        }

        if (keuz == 9)
        {
            StartCoroutine(TimerziekschaapH());
        }

        if (keuz == 10)
        {
            StartCoroutine(Timerziekschaaplaatste());
        }

        GameObject[] schapenlijst = GameObject.FindGameObjectsWithTag("Schaap");
        int isl = schapenlijst.Length;
        Aantalschapeninveld.text = "" + isl;

        GameObject[] gevsch = GameObject.FindGameObjectsWithTag("GevangenSchaap");
        int gsl = gevsch.Length;
        Aantalschapeninbarn.text = "" + gsl;

        if (isl == 0 && leng == 0 && gsl > 0)
        {
            Wintext.text = "YOU WIN!";
        }

        if (isl == 0 && leng == 0 && gsl == 0)
        {
            Wintext.text = "GAME OVER";
        }
    }

    IEnumerator Timerziekschaap()
    {
        GameObject[] ziek = GameObject.FindGameObjectsWithTag("ZiekSchaap");
        int leng = ziek.Length;
        if (leng > 0)
        {
            yield return new WaitForSeconds(2);
            Ziekschaapslider.value = 18;
            keuz = 2;
        }
    }

    IEnumerator TimerziekschaapA()
    {
        GameObject[] ziek = GameObject.FindGameObjectsWithTag("ZiekSchaap");
        int leng = ziek.Length;
        if (leng > 0)
        {
            yield return new WaitForSeconds(2);
            Ziekschaapslider.value = 16;
            keuz = 3;
        }
    }

    IEnumerator TimerziekschaapB()
    {
        GameObject[] ziek = GameObject.FindGameObjectsWithTag("ZiekSchaap");
        int leng = ziek.Length;
        if (leng > 0)
        {
            yield return new WaitForSeconds(2);
            Ziekschaapslider.value = 14;
            keuz = 4;
        }
    }

    IEnumerator TimerziekschaapC()
    {
        GameObject[] ziek = GameObject.FindGameObjectsWithTag("ZiekSchaap");
        int leng = ziek.Length;
        if (leng > 0)
        {
            yield return new WaitForSeconds(2);
            Ziekschaapslider.value = 12;
            keuz = 5;
        }
    }

    IEnumerator TimerziekschaapD()
    {
        GameObject[] ziek = GameObject.FindGameObjectsWithTag("ZiekSchaap");
        int leng = ziek.Length;
        if (leng > 0)
        {
            yield return new WaitForSeconds(2);
            Ziekschaapslider.value = 10;
            keuz = 6;
        }
    }

    IEnumerator TimerziekschaapE()
    {
        GameObject[] ziek = GameObject.FindGameObjectsWithTag("ZiekSchaap");
        int leng = ziek.Length;
        if (leng > 0)
        {
            yield return new WaitForSeconds(2);
            Ziekschaapslider.value = 8;
            keuz = 7;
        }
    }

    IEnumerator TimerziekschaapF()
    {
        GameObject[] ziek = GameObject.FindGameObjectsWithTag("ZiekSchaap");
        int leng = ziek.Length;
        if (leng > 0)
        {
            yield return new WaitForSeconds(2);
            Ziekschaapslider.value = 6;
            keuz = 8;
        }
    }

    IEnumerator TimerziekschaapG()
    {
        GameObject[] ziek = GameObject.FindGameObjectsWithTag("ZiekSchaap");
        int leng = ziek.Length;
        if (leng > 0)
        {
            yield return new WaitForSeconds(2);
            Ziekschaapslider.value = 4;
            keuz = 9;
        }
    }

    IEnumerator TimerziekschaapH()
    {
        GameObject[] ziek = GameObject.FindGameObjectsWithTag("ZiekSchaap");
        int leng = ziek.Length;
        if (leng > 0)
        {
            yield return new WaitForSeconds(2);
            Ziekschaapslider.value = 2;
            keuz = 10;
        }
    }

    IEnumerator Timerziekschaaplaatste()
    {
        GameObject[] ziek = GameObject.FindGameObjectsWithTag("ZiekSchaap");
        int leng = ziek.Length;
        if (leng > 0)
        {
            yield return new WaitForSeconds(2);
            Ziekschaapslider.value = 0;
            ziek[0].SetActive(false);
            keuz = 0;
            Ziekschaapslider.value = 20;
            Ziekschaapslider.gameObject.SetActive(false);
            Ziekschaapimage.gameObject.SetActive(false);
        }
    }

    public void Pause()
    {
        Pausebutton.gameObject.SetActive(false);
        Resumebutton.gameObject.SetActive(true);
        FFButton1.gameObject.SetActive(false);
        FFButton2.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Resumebutton.gameObject.SetActive(false);
        Pausebutton.gameObject.SetActive(true);
        FFButton1.gameObject.SetActive(true);
        FFButton2.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    public void FF()
    {
        Pausebutton.gameObject.SetActive(false);
        Resumebutton.gameObject.SetActive(true);
        FFButton1.gameObject.SetActive(false);
        FFButton2.gameObject.SetActive(false);
        Time.timeScale = 1.5f;
    }
}
