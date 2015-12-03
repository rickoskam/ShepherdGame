using UnityEngine;
using System.Collections;

public class HeksScript : MonoBehaviour {
    private int keuze = 1;
    private int lengte = 0;
    private int chosenone = 0;
    float xdesiredr;
    float zdesiredr;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (keuze == 1)
        {
            KeuzeMaken();
        }

        if (keuze == 2)
        {
            InvokeRepeating("SchaapZoeken", 0, 0.2f);
        }
        if (keuze == 3)
        {
            InvokeRepeating("RandomPlekje", 0, 0.2f);
        }
        
	}

    void KeuzeMaken()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Schaap");
        lengte = gos.Length;

        if (lengte > 0)
        {
            System.Random rn = new System.Random();

            int k = rn.Next(2);

            if (k == 0)
            {
                System.Random r = new System.Random();

                chosenone = r.Next(lengte);

                keuze = 2;
            }
            else
            {
                xdesiredr = Random.Range(-40, 40); //de range is de grootte van het weiland
                zdesiredr = Random.Range(-40, 40); //de range is de grootte van het weiland 

                keuze = 3;
            }
            
        }
        if (lengte == 0)
        {
            xdesiredr = Random.Range(-40, 40);//de range is de grootte van het weiland
            zdesiredr = Random.Range(-40, 40);//de range is de grootte van het weiland

            keuze = 3;
        }
    }

    void SchaapZoeken()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Schaap");
        lengte = gos.Length;

        if (chosenone >= lengte)
        {
            CancelInvoke();
            keuze = 1;
            return;
        }

        if (lengte > 0)
        {
            GameObject o1 = gos[chosenone];

            if (o1.activeSelf == false)
            {
                keuze = 1;
                CancelInvoke();
            }

            float xdesired = o1.transform.position.x;
            float zdesired = o1.transform.position.z;

            float xeigen = gameObject.transform.position.x;
            float zeigen = gameObject.transform.position.z;

            float xrichting = xdesired - xeigen;
            float zrichting = zdesired - zeigen;

            transform.Translate(new Vector3(xrichting * Time.deltaTime, 0, zrichting * Time.deltaTime));

            if (Mathf.Abs(xdesired - xeigen) < 0.1 && Mathf.Abs(zdesired - zeigen) < 0.1)
            {
                DoodmakenSchaap();
                keuze = 1;
                CancelInvoke();
            }
        }
    }

    void RandomPlekje()
    {
        float xeigen = gameObject.transform.position.x;
        float zeigen = gameObject.transform.position.z;

        float xrichting = xdesiredr - xeigen;
        float zrichting = zdesiredr - zeigen;

        transform.Translate(new Vector3(xrichting * Time.deltaTime, 0, zrichting * Time.deltaTime));

        if (Mathf.Abs(xdesiredr - xeigen) < 0.1 && Mathf.Abs(zdesiredr - zeigen) < 0.1)
        {
            keuze = 1;
            CancelInvoke();
        }
    }

    void DoodmakenSchaap()
    {
        System.Random r1 = new System.Random();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Schaap");
        GameObject o1 = gos[chosenone];

        int r2 = r1.Next(10);

        if (r2 == 0)
        {
            //doodschieten effect
            o1.SetActive(false);
        }
    }
}

