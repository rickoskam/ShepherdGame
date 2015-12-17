using UnityEngine;
using System.Collections;

public class MovingCamera : MonoBehaviour {
    float y2 = 1071;
    private float speed = 0.1f;
    public GameObject MainMenu;
    
    // Use this for initialization
   
       
       
    // Update is called once per frame
    void Update () {
        transform.Translate(new Vector3(0.0f, y2*Time.deltaTime*speed, 0.0f));
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            reset1();
            reset2();
            
        }
    }

    public void disable()
    {
        gameObject.SetActive(false);
    }

    public void enable()
    {
        gameObject.SetActive(true);
    }

    public void reset1()
    {
        //GameObject goo = GameObject.FindGameObjectWithTag("Credit");
       
      

        var H = GameObject.FindGameObjectWithTag("tag1");
        H.transform.position = new Vector3(198.5f, -363, 0);
        var I = GameObject.FindGameObjectWithTag("tag2");
        I.transform.position = new Vector3(198.5f, -451, 0);
        var J = GameObject.FindGameObjectWithTag("tag3");
        J.transform.position = new Vector3(198.5f, -557, 0);
        var K = GameObject.FindGameObjectWithTag("tag4");
        K.transform.position = new Vector3(198.5f, -645, 0);
        var L = GameObject.FindGameObjectWithTag("tag5");
        L.transform.position = new Vector3(198.5f, -753, 0);
        var M = GameObject.FindGameObjectWithTag("tag6");
        M.transform.position = new Vector3(198.5f, -857, 0);


    }
    public void reset2()
    {
 MainMenu.SetActive(true);
        GameObject go = GameObject.FindGameObjectWithTag("CreditCanvas");
        go.SetActive(false);
    }

}
