using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HondScript : MonoBehaviour {
    public static int aantalhondengekocht = HondWinkelScript.aantalhondgekocht; //PRIVATE MAKEN!
    public Text AantalHondenGekocht;
    bool aan = false;
    //float xbegin;
    //float zbegin;
    //bool n1 = false;
    //bool n2 = false;
    //bool n3 = false;
    //bool n4 = false;
    Vector3 schaapplekje = new Vector3();

    public static int Hondgebruiktindezegame;

    // Use this for initialization
    void Start() {
        SetAantalHondenText();
        Hondgebruiktindezegame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (aan == false)
        {
            transform.rotation = Quaternion.identity;
            var H = GameObject.FindGameObjectWithTag("Herder");

            float xdesired = H.transform.position.x -2;
            float zdesired = H.transform.position.z -2;

            float xnu = gameObject.transform.position.x;
            float znu = gameObject.transform.position.z;

            float xrichting = xdesired - xnu;
            float zrichting = zdesired - znu;

            transform.Translate(new Vector3(xrichting*Time.deltaTime, 0, zrichting*Time.deltaTime));
        }

        //Vind het middelste schaap
        GameObject[] schapen = GameObject.FindGameObjectsWithTag("Schaap");
        int lengte = schapen.Length;
        if (lengte > 0)
        {
            float afstandx;
            float afstandz;
            float totaleafstand = 0;
            float kleinstetotaleafstand = 10000;
            GameObject middelsteschaap = schapen[0];
            GameObject tijdelijkschaap = schapen[0];

            for (int j = 0; j < lengte; j++)
            {
                GameObject schaapje = schapen[j];

                for (int i = 0; i < lengte; i++)
                {
                    tijdelijkschaap = schapen[i];
                    afstandx = tijdelijkschaap.transform.position.x - schaapje.transform.position.x;
                    afstandz = tijdelijkschaap.transform.position.z - schaapje.transform.position.z;
                    float afstand = Mathf.Sqrt((afstandx * afstandx) + (afstandz * afstandz));

                    totaleafstand = totaleafstand + afstand;
                }

                if (totaleafstand < kleinstetotaleafstand)
                {
                    kleinstetotaleafstand = totaleafstand;
                    middelsteschaap = tijdelijkschaap;
                }
            }

            schaapplekje = new Vector3(middelsteschaap.transform.position.x, 0.0f, middelsteschaap.transform.position.z);

            if (Input.GetKey(KeyCode.Alpha2) && aantalhondengekocht > 0 && aan == false)
            {
                aan = true;
                aantalhondengekocht = aantalhondengekocht - 1;
                SetAantalHondenText();
                StartCoroutine(Ronddraaien());
                Herder.score = Herder.score - 100;
                Hondgebruiktindezegame = Hondgebruiktindezegame + 1;
            }

            if (aan == true)
            {
                transform.RotateAround(schaapplekje, Vector3.up, 90 * Time.deltaTime);
                //middelsteschaap is heel aantrekkelijk als de hond rondjes loopt
                float midplekx = middelsteschaap.transform.position.x;
                float midplekz = middelsteschaap.transform.position.z;
                GameObject[] andereschapen = GameObject.FindGameObjectsWithTag("Schaap");
                int l1 = schapen.Length;
                if (l1 > 0)
                {
                    for (int s = 0; s<l1; s++)
                    {
                        GameObject sch = andereschapen[s];
                        float eigenschplekx = sch.transform.position.x;
                        float eigenschplekz = sch.transform.position.z;

                        float afstx = (midplekx - eigenschplekx)*0.3f;
                        float afstz = (midplekz - eigenschplekz)*0.3f;

                        sch.transform.Translate(new Vector3(afstx * Time.deltaTime, 0.0f, afstz * Time.deltaTime));
                    }
                }
                }
            else
            {

            }
        }
    }

    IEnumerator Ronddraaien()
    {
        yield return new WaitForSeconds(10);
        aan = false;
    }



            //    {
            //        transform.parent = null;
            //        aan = true;
            //        n1 = true;
            //        aantalhondengekocht = aantalhondengekocht - 1;
            //        SetAantalHondenText();
            //    }
            //    if (n1 == true)
            //    {
            //        xbegin = gameObject.transform.position.x;
            //        zbegin = gameObject.transform.position.z;
            //        InvokeRepeating("PlekjeZoeken", 0, 0.2f);
            //    }
            //    if (n2 == true)
            //    {
            //        xbegin = gameObject.transform.position.x;
            //        zbegin = gameObject.transform.position.z;
            //        InvokeRepeating("PlekjeZoeken2", 0, 0.2f);
            //    }
            //    if (n3 == true)
            //    {
            //        xbegin = gameObject.transform.position.x;
            //        zbegin = gameObject.transform.position.z;
            //        InvokeRepeating("PlekjeZoeken3", 0, 0.2f);
            //    }
            //    if (n4 == true)
            //    {
            //        xbegin = gameObject.transform.position.x;
            //        zbegin = gameObject.transform.position.z;
            //        InvokeRepeating("TerugnaarHerder", 0, 0.2f);
            //    }
            //}


    //        void PlekjeZoeken()
    //{
    //            float xdesired = xbegin + 0.1f;
    //            float zdesired = zbegin + 0.5f;

    //            float xrichting = xdesired - xbegin;
    //            float zrichting = zdesired - zbegin;

    //            float xnu = gameObject.transform.position.x;
    //            float znu = gameObject.transform.position.z;

    //            transform.Translate(new Vector3(xrichting * Time.deltaTime, 0, zrichting * Time.deltaTime));

    //            if (Mathf.Abs(xdesired - xnu) < 0.1 && Mathf.Abs(zdesired - znu) < 0.1)
    //            {
    //                n1 = false;
    //                CancelInvoke();
    //                n2 = true;
    //            }
    //        }

    //        void PlekjeZoeken2()
    //{
    //            float xdesired = xbegin - 0.1f;
    //            float zdesired = zbegin - 0.5f;

    //            float xrichting = xdesired - xbegin;
    //            float zrichting = zdesired - zbegin;

    //            float xnu = gameObject.transform.position.x;
    //            float znu = gameObject.transform.position.z;

    //            transform.Translate(new Vector3(xrichting * Time.deltaTime, 0, zrichting * Time.deltaTime));

    //            if (Mathf.Abs(xdesired - xnu) < 0.1 && Mathf.Abs(zdesired - znu) < 0.1)
    //            {
    //                n2 = false;
    //                CancelInvoke();
    //                n3 = true;
    //            }
    //        }

    //        void PlekjeZoeken3()
    //{
    //            float xdesired = xbegin + 0.1f;
    //            float zdesired = zbegin + 0.5f;

    //            float xrichting = xdesired - xbegin;
    //            float zrichting = zdesired - zbegin;

    //            float xnu = gameObject.transform.position.x;
    //            float znu = gameObject.transform.position.z;

    //            transform.Translate(new Vector3(xrichting * Time.deltaTime, 0, zrichting * Time.deltaTime));

    //            if (Mathf.Abs(xdesired - xnu) < 0.1 && Mathf.Abs(zdesired - znu) < 0.1)
    //            {
    //                n3 = false;
    //                CancelInvoke();
    //                n4 = true;
    //            }
    //        }

    //        void TerugnaarHerder()
    //{
    //            var H = GameObject.FindGameObjectWithTag("Herder");

    //            float xdesired = H.transform.position.x + 1;
    //            float zdesired = H.transform.position.z - 1;

    //            float xnu = gameObject.transform.position.x;
    //            float znu = gameObject.transform.position.z;

    //            float xrichting = xdesired - xnu;
    //            float zrichting = zdesired - znu;

    //            transform.Translate(new Vector3(xrichting * Time.deltaTime, 0, zrichting * Time.deltaTime));

    //            if (Mathf.Abs(xdesired - xnu) < 0.1 && Mathf.Abs(zdesired - znu) < 0.1)
    //            {
    //                n4 = false;
    //                CancelInvoke();
    //                ReturnBeginHond();
    //            }
    //        }

    //        void ReturnBeginHond()
    //{
    //            var H = GameObject.FindGameObjectWithTag("Herder");
    //            transform.parent = H.transform;
    //            aan = false;
    //        }

            void SetAantalHondenText()
    {
                AantalHondenGekocht.text = "" + aantalhondengekocht;
            }
        }

