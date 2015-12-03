using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class SchaapScript : MonoBehaviour
{
    public float speed = 1;
    private bool herdernietdichtbij = true;


    void Start()
    {
        //        var K = GameObject.FindGameObjectWithTag("Kudde");
        //        float randx = Random.Range(0f, 10f);
        //        float randz = Random.Range(0f, 10f);
        //        K.transform.position = new Vector3(randx, 0, randz);

    }

    void Update()
    {
        GameObject[] goss = GameObject.FindGameObjectsWithTag("Schaap");
        int lengte = goss.Length;
        for (int j = 0; j<lengte; j++)
        {
            // De positie bepalen van elk schaap 'schaapje'
            GameObject schaapje = goss[j];
            float sheepx = schaapje.transform.position.x;
            float sheepz = schaapje.transform.position.z;

            // De schapen wiggelen rond door de weide als de herder of de hond niet in de buurt is
            if (herdernietdichtbij == true)
            {
                float randx = Random.Range(-10f, 10f);
                float randz = Random.Range(-10f, 10f);
                
                schaapje.transform.Translate(new Vector3(randx*speed*Time.deltaTime*0.1f, 0.0f, randz*speed*Time.deltaTime*0.1f));
                //Vector3 direction1 = new Vector3(randx * speed * Time.deltaTime * 0.1f, 0.0f, randz * speed * Time.deltaTime * 0.1f);
                //Quaternion lookrotation = Quaternion.LookRotation(direction1);
                //transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.deltaTime);
            }

            // 1. De positie bepalen van de herder, 2. de afstand tussen schaapje en herder, 3. de cognitieve component
            var H = GameObject.FindGameObjectWithTag("Herder");
            float herderx = H.transform.position.x;
            float herdery = H.transform.position.z;
            float afstandx = herderx - sheepx;
            float afstandz = herdery - sheepz;
            float sdesiredx = - 0.5f * afstandx;
            float sdesiredz = - 0.5f * afstandz;
            float euclid = Mathf.Sqrt((afstandx * afstandx) + (afstandz * afstandz));

            // De positie bepalen van de hond en de afstand tussen schaapje en hond
            var D = GameObject.FindGameObjectWithTag("Hond");
            float hondx = D.transform.position.x;
            float hondz = D.transform.position.z;
            float afstandhondx = hondx - sheepx;
            float afstandhondz = hondz - sheepz;
            float afstandhond = Mathf.Sqrt((afstandhondx * afstandhondx) + (afstandhondz * afstandhondz));

            // De drie dichtsbijzijnde schapen voor schaapje zoeken en daarmee de sociale component
            float besteafstand1 = 100;
            GameObject schaapbest1 = goss[0];
            float besteafstand2 = 100;
            GameObject schaapbest2 = goss[0];
            float besteafstand3 = 100;
            GameObject schaapbest3 = goss[0];
            for (int i = 0; i < lengte; i++)
            {
                GameObject tijdelijkschaap = goss[i];
                afstandx = tijdelijkschaap.transform.position.x - schaapje.transform.position.x;
                afstandz = tijdelijkschaap.transform.position.z - schaapje.transform.position.z;
                if (afstandx < 0.01f && afstandz < 0.01f)
                {

                }
                else
                {
                    float tijdelijkeafstand = Mathf.Sqrt((afstandx * afstandx) + (afstandz * afstandz));
                    if (tijdelijkeafstand < besteafstand1)
                    {
                        besteafstand1 = tijdelijkeafstand;
                        schaapbest1 = tijdelijkschaap;
                    }
                    else if (tijdelijkeafstand < besteafstand2)
                    {
                        besteafstand2 = tijdelijkeafstand;
                        schaapbest2 = tijdelijkschaap;
                    }
                    else if (tijdelijkeafstand < besteafstand3)
                    {
                        besteafstand3 = tijdelijkeafstand;
                        schaapbest3 = tijdelijkschaap;
                    }
                }
            }
            float xafstand1 = schaapbest1.transform.position.x;
            float xafstand2 = schaapbest2.transform.position.x;
            float xafstand3 = schaapbest3.transform.position.x;
            float zafstand1 = schaapbest1.transform.position.z;
            float zafstand2 = schaapbest2.transform.position.z;
            float zafstand3 = schaapbest3.transform.position.z;
            float xafstanddesired = ((xafstand1 + xafstand2 + xafstand3) / 3);
            float zafstanddesired = ((zafstand1 + zafstand2 + zafstand3) / 3);
            float xverschil = (xafstanddesired - sheepx);
            float zverschil = (zafstanddesired - sheepz);

            // Het gemiddelde berekenen van de cognitieve en de sociale component
            float xkant = ((sdesiredx + xverschil) / 2);
            float zkant = ((sdesiredz + zverschil) / 2);

            // Wat schaapje moet doen als de hond dichtbij is (alleen sociale component!)
            if (afstandhond <=5)
            {
                herdernietdichtbij = false;
                //xverschil = xverschil * 2;
                //zverschil = zverschil * 2;
                schaapje.transform.Translate(new Vector3(xverschil * Time.deltaTime*speed, 0.0f, zverschil * Time.deltaTime*speed));
            }
            else
            {
                herdernietdichtbij = true;
            }

            // Wat schaapje moet doen als de herder dichtbij is (sociale en cognitieve component)
            if (euclid <= 10)
            {
                herdernietdichtbij = false;
                schaapje.transform.Translate(new Vector3(xkant * Time.deltaTime*speed, 0.0f, zkant * Time.deltaTime*speed));
            }
            else
            {
                herdernietdichtbij = true;
            }
        }
        GameObject[] gevangen = GameObject.FindGameObjectsWithTag("GevangenSchaap");
        int le = gevangen.Length;
        for (int k = 0; k < le; k++)
        {
            GameObject gevschaap = gevangen[k];
            float sheepx = gevschaap.transform.position.x;
            float sheepz = gevschaap.transform.position.z;
            float randx = Random.Range(-20,-40);
            float randz = Random.Range(20,40);
            float xrichting = randx - sheepx;
            float zrichting = randz - sheepz;
            gevschaap.transform.Translate(new Vector3(xrichting * speed * Time.deltaTime * 0.1f, 0.0f, zrichting * speed * Time.deltaTime * 0.1f));
        }

    }



}
// closes update. 


