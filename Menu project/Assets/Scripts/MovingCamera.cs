using UnityEngine;
using System.Collections;

public class MovingCamera : MonoBehaviour {
    float y = -998;
    public float speed = 10;
    // Use this for initialization

   

    void Start() { 
    
	    	}
	
	// Update is called once per frame
	void Update () {
        var H = GameObject.FindGameObjectWithTag("camera");
        transform.Translate(new Vector3(0.0f, y*Time.deltaTime*speed, 0.0f));
    }
    public void disable()
    {
        gameObject.SetActive(false);
    }

    public void enable()
    {
        gameObject.SetActive(true);
    }
}
