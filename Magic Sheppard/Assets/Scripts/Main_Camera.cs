using UnityEngine;
using System.Collections;

public class Main_Camera : MonoBehaviour {
    public void disable()
    {
        gameObject.SetActive(false);
    }

    public void enable()
    {
        gameObject.SetActive(true);
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
